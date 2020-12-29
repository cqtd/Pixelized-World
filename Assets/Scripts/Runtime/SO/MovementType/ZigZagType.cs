using UnityEngine;

namespace Pixelo
{
	[CreateAssetMenu(menuName = "MovementType/ZigZag", fileName = "ZigZagType", order = 152)]
	public class ZigZagType : MovementType
	{
		public override void Move(Transform target, Vector3 velocity)
		{
			target.transform.position += Time.deltaTime * velocity;

			Vector3 pos = target.transform.position;

			// How fast turn it can be.
			float rate = 3.0f;

			// Calculate x variable to move zigzag.
			float x = Mathf.Cos(Time.time * rate);

			// Cos returns -1 ~ +1, so we should multiply a radius to get the final position.
			float radius = 3.0f;
			pos.x = x * radius;

			// Apply it. x variable from Cos, other variables from the velocity.
			target.transform.position = pos;
		}
	}
}