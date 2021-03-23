using EcsBattle.Components;
using Extension;
using Guirao.UltimateTextDamage;
using Interface;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Attacks
{
    public sealed class ShowUiMessageByDamageSystem : IEcsRunSystem
    {
        private IFightCamera _camera;
        private EcsFilter<UnitComponent, NeedShowUiEventComponent> _units;
        // private EcsFilter<PlayerComponent, NeedShowUiEventComponent> _player;

        public void Run()
        {
            foreach (var i in _units)
            {
                ref var entity = ref _units.GetEntity(i);
                ref var unit = ref _units.Get1(i);
                ref var message = ref _units.Get2(i);

                // Dbg.Log($"ShowUiMessageTextSystem.{message.pointsDamage}");
                _camera.UiTextManager.Add(message.pointsDamage.ToStringScientific(),
                    unit.rootTransform.position + unit.vision.offsetHead, "default");

                entity.Del<NeedShowUiEventComponent>();
            }
            // foreach (var i in _player)
            // {
            //     ref var entity = ref _player.GetEntity(i);
            //     ref var unit = ref _player.Get1(i);
            //     ref var message = ref _player.Get2(i);
            //
            //     Dbg.Log($"ShowUiMessageTextSystem.{message.pointsDamage}");
            //     _camera.UiTextManager.Add(message.pointsDamage.ToStringScientific(),
            //         unit.rootTransform.position + unit.unitVision.offsetHead, "default");
            //
            //     entity.Del<NeedShowUiEventComponent>();
            // }
        }
    }
}