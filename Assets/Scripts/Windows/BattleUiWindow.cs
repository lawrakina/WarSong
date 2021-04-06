using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Windows
{
    public sealed class BattleUiWindow : UiWindow
    {
        #region Fields

        [Header("UI")]
        [SerializeField]
        private Button _startButton;

        #endregion


        #region Properties

        public ReactiveCommand _startBattleCommand = new ReactiveCommand();

        #endregion


        #region ClassLiveCycles

        private void Awake()
        {
            _startBattleCommand.BindTo(_startButton);
        }

        #endregion
    }
}