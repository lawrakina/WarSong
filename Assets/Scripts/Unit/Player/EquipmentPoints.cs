using System.Linq;
using Data;
using Extension;
using UnityEngine;


namespace Unit.Player
{
    public class EquipmentPoints
    {
        private readonly GameObject _character;
        private readonly CharacterData _characterData;
        public GameObject _rightWeaponAttachPoint;
        public GameObject _leftWeaponAttachPoint;
        public GameObject _leftShildAttachPoint;


        public EquipmentPoints(GameObject character, CharacterData characterData)
        {
            _character = character;
            _characterData = characterData;
        }

        public void GenerateAllPoints()
        {
            _leftWeaponAttachPoint =
                GenerateAttachPoint(_characterData.ListAttachPoints.FirstOrDefault(x =>
                    x.Name == StringManager.ATTACH_POINT_LEFT_ONE_WEAPON));
            _rightWeaponAttachPoint =
                GenerateAttachPoint(_characterData.ListAttachPoints.FirstOrDefault(x =>
                    x.Name == StringManager.ATTACH_POINT_RIGHT_ONE_WEAPON));
            _leftShildAttachPoint =
                GenerateAttachPoint(
                    _characterData.ListAttachPoints.FirstOrDefault(x =>
                        x.Name == StringManager.ATTACH_POINT_LEFT_SHILD));
        }

        private GameObject GenerateAttachPoint(AttachPoint point)
        {
            var result = Object.Instantiate(new GameObject($"AttachPoint---{point.Name}"),
                _character.transform.Find(point.Path).transform);
            result.transform.localPosition = point.LocalPosition;
            result.transform.localRotation = Quaternion.Euler(point.LocalRotation);
            result.transform.localScale = point.LocalScale;
            return result;
        }
    }
}