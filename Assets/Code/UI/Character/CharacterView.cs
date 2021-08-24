using System;
using Code.Data;
using UnityEngine;
using UnityEngine.UI;


namespace Code.UI.Character
{
    public sealed class CharacterView : UiWindow
    {
        [SerializeField] private Text _info;
        [SerializeField] private GameObject _mainSlot;
        [SerializeField] private GameObject _secondSlot;
        [SerializeField] private GameObject _shieldSlot;
        [SerializeField] private GameObject _headSlot;
        [SerializeField] private GameObject _neckSlot;
        [SerializeField] private GameObject _shoulderSlot;
        [SerializeField] private GameObject _bodySlot;
        [SerializeField] private GameObject _cloakSlot;
        [SerializeField] private GameObject _braceletSlot;
        [SerializeField] private GameObject _glovesSlot;
        [SerializeField] private GameObject _beltSlot;
        [SerializeField] private GameObject _pantsSlot;
        [SerializeField] private GameObject _shoesSlot;
        [SerializeField] private GameObject _ringSlot;
        [SerializeField] private GameObject _earringSlot;

        private ListOfSellsEquipment _listObjects;

        public string InfoFormatted
        {
            set => _info.text = value;
        }

        public void Init(ListOfSellsEquipment listObjects)
        {
            _listObjects = listObjects;
            switch (listObjects.ActiveWeapons)
            {
                case ActiveWeapons.RightHand:
                    Instant(_mainSlot, listObjects.MainWeapon);
                    break;
                case ActiveWeapons.TwoHand:
                    Instant(_mainSlot, listObjects.MainWeapon);
                    break;
                case ActiveWeapons.RightAndLeft:
                    Instant(_mainSlot, listObjects.MainWeapon);
                    Instant(_secondSlot, listObjects.SecondWeapon);
                    break;
                case ActiveWeapons.RightAndShield:
                    Instant(_mainSlot, listObjects.MainWeapon);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var equip in _listObjects)
            {
                switch ((ArmorItemType) equip.SubItemType)
                {
                    case ArmorItemType.Shield:
                        Instant(_shieldSlot, equip);
                        break;
                    case ArmorItemType.Head:
                        Instant(_headSlot, equip);
                        break;
                    case ArmorItemType.Neck:
                        Instant(_neckSlot, equip);
                        break;
                    case ArmorItemType.Shoulder:
                        Instant(_shoulderSlot, equip);
                        break;
                    case ArmorItemType.Body:
                        Instant(_bodySlot, equip);
                        break;
                    case ArmorItemType.Cloak:
                        Instant(_cloakSlot, equip);
                        break;
                    case ArmorItemType.Bracelet:
                        Instant(_braceletSlot, equip);
                        break;
                    case ArmorItemType.Gloves:
                        Instant(_glovesSlot, equip);
                        break;
                    case ArmorItemType.Belt:
                        Instant(_beltSlot, equip);
                        break;
                    case ArmorItemType.Pants:
                        Instant(_pantsSlot, equip);
                        break;
                    case ArmorItemType.Shoes:
                        Instant(_shoesSlot, equip);
                        break;
                    case ArmorItemType.Ring:
                        Instant(_ringSlot, equip);
                        break;
                    case ArmorItemType.Earring:
                        Instant(_earringSlot, equip);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Instant(GameObject parent, SellEquipment sellEquipment)
        {
            var sell = Instantiate(_listObjects.TemplateSellEquipmentHandler, parent.transform, false);
            sell.Body = sellEquipment;
        }
    }
}