using UnityEngine;


namespace Pathfinding {

	[CreateAssetMenu(fileName = nameof(PathfindingConfig), menuName = "Configs/" + nameof(PathfindingConfig))]
	public class PathfindingConfig : ScriptableObject {
		public AstarPath AstarGrid => _astarGrid;
		public Vector3[] WaypointsPositions => _waypointsPositions;
		public Vector2Int PatrolSize => _patrolSize;
		public float BetweenWaypointsDelay => _betweenWaypointsDelay;
		public string WaypointsName => _waypointsName;

		[SerializeField] private AstarPath _astarGrid;
		[SerializeField] private Vector3[] _waypointsPositions;
		[SerializeField] private Vector2Int _patrolSize;
		[SerializeField] private float _betweenWaypointsDelay;
		[SerializeField] private string _waypointsName;
	}

}