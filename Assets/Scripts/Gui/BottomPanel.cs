using UnityEngine;
using UnityEngine.UI;


namespace Gui
{
    public class BottomPanel : MonoBehaviour
    {
        private void Start()
        {
        }


        #region Fields

        [SerializeField]
        private Toggle _charToggle;

        [SerializeField]
        private Toggle _equipToggle;

        [SerializeField]
        private Toggle _battleToggle;

        [SerializeField]
        private Toggle _spellsToggle;

        [SerializeField]
        private Toggle _talentsToggle;

        #endregion
    }
}