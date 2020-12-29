using System;

namespace Pixelo
{
	[Serializable]
	public struct DefaultBlockSlot
	{
		public int x;
		public int y;
		public BlockDefinition definition;

		public DefaultBlockSlot[] children;
	}
}