using Code.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Character
{
    public class CellEquipmentHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image;
        private CellEquipment Body { get; set; }

        public void Init(CellEquipment cellEquipment)
        {
            Body = cellEquipment;
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