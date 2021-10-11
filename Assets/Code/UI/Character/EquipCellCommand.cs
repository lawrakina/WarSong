using Code.Data.Unit;
using UniRx;

namespace Code.UI.Character
{
    public class EquipCellCommand
    {
        private EquipCell _body;

        public EquipCell Body
        {
            get => _body;
            set => _body = value;
        }

        public ReactiveCommand<EquipCell> Command = new ReactiveCommand<EquipCell>();

        public EquipCellCommand(EquipCell body)
        {
            _body = body;
        }
    }
}