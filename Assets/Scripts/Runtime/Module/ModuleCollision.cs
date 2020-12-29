using UnityEngine;

namespace Pixelo
{
	public class ModuleCollision : MonoBehaviour
	{
		// private void OnTriggerEnter2D(Collider2D other)
		// {
		// 	if (other.CompareTag("EnemyBullet") || other.CompareTag("Enemy"))
		// 	{
		// 		Module block = GetComponent<Module>();
		// 		Enemy enemy = other.GetComponent<Enemy>();
		//
		// 		if (enemy.isDead)
		// 		{
		// 			return;
		// 		}
		// 		
		// 		if (block.IsRoot())
		// 		{
		// 			enemy.OnDamage(transform.position);
		// 			Game.instance.ship.GameOver();
		// 		}
		// 		else
		// 		{
		// 			Game.instance.ship.DetachModule(block);
		// 			enemy.OnDamage(transform.position);
		// 		}
		// 	}
		// }
	}
}