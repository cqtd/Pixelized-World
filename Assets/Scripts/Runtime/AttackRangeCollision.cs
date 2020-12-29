using System;
using UnityEngine;

namespace Pixelo
{
	public class AttackRangeCollision : MonoBehaviour
	{
		private CircleCollider2D _collider = default;
		
		private void Awake()
		{
			_collider = GetComponent<CircleCollider2D>();
		}

		public void SetRadius(float radius)
		{
			_collider.radius = radius;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			transform.parent.GetComponent<AttackableModule>().AddTarget(other.gameObject);
		}
	}
}