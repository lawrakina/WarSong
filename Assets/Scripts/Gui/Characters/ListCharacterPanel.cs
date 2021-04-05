using Windows;
using Controller;
using Enums;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


namespace Gui.Characters
{
    public sealed class ListCharacterPanel : UiWindow
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
        public Text _info;

        
        public UiCommand _selectCharAction = new UiCommand();
        public UiCommand _createPrototypeAction = new UiCommand();
        public UiCommand _moveNextCharAction = new UiCommand();
        public UiCommand _movePrevCharAction = new UiCommand();
        public UiCommand _changeInfoAction = new UiCommand();
        
        #endregion


        public override void Init()
        {
            base.Init();
            
            _selectingCharacterButton.onClick.AddListener(() => { _selectCharAction.OnAction?.Invoke(); });
            _createCharacterButton.onClick.AddListener(() => { _createPrototypeAction.OnAction?.Invoke(); });
            _nextCharButton.onClick.AddListener(() => { _moveNextCharAction.OnAction?.Invoke(); });
            _prevCharButton.onClick.AddListener(() => { _movePrevCharAction.OnAction?.Invoke(); });
        }

        // public void Ctor(ListOfCharactersController listCharactersManager)
        // {
        //     base.Ctor();
        //     _listCharactersManager = listCharactersManager;
        //
        //     //создать нового персонажа
        //     _createCharacterButton.onClick.AddListener(()=>
        //         {
        //             _listCharactersManager.CreatePrototupe();
        //             
        //         });
        //         
        //         .OnPointerClickAsObservable().Subscribe(_ =>
        //     {
        //         _activeCharWindow.Value = EnumCharacterWindow.NewSelectClass;
        //     }).AddTo(_subscriptions);
        //
        //     //выбор персонажа и переход
        //     _selectingCharacterButton.OnPointerClickAsObservable().Subscribe(_ =>
        //     {
        //         _activeWindow.Value = EnumMainWindow.Battle;
        //     }).AddTo(_subscriptions);
        //
        //     //листаем список персонажей
        //     _prevCharButton.OnPointerClickAsObservable().Subscribe(_ => { _listCharactersManager.MovePrev(); })
        //                    .AddTo(_subscriptions);
        //     _nextCharButton.OnPointerClickAsObservable().Subscribe(_ => { _listCharactersManager.MoveNext(); })
        //                    .AddTo(_subscriptions);
        // }
    }
}