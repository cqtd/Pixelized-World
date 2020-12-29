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
		}

		public Ship ship;

#if UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		public static void ResetDomain()
		{
			instance = null;
		}
#endif
	}
}