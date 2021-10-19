using UnityEngine;
using UnityEngine.EventSystems;


namespace Code.UI.DragAndDrop{
    public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static GameObject itemBeingDragged;
        private Vector3 _startPositionDraggedGameObject;

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
            if (!eventData.pointerEnter.TryGetComponent<ReplaceSlotHandler>(out _))
                transform.position = _startPositionDraggedGameObject;
        }
    }
}