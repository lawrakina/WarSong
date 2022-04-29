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
                _state = value;
                return;
                switch (_state){
                    case AbilityState.Ready:
                        if (value == AbilityState.Started) _state = value;
                        break;
                    case AbilityState.Started:
                        if (value == AbilityState.Canceled) _state = value;
                        break;
                    case AbilityState.InProgress:
                        if (value == AbilityState.Canceled) _state = value;
                        break;
                    case AbilityState.Completed:
                        break;
                    case AbilityState.Cooldown:
                        break;
                    case AbilityState.Canceled:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

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