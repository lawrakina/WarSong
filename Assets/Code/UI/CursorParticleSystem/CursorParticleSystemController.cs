﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.UI.CursorParticleSystem
{
    public class CursorParticleSystemController : IController, IExecute
    {
        public CursorParticleSystemController(Canvas canvas)
        {
            _view = LoadView();
            _view.transform.SetParent(canvas.transform);
        }

        private readonly string _viewPath = "Prefabs/particleCursor";
        private CursorParticleSystemView _view;

        private CursorParticleSystemView LoadView()
        {
            GameObject objView = Object.Instantiate(Resources.Load<GameObject>(_viewPath));
            return objView.GetComponent<CursorParticleSystemView>();
        }

        public void Execute(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _view.PlayParticle(Input.mousePosition);
            }

            if (Input.touchCount > 0)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _view.PlayParticle(Input.mousePosition);
                }
        }

        public Guid Id { get; }
        public void OnExecute()
        {
            throw new NotImplementedException();
        }

        public void OffExecute()
        {
            throw new NotImplementedException();
        }

        public bool IsOn { get; }
    }
}