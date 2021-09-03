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

        public CharacterController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer): base(activate)
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
                TemplateCellEquipmentClickHandler = _profilePlayer.Settings.UiViews.CellTemplateClickEquipment,
                ActiveWeapons = equipment.ActiveWeapons
            };
            switch (equipment.ActiveWeapons)
            {
                case ActiveWeapons.RightHand:
                    listObjects.MainWeapon = new SlotEquipment(equipment.MainWeapon);
                    listObjects.MainWeapon.Command.Subscribe(CellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.TwoHand:
                    listObjects.MainWeapon = new SlotEquipment(equipment.MainWeapon);
                    listObjects.MainWeapon.Command.Subscribe(CellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.RightAndLeft:
                    listObjects.MainWeapon = new SlotEquipment(equipment.MainWeapon, (int )equipment.ActiveWeapons);
                    listObjects.MainWeapon.Command.Subscribe(CellExecute).AddTo(_subscriptions);

                    listObjects.SecondWeapon = new SlotEquipment(equipment.SecondWeapon, (int )equipment.ActiveWeapons);
                    listObjects.SecondWeapon.Command.Subscribe(CellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.RightAndShield:
                    listObjects.MainWeapon = new SlotEquipment(equipment.MainWeapon, (int )equipment.ActiveWeapons);
                    listObjects.MainWeapon.Command.Subscribe(CellExecute).AddTo(_subscriptions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            foreach (ArmorItemType type in Enum.GetValues(typeof(ArmorItemType)))
            {
                var item = equipment.ListArmor.FirstOrDefault(x => x.SubItemType == (int)type);
                var sell = item == null ? new SlotEquipment(null, (int)type) : new SlotEquipment(item, (int)type);
                sell.Command.Subscribe(CellExecute).AddTo(_subscriptions);
                listObjects.Add(sell);
            }

            _equipReplacementController = new EquipReplacementController(false, _placeForUi, _profilePlayer);
            AddController(_equipReplacementController, true);
            AddController(this, true, true);

            _view.Init(listObjects);
            
            Init(true);
        }

        private void CellExecute(SlotEquipment value)
        {
            _equipReplacementController.Show(value);
        }
    }
}