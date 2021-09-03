using UnityEngine;
using UnityEngine.EventSystems;


namespace Code.UI.Character
{
    public class SlotDropHandler: MonoBehaviour, IDropHandler
    {
        public GameObject item
        {
            get
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }

                return null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!item)
            {
                CellEquipmentDragAndDropHandler.itemBeingDragged.transform.SetParent(transform);
                ExecuteEvents.ExecuteHierarchy<IHasChanged>
                    (gameObject, null, (x, y) => x.HasChanged());
            }
        }
    }
}