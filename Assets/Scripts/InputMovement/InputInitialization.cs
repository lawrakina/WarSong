using Interface;


namespace InputMovement
{
    public sealed class InputInitialization : IInitialization
    {
        #region Fields

        private readonly IUserInputProxy _mobileInputHorizontal;
        private readonly IUserInputProxy _mobileInputVertical;

        #endregion
        
        
        public InputInitialization()
        {
            _mobileInputHorizontal = new MobileInputHorizontal();
            _mobileInputVertical = new MobileInputVertical();
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

        public (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) GetInput()
        {
            (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) result = (_mobileInputHorizontal,
                _mobileInputVertical);
            return result;
        }
    }
}