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
        private bool _illumination => _state == AbilityState.Started;
        private AbilityState _state = AbilityState.Ready;
        public Guid Id{ get; }
        public bool IsOn=> true;


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
        public AbilityState State{
            get => _state;
            set{
                Dbg.Log($"Ability on '{_cell.AbilityCellType}' change state from {_state} to {value}");
                if (_state == AbilityState.Started && value == AbilityState.InProgress)
                    _cooldownCurrentTime = 0.0f;

                if (_state == AbilityState.InProgress && value == AbilityState.Completed){
                    _state = AbilityState.Cooldown;
                    return;
                }
                
                _state = value;
            }
        }

        public void Execute(float deltaTime){
            if (_cooldownCurrentTime < _cooldownMaxTime){
                _cooldownCurrentTime += deltaTime;
                Recharge?.Invoke(_cooldownCurrentTime/_cooldownMaxTime);
            } else{
                if (_state == AbilityState.Cooldown){
                    _state = AbilityState.Ready;
                }
            }

            if (_illumination){
                //ToDo create illumination effect
            }
        }
    }
}