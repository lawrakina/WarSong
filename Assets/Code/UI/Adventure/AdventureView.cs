using System;
using Code.Extension;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.UI.Adventure
{
    public sealed class AdventureView : UiWindow
    {
        [SerializeField]
        private Button _createRandomLevelButton;

        public void Init(UnityAction startBattle)
        {
            _createRandomLevelButton.onClick.AddListener(startBattle);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _createRandomLevelButton.RemoveAllListeners();
        }
    }
}