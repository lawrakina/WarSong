using UnityEngine.EventSystems;


namespace Code.UI.Character
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}