using Code.UI;
using UnityEngine;
using UnityEngine.UI;


namespace Code.Loading
{
    public class LoadingView : UiWindow
    {
        [SerializeField] private Text _infoText;

        public void InfoText(string value)
        {
            _infoText.text = value;
        }
    }
}