using System.Collections;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
	[SerializeField]
	private	bool	onlyDeactivate = true;
	[SerializeField]
	private	float	destroyTime = 5;

	private IEnumerator Start()
	{
		while (destroyTime > 0)
		{
			destroyTime -= Time.deltaTime;

			yield return null;
		}

		if ( onlyDeactivate )
		{
			gameObject.SetActive(false);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}

