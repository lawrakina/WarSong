using System.Collections.Generic;
using CoreComponent;
using Enums;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public sealed class PositioningCharacterInMenuController : BaseController
    {
        public PositioningCharacterInMenuController(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumBattleWindow> battleState)
        {
            _battleState = battleState;
            _activeWindow = activeWindow;

            _activeWindow.Subscribe(_ =>
            {
                if (!_isEnable) return;
                if (!_parentsPositions.ContainsKey(_activeWindow.Value)) return;

                SetPlayerPosition(_parentsPositions[_activeWindow.Value]);
            });
        }

        public void AddPlayerPosition(Transform position, EnumMainWindow mainWindow)
        {
            if (position != null)
                _parentsPositions.Add(mainWindow, position);
        }

        private void SetPlayerPosition(Transform position)
        {
            Player.Transform.SetParent(position);
            Player.Transform.localPosition = Vector3.zero;
            Player.Transform.localRotation = Quaternion.identity;
        }


        #region Fields

        private readonly IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;

        private readonly Dictionary<EnumMainWindow, Transform> _parentsPositions =
            new Dictionary<EnumMainWindow, Transform>();

        #endregion


        #region Properties

        public IPlayerView Player { get; set; }

        public IGeneratorDungeon GeneratorDungeon { get; set; }

        #endregion
    }
}