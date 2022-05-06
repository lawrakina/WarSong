using System;
using Code.Data.Unit;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Settings;
using Code.GameCamera;
using Code.Profile.Models;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Output{
    public class DisplayEffectsS : IEcsRunSystem{
        private InOutControlFightModel _model;
        private BattleCamera _camera;
        private EcsFilter<PlayerTag, UnitC, ChangeHpC> _playerHealths;
        private EcsFilter<PlayerTag, UnitC, ChangeResourceC> _playerResources;
        private EcsFilter<UnitC, UiEnemyHealthBarC> _updateHealthBar;
        private EcsFilter<NeedShowUiEventC> _showUiDamage;

        public void Run(){
            foreach (var i in _playerHealths){
                ref var entity = ref _playerHealths.GetEntity(i);
                ref var unit = ref _playerHealths.Get2(i);

                _model.PlayerStats.CurrentHp = (int) unit.Health.CurrentHp;
                // _model.PlayerStats.MaxHp = (int) unit.Health.MaxHp;
                entity.Del<ChangeHpC>();
            }
            foreach (var i in _playerResources){
                ref var entity = ref _playerResources.GetEntity(i);
                ref var unit = ref _playerResources.Get2(i);

                _model.PlayerStats.CurrentResource = (int) unit.Resource.ResourceBaseValue;
                entity.Del<ChangeResourceC>();
            }

            foreach (var i in _updateHealthBar){
                ref var enemyEntity = ref _updateHealthBar.GetEntity(i);
                var maxHp = enemyEntity.Get<UnitC>().Health.MaxHp;
                var currentHp = enemyEntity.Get<UnitC>().Health.CurrentHp;
                var enemyHealthBar = enemyEntity.Get<UiEnemyHealthBarC>().value;
                enemyHealthBar.ChangeValue(currentHp, maxHp);
            }

            foreach (var i in _showUiDamage){
                ref var entity = ref _showUiDamage.GetEntity(i);
                ref var ui = ref _showUiDamage.Get1(i);

                switch (ui.DamageType){
                    case DamageType.Critical:
                        _camera.UiTextManager.Show(((int) ui.PointsDamage).ToString(), ui.Position, $"critical", true);
                        break;
                    case DamageType.Default:
                        _camera.UiTextManager.Show(((int) ui.PointsDamage).ToString(), ui.Position, $"default", true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                entity.Del<NeedShowUiEventC>();
            }
        }
    }
}