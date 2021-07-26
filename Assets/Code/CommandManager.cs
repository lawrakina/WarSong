using UniRx;


namespace Code
{
    public class CommandManager : BaseController
    {
        public ReactiveCommand ShowAdventureWindow { get; } = new ReactiveCommand();
        public ReactiveCommand ShowCharacterWindow { get; } = new ReactiveCommand();
        public ReactiveCommand ShowTavernWindow { get; } = new ReactiveCommand();
        public ReactiveCommand ShowShopWindow { get; } = new ReactiveCommand();
        public ReactiveCommand ShowInventoryWindow { get; } = new ReactiveCommand();
    }
}