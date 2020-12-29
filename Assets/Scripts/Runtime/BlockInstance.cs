using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelo
{
	public class BlockInstance : MonoBehaviour
	{
		[SerializeField] private BlockDefinition m_block = default;

		public BlockInstance parent = default;
		public List<BlockInstance> children = default;

		private SpriteRenderer _renderer = default;
		
		[SerializeField]
		Bullet prefabBullet;

		[Header("Interval between each fire")]
		[SerializeField]
		float interval = 1.0f;

		[Header("Bullet speed")]
		[SerializeField]
		float speed = 40.0f;

		[Header("Fire direction")]
		[SerializeField]
		Vector3 direction = new Vector3(0, 0, 1);
		
		[SerializeField]
		bool toPlayer = false;

		[Header("Missile")]
		// If this sets true, bullets will be shoot towards to enemies.
		[SerializeField]
		bool isMissile = false;
		private void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

		private void Start()
		{
			_renderer.color = m_block.blockColor;
		}

		public bool IsRoot()
		{
			return parent == null;
		}

		private void OnEnable()
		{
			// StartCoroutine(FireLoop());
		}
		
		IEnumerator FireLoop()
		{
			GameObject player = GameObject.Find("Player");

			while (true)
			{
				yield return new WaitForSeconds(this.interval);

				// Instantiate a bullet, position.
				Bullet bullet = GameObject.Instantiate(this.prefabBullet);
				bullet.transform.position = transform.position;
				bullet.transform.rotation = transform.rotation;

				// Set bullet properties.
				bullet.SetDirection(transform.forward);
				bullet.SetSpeed(this.speed);

				// Calculate fire direction.
				if (this.toPlayer)
				{
					if (player != null)
					{
						this.direction = (player.transform.position - transform.position).normalized;
						bullet.transform.rotation = Quaternion.LookRotation(this.direction);
						bullet.SetDirection(this.direction);
					}
				}

				// Missile.
				if (this.isMissile)
				{
					bullet.SetFollowTarget();
				}
			}
		}
	}
}