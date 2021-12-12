using System.Collections;
using UnityEngine;


namespace Pathfinding {

	public class PathsController {
		public bool IsRandomPatrol => _waypoints.Length < 2;

		private readonly AIDestinationSetter _aiDestinationSetter;
		private Vector3 _startPosition;
		private Transform[] _waypoints;
		private int _currentTargetIndex;
		private bool _isDelayed;
		private PathfindingConfig _pathfindingConfig;
		private readonly System.Random _random;


		public PathsController(AIDestinationSetter aiDestinationSetter) {
			_aiDestinationSetter = aiDestinationSetter;
			_random = new System.Random();
		}
		
		public void StartPath(Transform[] waypoints, PathfindingConfig pathfindingConfig) {
			_waypoints = waypoints;
			_currentTargetIndex = 0;
			_pathfindingConfig = pathfindingConfig;
			_aiDestinationSetter.OnDestinationReached += OnWaypointReached;
			if (IsRandomPatrol) {
				_startPosition = _waypoints[0].position;
				_aiDestinationSetter.target = _waypoints[0];
			}
			_aiDestinationSetter.StartCoroutine(SetTarget());
		}

		private Vector3 GetRandomTargetPosition() {
			// var isReachable = false;
			var xPosition = _random.Next(-_pathfindingConfig.PatrolSize.x / 2, _pathfindingConfig.PatrolSize.x / 2);
			var yPosition = _random.Next(-_pathfindingConfig.PatrolSize.y / 2, _pathfindingConfig.PatrolSize.y / 2);
			var newPosition = AstarPath.active.GetNearest(_startPosition + new Vector3(xPosition, 0, yPosition), NNConstraint.Default).position;
			// while (!isReachable) {
			// 	xPosition = _random.Next(-_pathfindingConfig.PatrolSize.x / 2, _pathfindingConfig.PatrolSize.x / 2);
			// 	yPosition = _random.Next(-_pathfindingConfig.PatrolSize.y / 2, _pathfindingConfig.PatrolSize.y / 2);
			// 	var newPosition = _startPosition + new Vector3(xPosition, 0, yPosition);
			// 	var collisions = Physics.OverlapSphere(newPosition, 1, 1 << 6);
			// 	isReachable = collisions.Length == 0;
			// 	
			// }
			return newPosition;
		}

		private IEnumerator SetTarget(float delay = 0) {
			_isDelayed = true;
			yield return new WaitForSeconds(delay);
			
			_isDelayed = false;
			if (IsRandomPatrol) {
				_waypoints[0].position = GetRandomTargetPosition();
			} else {
				_aiDestinationSetter.target = _waypoints[_currentTargetIndex];
			}
		}

		private void OnWaypointReached() {
			if (_isDelayed)
				return;
			
			_currentTargetIndex = _currentTargetIndex < _waypoints.Length - 1 ? _currentTargetIndex + 1 : 0;
			_aiDestinationSetter.StartCoroutine(SetTarget(_pathfindingConfig.BetweenWaypointsDelay));
		}

	}

}