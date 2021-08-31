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
        // private ToggleControllerGroup _navigationToggleGroupControllers;

        public MainContentController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            var adventureController = ConfigureAdventureController(_placeForUi, _profilePlayer);
            var characterController = ConfigureCharacterController(_placeForUi, _profilePlayer);
            var inventoryController = ConfigureInventoryController(_placeForUi, _profilePlayer);
            var tavernController = ConfigureTavernController(_placeForUi, _profilePlayer);
            var shopController = ConfigureShopController(_placeForUi, _profilePlayer);
            Init(true);
            // AddController(null, true,true);

            // _navigationToggleGroupControllers = new ToggleControllerGroup();
            // _navigationToggleGroupControllers.Add(adventureController);
            // _navigationToggleGroupControllers.Add(characterController);
            // _navigationToggleGroupControllers.Add(inventoryController);
            // _navigationToggleGroupControllers.Add(tavernController);
            // _navigationToggleGroupControllers.Add(shopController);
            // _navigationToggleGroupControllers.Init();

            _profilePlayer.CommandManager.ShowAdventureWindow.Subscribe(_ => { adventureController.OnActivate(); });
            _profilePlayer.CommandManager.ShowCharacterWindow.Subscribe(_ => { characterController.OnActivate(); });
            _profilePlayer.CommandManager.ShowInventoryWindow.Subscribe(_ => { inventoryController.OnActivate(); });
            _profilePlayer.CommandManager.ShowTavernWindow.Subscribe(_ => { tavernController.OnActivate(); });
            _profilePlayer.CommandManager.ShowShopWindow.Subscribe(_ => { shopController.OnActivate(); });
            
            switch (_profilePlayer.WindowAfterStart)
            {
                case UiWindowAfterStart.Adventure:
                    adventureController.OnActivate();
                    break;

                case UiWindowAfterStart.Characters:
                    characterController.OnActivate();
                    break;

                case UiWindowAfterStart.Inventory:
                    inventoryController.OnActivate();
                    break;

                case UiWindowAfterStart.Tavern:
                    tavernController.OnActivate();
                    break;

                case UiWindowAfterStart.Shop:
                    shopController.OnActivate();
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
            var controller = new InventoryController(false, placeForUi.transform, profilePlayer);
            AddController(controller, true);
            return controller;
        }

        private ShopController ConfigureShopController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new ShopController(false,placeForUi.transform, profilePlayer);
            AddController(controller, true);
            return controller;
        }

        private TavernController ConfigureTavernController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new TavernController(false, placeForUi.transform, profilePlayer);
            AddController(controller, true);
            return controller;
        }

        private CharacterController ConfigureCharacterController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new CharacterController(false, placeForUi.transform, profilePlayer);
            AddController(controller, true);
            return controller;
        }

        private AdventureController ConfigureAdventureController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var controller = new AdventureController(false, placeForUi.transform, profilePlayer);
            AddController(controller, true);
            return controller;
        }

        // protected override void OnDispose()
        // {
        //     base.OnDispose();
        //
        //     _navigationToggleGroupControllers.Dispose();
        // }
    }
}