using UnityEngine;

public class PropsGoal : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( collision.CompareTag("Player") )
		{
			collision.GetComponent<PlayerController>().LevelComplete();
		}
	}
}

