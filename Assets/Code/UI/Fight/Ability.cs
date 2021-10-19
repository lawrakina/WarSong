using System;
using Code.Data.Abilities;


namespace Code.UI.Fight{
    public class Ability{
        private bool _isEnable;
        private float _cooldownCurrentTime;
        private float _cooldownMaxTime;
        private AbilityCell _cell;
        
        public Action<float> Recharge;

        public Ability(AbilityCell abilityCell){
            _cell = abilityCell;
            _isEnable = true;
            _cooldownCurrentTime = abilityCell.Body.cooldown;
            _cooldownMaxTime = abilityCell.Body.cooldown;
        }

        public AbilityCell Cell => _cell;
        public bool IsEnable => !(_cooldownCurrentTime < _cooldownMaxTime);

        public void OnRecharge(float deltaTime){
            _cooldownCurrentTime += deltaTime;
            Recharge?.Invoke(_cooldownCurrentTime/_cooldownMaxTime);
        }

        public Ability Start(){
            _cooldownCurrentTime = 0.0f;
            return this;
        }
    }
}