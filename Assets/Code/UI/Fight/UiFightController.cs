using System.Collections.Generic;
using System.Linq;
using Code.Extension;
using Code.Fight;
using Code.Profile;
using Code.Profile.Models;
using UniRx;
using UnityEngine;


namespace Code.UI.Fight{
    public class UiFightController : BaseController, IExecute{
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private FightView _fightView;
        private InputControlModel _inputModel;
        private PlayerStatsModel _playerModel;
        private List<Ability> _abilities = new List<Ability>();

        public UiFightController(Transform placeForUi, ProfilePlayer profilePlayer){
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _inputModel = _profilePlayer.Models.InOutControlFightModel.InputControl;
            _playerModel = _profilePlayer.Models.InOutControlFightModel.PlayerStats;
            var inputSettings = _profilePlayer.Settings.FightInputData;

            _fightView =
                ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.FightView, _placeForUi, false);
            AddGameObjects(_fightView.GameObject);
            
            //Abilities
            foreach (var abilityCell in _profilePlayer.CurrentPlayer.UnitAbilities.ActiveAbilities){
                var ability = new Ability(abilityCell);
                var abilityButton =
                    _fightView.ListOfButtonAbilities.FirstOrDefault(x =>
                        x.AbilityCellType == abilityCell.AbilityCellType);
                if (abilityButton != null){
                    abilityButton.Init(ability, ActionOfAbility);
                    ability.Recharge += abilityButton.Recharge;
                }
                _abilities.Add(ability);
            }

            _playerModel.ChangeMaxHp += f => { _fightView.PlayerMaxHp = f;};
            _playerModel.ChangeCurrentHp += f => { _fightView.PlayerCurrentHp = f;};
            _playerModel.ChangeMaxResource += f => { _fightView.PlayerMaxResource = f;};
            _playerModel.ChangeCurrentResource += f => { _fightView.PlayerCurrentResource = f;};

            _profilePlayer.Models.FightModel.FightState.Subscribe(ShowUiControls).AddTo(_subscriptions);

            _inputModel.Joystick = (_fightView.Joystick);
            _inputModel.MaxOffsetForClick = inputSettings.MaxOffsetForClick;
            _inputModel.MaxOffsetForMovement = inputSettings.MaxOffsetForMovement;
            _inputModel.MaxPressTimeForClickButton = inputSettings.MaxPressTimeForClickButton;
        }

        private void ActionOfAbility(Ability ability){
            // Dbg.Log($"ActionOfAbility:{ability}");
            if (ability.IsOn){
                _inputModel.QueueOfAbilities.Enqueue(ability.OnAwake());
                // _inputModel.QueueOfAbilities.Enqueue(ability.Start());
            }
        }

        private void ShowUiControls(FightState state){
            if (state == FightState.Fight){
                _fightView.Show();
            } else{
                _fightView.Hide();
            }
        }

        public override void Dispose(){
            foreach (var ability in _abilities){
                ability.Recharge = null;
            }
            base.Dispose();
        }

        public void Execute(float deltaTime){
            foreach (var ability in _abilities){
                if (!ability.IsOn){
                    ability.Execute(deltaTime);
                }
            }
        }
    }
}