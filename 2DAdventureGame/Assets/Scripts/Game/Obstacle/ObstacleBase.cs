using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
	[SerializeField]
	private	bool	isInstantDeath = true;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 장애물은 플레이어와 충돌했을 때만 로직 처리
		if ( !collision.CompareTag("Player") ) return;

		if ( isInstantDeath )
		{
			collision.GetComponent<PlayerController>().OnDie();
		}
		else
		{
			collision.GetComponent<PlayerHP>().DecreaseHP();
		}
	}
}

