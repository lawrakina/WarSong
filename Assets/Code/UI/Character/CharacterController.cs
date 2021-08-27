using System;
using System.Linq;
using Code.Data;
using Code.Extension;
using Code.Profile;
using UniRx;
using UnityEngine;


namespace Code.UI.Character
{
    public sealed class CharacterController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private CharacterView _view;
        
        private EquipReplacementController _equipReplacementController;

        public CharacterController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.CharacterView, _placeForUi, false);
            AddGameObjects(_view.GameObject);
            _profilePlayer.InfoAboutCurrentPlayer
                .Subscribe(info => _view.InfoFormatted = info.AllCharacteristics).AddTo(_subscriptions);

            var equipment = _profilePlayer.CurrentPlayer.UnitEquipment;
            var listObjects = new ListOfCellsEquipment
            {
                TemplateCellEquipmentHandler = _profilePlayer.Settings.UiViews.Equipment_ClearCell,
                ActiveWeapons = equipment.ActiveWeapons
            };
            switch (equipment.ActiveWeapons)
            {
                case ActiveWeapons.RightHand:
                    listObjects.MainWeapon = new CellEquipment(equipment.MainWeapon);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.TwoHand:
                    listObjects.MainWeapon = new CellEquipment(equipment.MainWeapon);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.RightAndLeft:
                    listObjects.MainWeapon = new CellEquipment(equipment.MainWeapon, (int )equipment.ActiveWeapons);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);

                    listObjects.SecondWeapon = new CellEquipment(equipment.SecondWeapon, (int )equipment.ActiveWeapons);
                    listObjects.SecondWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.RightAndShield:
                    listObjects.MainWeapon = new CellEquipment(equipment.MainWeapon, (int )equipment.ActiveWeapons);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            foreach (ArmorItemType type in Enum.GetValues(typeof(ArmorItemType)))
            {
                var item = equipment.ListArmor.FirstOrDefault(x => x.SubItemType == (int)type);
                var sell = item == null ? new CellEquipment(null, (int)type) : new CellEquipment(item, (int)type);
                sell.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                listObjects.Add(sell);
            }

            _equipReplacementController = new EquipReplacementController(_placeForUi, _profilePlayer);
            AddController(_equipReplacementController);
            _equipReplacementController.OffExecute();

            var toggleGroup = new ToggleControllerGroup();
            toggleGroup.SetRoot(this);
            toggleGroup.Add(_equipReplacementController);
            toggleGroup.Init();
            
            OnExecute();
            
            _view.Init(listObjects);
        }

        private void SellExecute(CellEquipment value)
        {
            // _view.Hide();
            _equipReplacementController.ShowReplacementVariants(value);
            Dbg.Log($"{value}. Command Execute");
        }
    }
}