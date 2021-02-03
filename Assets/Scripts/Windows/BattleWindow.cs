using Enums;
using UniRx;
using UnityEngine;


namespace Windows
{
    public sealed class BattleWindow : BaseWindow
    {
        #region Fields

        [SerializeField]
        private Camera _forTextureRenderCamera;

        private IReactiveProperty<EnumBattleWindow> _battleState;

        #endregion


        #region Properties

        public GameObject Content => _content;

        #endregion


        #region ClassLiveCycles

        public void Ctor(IReactiveProperty<EnumBattleWindow> battleState)
        {
            base.Ctor();
            _battleState = battleState;

            _battleState.Subscribe(_ =>
            {
                _forTextureRenderCamera.gameObject.SetActive(
                    _battleState.Value == EnumBattleWindow.DungeonGenerator);
            });
        }

        #endregion


        #region Methods

        public override void Show()
        {
            base.Show();
            _forTextureRenderCamera.enabled = true;
        }

        public override void Hide()
        {
            base.Hide();
            _forTextureRenderCamera.enabled = false;
        }

        #endregion
    }
}