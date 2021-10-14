using UnityEngine;
using UnityEngine.EventSystems;


namespace Code.UI.DragAndDrop{
    public interface IHasChanged : IEventSystemHandler{
        void HasChanged(Transform sender, Transform dropped);
    }
}