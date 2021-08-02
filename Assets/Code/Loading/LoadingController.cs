using Code.Data;
using Code.Extension;
using UniRx;
using UnityEngine;


namespace Code.Loading
{
    public sealed class LoadingController : BaseController
    {
        private readonly ReactiveProperty<string> _infoState;
        private LoadingView _loadingView;

        public LoadingController(UiViewsData uiViews, ReactiveProperty<string> infoState, Transform placeForUi)
        {
            _infoState = infoState;
            
            _loadingView = ResourceLoader.InstantiateObject(uiViews.LoadingView, placeForUi, false);
            AddGameObjects(_loadingView.gameObject);
            infoState.Subscribe(_loadingView.InfoText).AddTo(_subscriptions);
            _loadingView.Init();
            
            _loadingView.Hide();
        }

        public void ShowLoading()
        {
            _loadingView.Show();
        }

        public void UpdateInfo()
        {
            Dbg.Log($"LoadingController.UpdateInfo");
        }

        public void HideLoading()
        {
            _loadingView.Hide();
        }
    }
}