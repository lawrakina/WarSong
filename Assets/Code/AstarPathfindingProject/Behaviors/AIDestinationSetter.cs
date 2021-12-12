using System;
using UnityEngine;
using System.Collections;
using Codice.Client.GameUI.Explorer;


namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		private const float TARGET_REACHED_SQR_MAGNITUDE_TRESHOLD = .2f;
		
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		// IAstarAI ai;
		public AIPath AIPath { get => _ai; }
		public Action OnDestinationReached;
		
		private AIPath _ai;
		private bool DestinationReached {
			get {
				if (Equals(target, null))
					return false;

				return (target.position - transform.position).sqrMagnitude < TARGET_REACHED_SQR_MAGNITUDE_TRESHOLD;
			}
		} 
		private PathsController _pathsController;


		void OnEnable () {
			// ai = GetComponent<IAstarAI>();
			_ai = GetComponent<AIPath>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (_ai != null) _ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (_ai != null) _ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {
			if (target != null && _ai != null) {
				if (DestinationReached)
					OnDestinationReached.Invoke();
				_ai.destination = target.position;
			}
		}
	}
}
