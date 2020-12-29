using UnityEngine;

namespace Pixelo
{
	[CreateAssetMenu(menuName = "MovementType/Straight", fileName = "StraightType", order = 151)]
	public class StraightType : MovementType
	{
		public override void Move(Transform target, Vector3 velocity)
		{
			target.transform.position += Game.deltaTime * velocity;
		}
	}
}