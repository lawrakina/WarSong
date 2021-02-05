using InputMovement;
using Interface;


namespace Controller
{
    internal class InputController : IInitialization, IExecute
    {
        #region Fields

        private readonly IUserInputProxy _horizontal;
        private readonly IUserInputProxy _vertical;

        #endregion


        #region ClassLiveCycles

        public InputController((IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) input)
        {
            _horizontal = input.inputHorizontal;
            _vertical = input.inputVertical;
        }

        #endregion


        public void Execute(float deltaTime)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
        }

        public void On()
        {
        }

        public void Off()
        {
        }

        public void Initialization()
        {
        }
    }
}