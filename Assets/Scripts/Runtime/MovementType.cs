using UnityEngine;

namespace Pixelo
{
	public abstract class MovementType : ScriptableObject
	{
		public abstract void Move(Transform target, Vector3 velocity);
	}
}