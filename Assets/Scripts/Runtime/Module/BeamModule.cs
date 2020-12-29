using System.Collections;
using UnityEngine;

namespace Pixelo
{
	public class BeamModule : AttackableModule
	{
		public int rayCount = 1;
		public float rayRadius = 1;
		public float _interval = 2f;

		private LineRenderer ren1;
		private LineRenderer ren2;
		
		private void OnEnable()
		{
			if (ren1 == null)
			{
				ren1 = new GameObject("Renderer1").AddComponent<LineRenderer>();
				ren1.transform.parent = transform;
				ren1.transform.localPosition = Vector3.zero;
				ren1.transform.localRotation = Quaternion.identity;
				ren1.positionCount = 2;
				ren1.startWidth = (0.02f);
				ren1.endWidth = (0.02f);
				ren1.startColor = Color.white;
				ren1.endColor = Color.white;
				ren1.material = new Material(Shader.Find("Standard"));
				// ren1.useWorldSpace = true;
			}
			
			if (ren2 == null)
			{
				ren2 = new GameObject("Renderer2").AddComponent<LineRenderer>();
				ren2.transform.parent = transform;
				ren2.transform.localPosition = Vector3.zero;
				ren2.transform.localRotation = Quaternion.identity;
				ren2.positionCount = 2;
				ren2.startWidth = (0.02f);
				ren2.endWidth = (0.02f);
				ren2.startColor = Color.white;
				ren2.endColor = Color.white;
				ren1.material = new Material(Shader.Find("Standard"));
				// ren2.useWorldSpace = true;
			}
			
			StartCoroutine(FireCoroutine());
		}

		IEnumerator FireCoroutine()
		{
			while (true)
			{
				var circles = Physics2D.OverlapCircleAll(transform.position, rayRadius);

				Collider2D enemy1 = null;
				Collider2D enemy2 = null;
				
				foreach (Collider2D found in circles)
				{
					if (enemy1 == null)
					{
						if (found.CompareTag("Enemy"))
						{
							enemy1= found;
							continue;
						}
					}
					else
					{
						if (found.CompareTag("Enemy"))
						{
							enemy2 = found;
							break;
						}
					}
				}
				
				ren1.gameObject.SetActive(true);
				ren2.gameObject.SetActive(true);

				if (enemy1 != null)
				{
					ren1.SetPosition(0, transform.position);
					ren1.SetPosition(1, enemy1.transform.position);

					var enemy = enemy1.GetComponent<Enemy>();
					if (enemy != null)
					{
						Game.instance.ship.onAttackEnemy?.Invoke(enemy.definition);
					}
				}
				
				if (enemy2 != null)
				{
					ren2.SetPosition(0, transform.position);
					ren2.SetPosition(1, enemy2.transform.position);
					
					var enemy = enemy2.GetComponent<Enemy>();
					if (enemy != null)
					{
						Game.instance.ship.onAttackEnemy?.Invoke(enemy.definition);
					}
				}

				yield return new WaitForSeconds(0.1f);
				
				ren1.gameObject.SetActive(false);
				ren2.gameObject.SetActive(false);
				
				yield return new WaitForSeconds(_interval);
			}
		}
	}
}