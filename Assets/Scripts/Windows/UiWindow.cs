using System;
using UnityEngine;


namespace Windows
{
    public abstract class UiWindow : MonoBehaviour
    {
        #region Fields

        [HideInInspector]
        public bool _showBeforeStart = false;

        public Action<Guid> OnShow;
        public Action<Guid> OnHide;

        #endregion


        #region Properties

        public Guid Id { get; } = Guid.NewGuid();
        
        public delegate Transform GetCharacterSpawn();
        public GetCharacterSpawn CharacterSpawn;

        #endregion


        #region ClassLiveCycles

        public virtual void Init()
        {
            if (_showBeforeStart)
                Show();
            else
                Hide();
        }

        ~UiWindow()
        {
            OnShow = null;
            OnHide = null;
        }

        #endregion


        #region Methods

        public virtual void Show()
        {
            OnShow?.Invoke(Id);

            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            OnHide?.Invoke(Id);

            gameObject.SetActive(false);
        }

        #endregion
    }
}