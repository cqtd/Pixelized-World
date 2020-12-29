using UnityEngine;

namespace Pixelo
{
	[CreateAssetMenu(menuName = "Enemy/Enemy", fileName = "EnemyDefinition", order = 170)]
	public class EnemyDefinition : ScriptableObject
	{
		public int score = 100;
		public int experience = 17;
	}
}