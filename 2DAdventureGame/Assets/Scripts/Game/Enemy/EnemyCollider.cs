using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
	private	EnemyBase enemyBase;

	private void Awake()
	{
		enemyBase = GetComponentInParent<EnemyBase>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( enemyBase.IsDie == true ) return;

		if ( collision.CompareTag("Player") )
		{
			collision.GetComponent<PlayerHP>().DecreaseHP();
		}
		else if ( collision.CompareTag("PlayerProjectile") )
		{
			enemyBase.OnDie();
			Destroy(collision.gameObject);
		}
	}
}

