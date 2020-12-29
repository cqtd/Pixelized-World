using UnityEngine;

namespace Pixelo
{
	[CreateAssetMenu(menuName = "Block/Default Block", fileName = "BlockDefinition", order = 101)]
	public class BlockDefinition : ScriptableObject
	{
		public Color blockColor = default;
		public string blockName = default;
		public GameObject prefab = default;

	}
}