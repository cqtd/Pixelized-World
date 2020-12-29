using System.Collections;
using UnityEngine;

namespace Pixelo
{
	public class Enemy : MonoBehaviour
	{
		public MovementType type;
		private Vector3 direction;
		public float speed = 3f;
		public GameObject dieEffect;

		[SerializeField] private float m_interval = 1.0f;

		public bool isDead;

		public void Initialize()
		{
			Ship player = Game.instance.ship;
			direction = (player.transform.position - transform.position).normalized;
		}

		private void Update()
		{
			type.Move(transform, direction * speed);
		}

		public void OnDamage(Vector3 collisionPoint)
		{
			Instantiate(dieEffect, collisionPoint, Quaternion.identity);

			isDead = true;
			Destroy(gameObject);
		}

		IEnumerator FireCoroutine()
		{
			while (true)
			{
				
				
				
				yield return new WaitForSeconds(m_interval);
			}
		}
	}
}