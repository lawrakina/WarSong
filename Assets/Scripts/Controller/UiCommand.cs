namespace Controller
{
    public class UiCommand
    {
        public delegate void Execute();

        public Execute OnAction;
    }
}