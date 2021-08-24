using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Equipment;
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

        public CharacterController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.CharacterView, _placeForUi, false);
            AddGameObjects(_view.gameObject);
            _profilePlayer.InfoAboutCurrentPlayer
                .Subscribe(info => _view.InfoFormatted = info.AllCharacteristics).AddTo(_subscriptions);

            var equipment = _profilePlayer.CurrentPlayer.UnitEquipment;
            var listObjects = new ListOfSellsEquipment
            {
                TemplateSellEquipmentHandler = _profilePlayer.Settings.UiViews.Equipment_ClearSell,
                ActiveWeapons = equipment.ActiveWeapons
            };
            switch (equipment.ActiveWeapons)
            {
                case ActiveWeapons.RightHand:
                    listObjects.MainWeapon = new SellEquipment(equipment.MainWeapon);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.TwoHand:
                    listObjects.MainWeapon = new SellEquipment(equipment.MainWeapon);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.RightAndLeft:
                    listObjects.MainWeapon = new SellEquipment(equipment.MainWeapon, (int )equipment.ActiveWeapons);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);

                    listObjects.SecondWeapon = new SellEquipment(equipment.SecondWeapon, (int )equipment.ActiveWeapons);
                    listObjects.SecondWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                case ActiveWeapons.RightAndShield:
                    listObjects.MainWeapon = new SellEquipment(equipment.MainWeapon, (int )equipment.ActiveWeapons);
                    listObjects.MainWeapon.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            foreach (ArmorItemType type in Enum.GetValues(typeof(ArmorItemType)))
            {
                var item = equipment.ListArmor.FirstOrDefault(x => x.ArmorItemType == type);
                var sell = item == null ? new SellEquipment(null, (int)type) : new SellEquipment(item, (int)type);
                sell.Command.Subscribe(SellExecute).AddTo(_subscriptions);
                listObjects.Add(sell);
            }

            _view.Init(listObjects);
        }

        private void SellExecute(SellEquipment value)
        {
            Dbg.Log($"{value.EquipmentItem.name}. Command Execute");
        }
    }


    public class ListOfSellsEquipment : List<SellEquipment>
    {
        public SellEquipmentHandler TemplateSellEquipmentHandler { get; set; }
        public SellEquipment MainWeapon { get; set; }
        public SellEquipment SecondWeapon { get; set; }
        public ActiveWeapons ActiveWeapons { get; set; }
    }
    
    public class SellEquipment
    {
        public InventoryItemType ItemType { get; }
        public int SubItemType { get; }
        public ReactiveCommand<SellEquipment> Command { get; } = new ReactiveCommand<SellEquipment>();
        public BaseEquipItem EquipmentItem { get; }
        
        public SellEquipment(BaseEquipItem equip, int subType = -1)
        {
            ItemType = equip == null ? InventoryItemType.None : equip.ItemType;
            SubItemType = subType;
            EquipmentItem = equip;
        }
    }
}