using System;
using UniRx;
using UnityEngine;

namespace Pixelo
{
	[DefaultExecutionOrder(Constant.Order.GAME)]
	public class Game : MonoBehaviour
	{
		public static Game instance = default;

		private void Awake()
		{
			instance = this;

			Application.targetFrameRate = 60;
			Screen.fullScreen = true;
		}

		private void OnEnable()
		{
			ship.onAttackEnemy += score.OnKillEnemy;
			ship.onAttackEnemy += experience.OnKillEnemy;
		}

		public static float deltaTime {
			get
			{
				if (instance.isPaused)
				{
					return 0f;
				}
				else
				{
					return Time.deltaTime;
				}
			}
		}
		
		

		public static float time {
			get
			{
				if (instance.isPaused)
				{
					return instance.lastTime;
				}
				else
				{
					return Time.time;
				}
			}
		}

		public bool IsPaused {
			get
			{
				return isPaused;
			}
		}
		private bool isPaused;
		private float lastTime;

		public void Pause(bool pause)
		{
			if (pause)
			{
				// Time.timeScale = 00f;
				isPaused = true;
				lastTime = Time.time;
			}
			else
			{
				// Time.timeScale = 1f;
				isPaused = false;
			}
		}

		public Ship ship;
		public Score score;
		public BuffManager buff;
		public Experience experience;

		public Action onGameOver;
		public Action onDashCooldown;

		private void Update()
		{
			if (ship.isAlive && !isPaused)
			{
				int add = buff.isFeverTime ? 1 : 2;
				score.lifeTimeScore.Value += add;
			}
		}

#if UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		public static void ResetDomain()
		{
			instance = null;
		}
#endif
	}

	[Serializable]
	public class Score
	{
		public ReactiveProperty<int> lifeTimeScore;
		public ReactiveProperty<int> killScore;

		public int Total {
			get
			{
				return lifeTimeScore.Value + killScore.Value;
			}
		}

		public void OnKillEnemy(EnemyDefinition enemy)
		{
			killScore.Value = enemy.score;
		}
	}

	[Serializable]
	public class BuffManager
	{
		public bool isFeverTime;
	}

	[Serializable]
	public class Experience
	{
		public int experience = 0;
		public int maxExperience = 80;
		public int level = 1;

		public Action onLevelUp;
		
		public void OnKillEnemy(EnemyDefinition enemy)
		{
			experience += enemy.experience;

			if (experience >= maxExperience)
			{
				experience -= maxExperience;
				level += 1;

				int a = (int) Mathf.Pow(1.71f, level) * 100 ;
				maxExperience += a;
				
				onLevelUp.Invoke();
			}
		}
	}
}