using Models;
using TMPro;
using UnityEngine.UI;


namespace Gui.Battle
{
    public sealed class FightPanel : BasePanel
    {
        public Image HpFiel;
        public Image ResourceFiel;
        public TextMeshProUGUI HpLabel;
        private BattlePlayerModel _playerModel;

        private float CurrentHpValue
        {
            set
            {
                HpLabel.text = $"{value}/{MaxHpValue}";
                HpFiel.fillAmount = value/MaxHpValue;
            }
        }

        private float MaxHpValue { get; set; }

        public void Ctor(BattlePlayerModel playerModel)
        {
            _playerModel = playerModel;
            
            _playerModel.ChangeCurrentHp += f => { CurrentHpValue = f;};
            _playerModel.ChangeMaxHp += f => { MaxHpValue = f;};
        }
    }
}