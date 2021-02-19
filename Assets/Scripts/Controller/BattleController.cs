using Interface;


namespace Controller
{
    public sealed class BattleController : BaseController, IExecute, IFixedExecute
    {
        #region Fields

        private readonly EcsBattle.EcsBattle _ecsEngine;

        #endregion


        #region ClassLiveCycles

        public BattleController(EcsBattle.EcsBattle ecsEngine)
        {
            _ecsEngine = ecsEngine;
        }

        #endregion


        #region UnityMethods

        public void Execute(float deltaTime)
        {
            _ecsEngine.Execute();
        }

        public void FixedExecute(float deltaTime)
        {
            _ecsEngine.FixedExecute();
        }

        #endregion
    }
}