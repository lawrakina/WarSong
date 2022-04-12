using System;
using Code.Data.Abilities;
using Code.Extension;


namespace Code.UI.Fight{
    public class Ability: IExecute{
        // private bool _isEnable;
        private float _cooldownCurrentTime;
        private float _cooldownMaxTime;
        private AbilityCell _cell;
        
        public Action<float> Recharge;
        private bool _illumination;
        public Guid Id{ get; }
        public bool IsOn=> !(_cooldownCurrentTime < _cooldownMaxTime);


        public Ability(AbilityCell abilityCell){
            _cell = abilityCell;
            // _isEnable = true;
            _cooldownCurrentTime = abilityCell.Body.cooldown;
            _cooldownMaxTime = abilityCell.Body.cooldown;
        }

        public AbilityCell Cell => _cell;
        public float CostResource => Cell.Body.costResource;
        public AbilityTargetType AbilityTargetType => Cell.Body.abilityTargetType;
        public float TimeLagBeforeAction => Cell.Body.timeLagBeforeAction;
        public UiInfo UiInfo => Cell.Body.uiInfo;
        public float Distance => Cell.Body.distance;

        public Ability StartReload(){
            _cooldownCurrentTime = 0.0f;
            return this;
        }

        public Ability OnAwake(){
            _illumination = true;
            return this;
        }

        public void Execute(float deltaTime){
            if (_cooldownCurrentTime < _cooldownMaxTime){
                Dbg.Log($"111");
                _cooldownCurrentTime += deltaTime;
                Recharge?.Invoke(_cooldownCurrentTime/_cooldownMaxTime);
            }

            if (_illumination){
                //ToDo create illumination effect
            }
        }
    }
}