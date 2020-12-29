using UnityEngine;

namespace Pixelo
{
	public class Enemy : MonoBehaviour
	{
		public MovementType type;
		private Vector3 direction;
		public float speed = 3f;
		public GameObject dieEffect;

		public void Initialize()
		{
			var player = GameObject.Find("Player");
			direction = (player.transform.position - transform.position).normalized;
		}

		private void Update()
		{
			type.Move(transform, direction * speed);
		}

		public void OnDamage(Vector3 collisionPoint)
		{
			Instantiate(dieEffect, collisionPoint, Quaternion.identity);
			
			Destroy(gameObject);
		}
	}
}