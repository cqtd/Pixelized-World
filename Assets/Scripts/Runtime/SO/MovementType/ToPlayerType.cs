using UnityEngine;

namespace Pixelo
{
	[CreateAssetMenu(menuName = "MovementType/ToPlayer", fileName = "ToPlayerType", order = 153)]
	public class ToPlayerType : MovementType
	{
		public override void Move(Transform target, Vector3 velocity)
		{
			target.transform.position += Game.deltaTime * velocity;

		}
	}
}