using UnityEngine.UI;


namespace Code.Extension
{
    public static class ListenersExtension
    {
        public static void RemoveAllListeners(this Toggle toggle)
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
        public static void RemoveAllListeners(this Button button)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}