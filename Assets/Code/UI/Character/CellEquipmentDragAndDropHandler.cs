using Code.Data;
using Code.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Character
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CellEquipmentDragAndDropHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        private SlotEquipment Body { get; set; }
        
        public static GameObject itemBeingDragged;
        private Vector3 _startPositionDraggedGameObject;

        public void Init(SlotEquipment slotEquipment)
        {
            Body = slotEquipment;
            if (Body.EquipmentItem != null)
            {
                _image.sprite = Body.EquipmentItem.UiInfo.Icon;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Dbg.Log($"Click:?");
            Body?.Command.Execute(Body);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDragged = gameObject;
            _startPositionDraggedGameObject = transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged.transform.localPosition = Vector3.zero;
            itemBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (!eventData.pointerEnter) return;
            if (!eventData.pointerEnter.TryGetComponent<SlotDropHandler>(out _))
                transform.position = _startPositionDraggedGameObject;
        }
    }
}