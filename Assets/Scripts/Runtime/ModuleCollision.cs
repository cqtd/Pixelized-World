using UnityEngine;

namespace Pixelo
{
	public class ModuleCollision : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("EnemyBullet") || other.CompareTag("Enemy"))
			{
				BlockInstance block = GetComponent<BlockInstance>();
				Pawn pawn = transform.root.GetComponent<Pawn>();

				if (block.IsRoot())
				{
					other.GetComponent<Enemy>().OnDamage(transform.position);
					pawn.GameOver();
				}
				else
				{
					pawn.DetachModule(block);
					// other.gameObject.SetActive(false);
					other.GetComponent<Enemy>().OnDamage(transform.position);
				}
			}
		}
	}
}