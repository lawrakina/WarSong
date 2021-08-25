using Code.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Character
{
    public class SellEquipmentHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private Image _image;
        public SellEquipment Body { get; set; }
        public void Init(SellEquipment sellEquipment)
        {
            Body = sellEquipment;
            _image.sprite = Body.EquipmentItem.UiInfo.Icon;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Dbg.Log($"Click:?");
            Body?.Command.Execute(Body);
        }

    }
}