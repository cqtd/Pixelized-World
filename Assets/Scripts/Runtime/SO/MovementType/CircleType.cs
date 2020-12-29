using UnityEngine;

namespace Pixelo
{
	[CreateAssetMenu(menuName = "MovementType/Circle", fileName = "CircleType", order = 154)]
	public class CircleType : MovementType
	{
		private bool initialized;
		private Vector3 hiddenPosition;
		
		public override void Move(Transform target, Vector3 velocity)
		{
			if (!initialized)
			{
				hiddenPosition = target.transform.position;
				initialized = true;
			}
			
			// Radius.
			float radius = 2.5f;

			// How fast turn it can be.
			float rate = 4.0f;
			float z = Mathf.Sin(Time.time * rate) * radius;
			float x = Mathf.Cos(Time.time * rate) * radius;
			Vector3 localCirclePosition = new Vector3(x, 0, z);

			// Calculate the body position to move down.
			this.hiddenPosition += Time.deltaTime * velocity;
			
			// The final position is combined with two vectors.
			// One is its own position.
			// Another is the circle position.
			target.transform.position = this.hiddenPosition + localCirclePosition;
		}
	}
}