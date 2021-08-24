using Code.Extension;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Code.UI.Character
{
    public class SellEquipmentHandler : MonoBehaviour, IPointerDownHandler
    {
        public SellEquipment Body { get; set; }
        public void OnPointerDown(PointerEventData eventData)
        {
            Dbg.Log($"Click:?");
            Body?.Command.Execute(Body);
        }
    }
}