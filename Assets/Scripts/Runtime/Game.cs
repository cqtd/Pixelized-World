using System;
using System.Collections;
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
			StartCoroutine(ScoreByTime());
		}

		public Ship ship;
		public Score score;
		public BuffManager buff;

		private IEnumerator ScoreByTime()
		{
			while (ship.isAlive)
			{
				int add = buff.isFeverTime ? 1 : 2;
				score.lifeTimeScore.Value += add;
				
				yield return null;
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
	}

	[Serializable]
	public class BuffManager
	{
		public bool isFeverTime;
	}
}