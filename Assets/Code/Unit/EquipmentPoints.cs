using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Extension;
using UnityEngine;


namespace Code.Unit
{
    public class EquipmentPoints
    {
        private readonly GameObject _character;
        private readonly CharacterData _characterData;
        public Transform _rightWeaponAttachPoint;
        public Transform _leftWeaponAttachPoint;
        public Transform _leftShildAttachPoint;


        public EquipmentPoints(GameObject character, CharacterData characterData)
        {
            _character = character;
            _characterData = characterData;
        }

        public void GenerateAllPoints()
        {
            _leftWeaponAttachPoint =
                GenerateAttachPoint(_characterData.ListAttachPoints.FirstOrDefault(x =>
                    x.Name == StringManager.ATTACH_POINT_LEFT_ONE_WEAPON)).transform;
            _rightWeaponAttachPoint =
                GenerateAttachPoint(_characterData.ListAttachPoints.FirstOrDefault(x =>
                    x.Name == StringManager.ATTACH_POINT_RIGHT_ONE_WEAPON)).transform;
            _leftShildAttachPoint =
                GenerateAttachPoint(
                    _characterData.ListAttachPoints.FirstOrDefault(x =>
                        x.Name == StringManager.ATTACH_POINT_LEFT_SHILD)).transform;
        }

        private GameObject GenerateAttachPoint(AttachPoint point)
        {
            var result = new GameObject($"AttachPoint---{point.Name}");
            result.transform.SetParent(_character.transform.Find(point.Path).transform);
            result.transform.localPosition = point.LocalPosition;
            result.transform.localRotation = Quaternion.Euler(point.LocalRotation);
            result.transform.localScale = point.LocalScale;
            return result;
        }
    }
}