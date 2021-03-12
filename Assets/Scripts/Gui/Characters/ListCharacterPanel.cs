using Controller;
using Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Characters
{
    public sealed class ListCharacterPanel : BasePanel
    {
        #region Fields

        [SerializeField]
        private Button _prevCharButton;

        [SerializeField]
        private Button _nextCharButton;

        [SerializeField]
        private Button _createCharacterButton;

        [SerializeField]
        private Button _selectingCharacterButton;

        [SerializeField]
        private Text _info;

        private IReactiveProperty<EnumCharacterWindow> _activeCharWindow;
        private ListOfCharactersController _listCharactersManager;
        private IReactiveProperty<EnumMainWindow> _activeWindow;

        #endregion
        
        public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumCharacterWindow> activeCharWindow, ListOfCharactersController listCharactersManager)
        {
            base.Ctor();
            _activeWindow = activeWindow;
            _listCharactersManager = listCharactersManager;
            _activeCharWindow = activeCharWindow;

            //создать нового персонажа
            _createCharacterButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _activeCharWindow.Value = EnumCharacterWindow.NewSelectClass;
            }).AddTo(_subscriptions);

            //выбор персонажа и переход
            _selectingCharacterButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _activeWindow.Value = EnumMainWindow.Battle;
            }).AddTo(_subscriptions);

            //листаем список персонажей
            _prevCharButton.OnPointerClickAsObservable().Subscribe(_ => { _listCharactersManager.MovePrev(); })
                           .AddTo(_subscriptions);
            _nextCharButton.OnPointerClickAsObservable().Subscribe(_ => { _listCharactersManager.MoveNext(); })
                           .AddTo(_subscriptions);
        }
    }
}