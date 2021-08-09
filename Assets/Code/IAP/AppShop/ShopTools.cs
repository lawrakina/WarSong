using System.Collections.Generic;
using UnityEngine.Purchasing;


namespace Code.IAP.AppShop
{
    public class ShopTools : IShop, IStoreListener
    {
        private IStoreController _controller;
        private IExtensionProvider _extensionProvider;
        private bool _isInitialized;

        private readonly SubscriptionAction _onSuccessPurchase;
        private readonly SubscriptionAction _onFailedPurchase;
        
        public ShopTools(List<ShopProduct> products)
        {
            _onSuccessPurchase = new SubscriptionAction();
            _onFailedPurchase = new SubscriptionAction();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (ShopProduct product in products)
            {
                builder.AddProduct(product.Id, product.CurrentProductType);
            }
        }

        public IReadOnlySubscriptionAction OnSuccessPurchase => _onSuccessPurchase;
        public IReadOnlySubscriptionAction OnFailedPurchase => _onFailedPurchase;
        
        public void Buy(string id)
        {
            if(!_isInitialized)
                return;
            _controller.InitiatePurchase(id);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _isInitialized = false;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            _onSuccessPurchase.Invoke();
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _onFailedPurchase.Invoke();
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensionProvider = extensions;
            _isInitialized = true;
        }
        
        public string GetCost(string productID)
        {
            Product product = _controller.products.WithID(productID);
            
            if (product != null)
                return product.metadata.localizedPriceString;

            return "N/A";
        }
        
        public void RestorePurchase()
        {
            if (!_isInitialized)
            {
                return;
            }
                      
#if UNITY_IOS
            _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestoreFinished); 
#else
            _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnRestoreFinished);
#endif
        }

        private void OnRestoreFinished(bool isSuccess)
        {
            
        }
    }
}