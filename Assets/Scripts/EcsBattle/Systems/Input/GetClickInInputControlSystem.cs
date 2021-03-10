using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Input
{
    public class GetClickInInputControlSystem : IEcsRunSystem
    {
        private EcsFilter<InputControlComponent, UnpressJoystickComponent> _input;
        private EcsFilter<PlayerComponent> _player;
        private EcsFilter<FightCameraComponent> _camera;

        public void Run()
        {
            foreach (var index in _input)
            {
                ref var joystick = ref _input.Get1(index);
                ref var lastState = ref _input.Get2(index);
                ref var entity = ref _input.GetEntity(index);

                //имитация нажатия кнопки. Время удержания меньше и смещения нет
                if (lastState.PressTime <= joystick.MaxPressTimeForClickButton &&
                    lastState.LastValueVector.sqrMagnitude <= joystick.MaxOffsetForClick.sqrMagnitude)
                {
                    foreach (var p in _player)
                    {
                        _player.GetEntity(p).Get<NeedAttackComponent>();
                    }

                    foreach (var c in _camera)
                    {
                        ref var entityCamera = ref _camera.GetEntity(c);
                        ref var camera = ref _camera.Get1(c);
                        
                        entityCamera.Get<TimerStopFollowingInPlayerComponent>().currentTime = 0.0f;
                        entityCamera.Get<TimerStopFollowingInPlayerComponent>().maxTime =
                            camera.maxTimeToStopFollowingInPlayer;
                    }

                    Dbg.Log($"joystick.Click");
                    entity.Del<UnpressJoystickComponent>();
                }
            }
        }
    }

    public struct TimerStopFollowingInPlayerComponent
    {
        public float currentTime;
        public float maxTime;
    }
}