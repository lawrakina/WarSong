using System;
using UnityEngine;
using UnityEngine.UIElements;


namespace Code.Equipment
{
    public interface IUiEquipItem
    {
        [SerializeField]
        UiInfo UiInfo { get; }
    }

    [Serializable]
    public class UiInfo
    {
        [SerializeField]
        public Image Icon { get; }
        [SerializeField]
        public string Title { get; }
        [SerializeField]
        public string Description { get; }
    }
}