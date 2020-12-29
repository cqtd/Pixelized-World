using System.Collections.Generic;
using UnityEngine;

namespace Pixelo
{
	public class Module : MonoBehaviour
	{
		[SerializeField] 
		protected BlockDefinition m_block = default;
		
		public Module parent = default;
		public List<Module> children = default;

		protected SpriteRenderer _renderer = default;
		
		protected virtual void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

		protected virtual  void Start()
		{
			_renderer.color = m_block.blockColor;
		}

		public bool IsRoot()
		{
			return parent == null;
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Enemy"))
			{
				Module block = GetComponent<Module>();
				Enemy enemy = other.GetComponent<Enemy>();

				if (enemy.isDead)
				{
					return;
				}
				
				if (block.IsRoot())
				{
					enemy.OnDamage(transform.position);
					Game.instance.ship.GameOver();
				}
				else
				{
					Game.instance.ship.DetachModule(block);
					enemy.OnDamage(transform.position);
				}
			}
			
			else if (other.CompareTag("EnemyBullet"))
			{
				Module block = GetComponent<Module>();
				Enemy enemy = other.GetComponent<Enemy>();

				if (enemy.isDead)
				{
					return;
				}
				
				if (block.IsRoot())
				{
					enemy.OnDamage(transform.position);
					Game.instance.ship.GameOver();
				}
				else
				{
					Game.instance.ship.DetachModule(block);
					enemy.OnDamage(transform.position);
				}
			}
		}
	}
}