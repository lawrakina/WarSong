using System;
using UnityEngine;


namespace Code.Equipment
{
    [Serializable]
    public class UiInfo
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;
        [SerializeField] private string _description;

        public Sprite Icon => _icon;
        public string Title => _title;
        public string Description => _description;
    }
}