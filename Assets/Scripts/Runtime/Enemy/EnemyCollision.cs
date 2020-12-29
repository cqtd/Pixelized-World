using System;
using UnityEngine;

namespace Pixelo
{
	public class EnemyCollision : MonoBehaviour
	{
		// public LayerMask layermask;
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("PlayerBullet"))
			{
				OnDamage(other.transform.position);

				// Destroy bullet.
				Destroy(other.gameObject);
			} else if (other.CompareTag("Player"))
			{
				// Pawn pawn = other.GetComponent<Pawn>();
				// var hit = pawn.GetClosestModule(transform.position);
				// Debug.LogError(hit.gameObject, hit.gameObject);
			} 
		}

		private void OnDamage(Vector3 collisionPoint)
		{
			GetComponentInParent<Enemy>().OnDamage(collisionPoint);
		}
	}
}