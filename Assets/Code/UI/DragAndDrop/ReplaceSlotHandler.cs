using Code.Extension;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Code.UI.DragAndDrop{
    public class ReplaceSlotHandler : MonoBehaviour, IDropHandler{

        public void OnDrop(PointerEventData eventData){
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged(transform, DragHandler.itemBeingDragged.transform));
        }
    }
}