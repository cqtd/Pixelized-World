using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelo
{
	public class AttackableModule : Module
	{
		[SerializeField]
		protected Bullet prefabBullet;

		[Header("Interval between each fire")]
		[SerializeField]
		protected float interval = 1.0f;

		[Header("Bullet speed")]
		[SerializeField]
		protected float speed = 40.0f;

		[Header("Fire direction")]
		[SerializeField]
		protected Vector3 direction = new Vector3(0, 0, 1);
		
		[SerializeField]
		protected bool toPlayer = false;

		[Header("Missile")]
		// If this sets true, bullets will be shoot towards to enemies.
		[SerializeField]
		protected bool isMissile = false;

		public List<GameObject> targets;

		private void OnEnable()
		{
			targets = new List<GameObject>();
			
			StartCoroutine(FireLoop());
		}

		IEnumerator FireLoop()
		{
			GameObject player = Game.instance.ship.gameObject;

			while (true)
			{
				yield return new WaitForSeconds(this.interval);

				float min = float.MaxValue;
				Transform closest = null;
				foreach (GameObject target in targets)
				{
					if (target != null)
					{
						var value = Vector3.SqrMagnitude(target.transform.position - transform.position);
						if (value < min)
						{
							min = value;
							closest = target.transform;
						}
					}
				}

				if (closest != null)
				{
					// Instantiate a bullet, position.
					Bullet bullet = GameObject.Instantiate(this.prefabBullet);
					bullet.transform.position = transform.position;
					bullet.transform.rotation = transform.rotation;
					
					bullet.SetTarget(closest);
					bullet.SetSpeed(10);
				
					// Set bullet properties.
					// bullet.SetDirection((closest.transform.position-transform.position).normalized);
					// bullet.SetSpeed(this.speed);

					isMissile = true;

					// Calculate fire direction.
					if (this.toPlayer)
					{
						if (player != null)
						{
							this.direction = (player.transform.position - transform.position).normalized;
							bullet.transform.rotation = Quaternion.LookRotation(this.direction);
							// bullet.SetDirection(this.direction);
						}
					}

					// Missile.
					if (this.isMissile)
					{
						// bullet.SetFollowTarget();
					}
				}
			}
		}

		public void AddTarget(GameObject target)
		{
			targets.Add(target);
		}
	}
}