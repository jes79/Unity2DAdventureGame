using System.Collections;
using UnityEngine;

public class EnemyFlower : MonoBehaviour
{
	[SerializeField]
	private	float		attackRate = 2;
	private	float		currentTime = 0;
	private	Animator	animator;

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
	}

	private IEnumerator Start()
	{
		while ( true )
		{
			if ( Time.time - currentTime > attackRate )
			{
				animator.SetTrigger("onFire");

				currentTime = Time.time;
			}

			yield return null;
		}
	}
}

