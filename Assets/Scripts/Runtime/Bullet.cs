using System;
using UnityEngine;

namespace Pixelo
{
	public class Bullet : MonoBehaviour
	{
		private Transform _target;
		private float _speed;

		public void SetTarget(Transform target)
		{
			_target = target;
		}

		public void SetSpeed(float speed)
		{
			this._speed = speed;
		}

		private void Update()
		{
			if (_target == null)
			{
				Destroy(gameObject);
			}
			
			Vector3 velocity = (_target.position - transform.position).normalized * _speed;
			transform.position += Game.deltaTime * velocity;
		}
	}
}