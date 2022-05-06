using System;
using Code.Data.Abilities;
using Code.Extension;
using UniRx;


namespace Code.UI.Fight{
    public class Ability : IExecute, IDisposable{
        private float _cooldownCurrentTime;
        private float _cooldownMaxTime;
        private AbilityCell _cell;
        public Action<float> OnRecharge;
        public Action<bool> OnSelected;
        private AbilityState _oldState;

        protected CompositeDisposable _subscriptions = new CompositeDisposable();
        public ReactiveProperty<AbilityState> State = new ReactiveProperty<AbilityState>();
        public Guid Id{ get; }
        public bool IsOn => true;
        public AbilityCell Cell => _cell;
        public float CostResource => Cell.Body.costResource;
        public AbilityTargetType AbilityTargetType => Cell.Body.abilityTargetType;
        public float TimeLagBeforeAction => Cell.Body.timeLagBeforeAction;
        public UiInfo UiInfo => Cell.Body.uiInfo;
        public float Distance => Cell.Body.distance;

        public Ability(AbilityCell abilityCell){
            _cell = abilityCell;
            _cooldownCurrentTime = abilityCell.Body.cooldown;
            _cooldownMaxTime = abilityCell.Body.cooldown;

            State.Subscribe(OnChangeState).AddTo(_subscriptions);
        }

        private void OnChangeState(AbilityState state){
            Dbg.Log($"Ability on '{_cell.AbilityCellType}' change state from {_oldState} to {State.Value}");
            if (_oldState == State.Value){
                Dbg.Log($"Old state == new state. Stop executing the method");
                return;
            }
            _oldState = State.Value;
            if (state == AbilityState.InProgress){
                _cooldownCurrentTime = 0.0f;
                _cooldownMaxTime = _cell.Body.cooldown;
            }

            if (state == AbilityState.Completed){
                State.Value = AbilityState.Cooldown;
            }

            if (state == AbilityState.Canceled){
                _cooldownMaxTime = 0.01f;
                State.Value = AbilityState.Ready;
            }

            OnSelected?.Invoke(state == AbilityState.Started);
        }

        public void Execute(float deltaTime){
            if (_cooldownCurrentTime < _cooldownMaxTime){
                _cooldownCurrentTime += deltaTime;
                OnRecharge?.Invoke(_cooldownCurrentTime / _cooldownMaxTime);
            } else{
                if (State.Value == AbilityState.Cooldown){
                    State.Value = AbilityState.Ready;
                }
            }
        }

        public void Dispose(){
            _subscriptions?.Dispose();
            State?.Dispose();
        }
    }
}