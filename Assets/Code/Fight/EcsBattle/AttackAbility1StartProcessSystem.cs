using Code.Fight.EcsBattle.Input;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle {

	public class AttackAbility1StartProcessSystem : IEcsRunSystem {
		private EcsFilter<StartKnockbackAbilityComponent, PermissionForKnockbackAbilityAllowedComponent> _filter;

		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var entity = ref _filter.GetEntity(i);

				entity.Get<NeedFindTargetComponent>();
			}
		}
	}

}