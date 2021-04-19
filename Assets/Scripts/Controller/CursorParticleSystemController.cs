using Data;
using Gui;
using Interface;
using UnityEngine;


namespace Controller
{
    internal class CursorParticleSystemController : IController, IExecute
    {
        public CursorParticleSystemController(Canvas canvas)
        {
            _view = LoadView();
            _view.transform.SetParent(canvas.transform);
        }

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/particleCursor"};
        private CursorParticleSystemView _view;

        private CursorParticleSystemView LoadView()
        {
            GameObject objView = Object.Instantiate(Resources.Load<GameObject>(_viewPath.PathResource));
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
    }
}