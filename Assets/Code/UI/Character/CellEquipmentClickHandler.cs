using Code.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Character
{
    public class CellEquipmentClickHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image;
        private SlotEquipment Body { get; set; }

        public void Init(SlotEquipment slotEquipment)
        {
            Body = slotEquipment;
            if(Body.EquipmentItem)
                _image.sprite = Body.EquipmentItem.UiInfo.Icon;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Dbg.Log($"Click:?");
            Body?.Command.Execute(Body);
        }
    }
}