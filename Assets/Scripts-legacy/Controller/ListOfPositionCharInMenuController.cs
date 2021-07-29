using Code.Extension;
using Controller.Model;
using Interface;
using UniRx;


namespace Controller
{
    public sealed class ListOfPositionCharInMenuController : IInitialization, ICleanup
    {
        private readonly ListOfPositionCharInMenuModel _model;
        private readonly CommandManager _commandManager;
        private CompositeDisposable _subscriptions;

        public ListOfPositionCharInMenuController(ListOfPositionCharInMenuModel model,
            CommandManager commandManager)
        {
            _model = model;
            _commandManager = commandManager;
            _subscriptions = new CompositeDisposable();
        }

        public void Init()
        {
            _commandManager._characterWindowShowCommand.Subscribe(value =>
            {
                GlobalLinks.Player.Transform.Change(_model.Windows.RootCharacterSpawnPoint);
            }).AddTo(_subscriptions);
            _commandManager.ChangePlayer.Subscribe(value =>
            {
                value.Transform.Change(_model.Windows.RootCharacterSpawnPoint);
            }).AddTo(_subscriptions);
        }

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }
    }
}