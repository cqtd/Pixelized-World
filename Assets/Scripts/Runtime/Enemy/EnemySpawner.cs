using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelo
{
	[DefaultExecutionOrder(Constant.Order.ENEMY_SPAWNER)]
	public class EnemySpawner : MonoBehaviour
	{
		public Enemy[] enemies;
		public Collider[] spawnArea;

		[SerializeField] private float startDelay = 1.0f;
		[SerializeField] private float intervalMin = 1.0f;
		[SerializeField] private float intervalMax = 3.0f;
		[SerializeField] private int countMin = 2;
		[SerializeField] private int countMax = 5;

		private void OnEnable()
		{
			instances = new List<Enemy>();

			Enemy.onEnemySpawn += OnEnemySpawned;
			Enemy.onEnemyDeath += OnEnemyDeath;

			StartCoroutine(SpawnCoroutine());
		}

		private void OnEnemySpawned(Enemy enemy)
		{
			instances.Add(enemy);
		}

		private void OnEnemyDeath(Enemy enemy)
		{
			instances.Remove(enemy);
		}

		private List<Enemy> instances;

		private IEnumerator SpawnCoroutine()
		{
			if (startDelay > 0.0f)
			{
				yield return new WaitForSeconds(startDelay);
			}

			while (true)
			{
				StartWave();

				// Wait for the next wave.
				float interval = Random.Range(intervalMin, intervalMax);
				yield return new WaitForSeconds(interval);
			}
		}

		private void StartWave()
		{
			// It determines how many enemies to be spawned on this wave.
			int count = Random.Range(countMin, countMax + 1);
			for (int i = 0; i < count; ++i)
			{
				// Pick one enemy prefab randomly.
				int enemyIndex = Random.Range(0, enemies.Length);

				// Instantiate it.
				Enemy enemy = Instantiate(enemies[enemyIndex], transform, false);

				// Set its position.
				enemy.transform.position = GetRandomPosition();
				enemy.Initialize();
			}
		}

		private Vector3 GetRandomPosition()
		{
			int index = Random.Range(0, spawnArea.Length);
			Bounds bounds = spawnArea[index].bounds;

			float x = Random.Range(bounds.min.x, bounds.max.x);
			float y = Random.Range(bounds.min.y, bounds.max.y);
			float z = Random.Range(0, 0);

			return transform.position + new Vector3(x, y, z);
		}
	}
}