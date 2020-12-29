using System.Collections;
using UnityEngine;

namespace Pixelo
{
	public class EnemySpawner : MonoBehaviour
	{
		public Enemy[] enemies;
		public Collider[] spawnArea;
		
		[SerializeField]
		float startDelay = 1.0f;
		
		[SerializeField]
		float intervalMin = 1.0f;

		[SerializeField]
		float intervalMax = 3.0f;
		
		[SerializeField]
		int countMin = 2;

		[SerializeField]
		int countMax = 5;
		private void OnEnable()
		{
			StartCoroutine(SpawnCoroutine());
		}

		IEnumerator SpawnCoroutine()
		{
			if (this.startDelay > 0.0f)
			{
				yield return new WaitForSeconds(this.startDelay);
			}

			while (true)
			{
				StartWave();

				// Wait for the next wave.
				float interval = Random.Range(this.intervalMin, this.intervalMax);
				yield return new WaitForSeconds(interval);
			}
		}

		private void StartWave()
		{
			// It determines how many enemies to be spawned on this wave.
			int count = Random.Range(this.countMin, this.countMax + 1);
			for (int i = 0; i < count; ++i)
			{
				// Pick one enemy prefab randomly.
				int enemyIndex = Random.Range(0, this.enemies.Length);

				// Instantiate it.
				Enemy enemy = GameObject.Instantiate(this.enemies[enemyIndex], transform, false);

				// Set its position.
				enemy.transform.position = GetRandomPosition();
				enemy.Initialize();
			}
		}
		
		Vector3 GetRandomPosition()
		{
			int index = Random.Range(0, spawnArea.Length);
			var bounds = spawnArea[index].bounds;
			
			float x = Random.Range(bounds.min.x, bounds.max.x);
			float y = Random.Range(bounds.min.y, bounds.max.y);
			float z = Random.Range(0, 0);

			return transform.position + new Vector3(x, y, z);
		}
	}
}