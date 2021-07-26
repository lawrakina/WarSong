using System;
using Code.Data;
using Code.Profile;
using Code.UI.Adventure;
using Code.UI.Inventory;
using Code.UI.Shop;
using Code.UI.Tavern;
using UniRx;
using UnityEngine;
using CharacterController = Code.UI.Character.CharacterController;


namespace Code
{
    public sealed class MainContentController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private ToggleControllerGroup _navigationToggleGroupControllers;

        public MainContentController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            var adventureController = ConfigureAdventureController(_placeForUi, _profilePlayer);
            var characterController = ConfigureCharacterController(_placeForUi, _profilePlayer);
            var inventoryController = ConfigureInventoryController(_placeForUi, _profilePlayer);
            var tavernController = ConfigureTavernController(_placeForUi, _profilePlayer);
            var shopController = ConfigureShopController(_placeForUi, _profilePlayer);

            _navigationToggleGroupControllers = new ToggleControllerGroup();
            _navigationToggleGroupControllers.Add(adventureController);
            _navigationToggleGroupControllers.Add(characterController);
            _navigationToggleGroupControllers.Add(inventoryController);
            _navigationToggleGroupControllers.Add(tavernController);
            _navigationToggleGroupControllers.Add(shopController);
            _navigationToggleGroupControllers.Init();

            _profilePlayer.CommandManager.ShowAdventureWindow.Subscribe(_ => { adventureController.OnExecute(); });
            _profilePlayer.CommandManager.ShowCharacterWindow.Subscribe(_ => { characterController.OnExecute(); });
            _profilePlayer.CommandManager.ShowInventoryWindow.Subscribe(_ => { inventoryController.OnExecute(); });
            _profilePlayer.CommandManager.ShowTavernWindow.Subscribe(_ => { tavernController.OnExecute(); });
            _profilePlayer.CommandManager.ShowShopWindow.Subscribe(_ => { shopController.OnExecute(); });
            
            switch (_profilePlayer.WindowAfterStart)
            {
                case UiWindowAfterStart.Adventure:
                    adventureController.OnExecute();
                    break;

                case UiWindowAfterStart.Characters:
                    characterController.OnExecute();
                    break;

                case UiWindowAfterStart.Inventory:
                    inventoryController.OnExecute();
                    break;

                case UiWindowAfterStart.Tavern:
                    tavernController.OnExecute();
                    break;

                case UiWindowAfterStart.Shop:
                    shopController.OnExecute();
                    break;

                case UiWindowAfterStart.Tutorial:
                    break;

                case UiWindowAfterStart.StartVideo:
                    break;

                case UiWindowAfterStart.FuckOff:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private InventoryController ConfigureInventoryController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new InventoryController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }

        private ShopController ConfigureShopController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new ShopController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }

        private TavernController ConfigureTavernController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new TavernController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }

        private CharacterController ConfigureCharacterController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new CharacterController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }

        private AdventureController ConfigureAdventureController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new AdventureController(placeForUi.transform, profilePlayer);
            AddController(controller);
            return controller;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _navigationToggleGroupControllers.Dispose();
        }
    }
}