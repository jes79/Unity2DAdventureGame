using System.Collections;
using UnityEngine;

public enum RespawnType { AfterTime = 0, PlayerDead }

public class PlatformDrop : PlatformBase
{
	[SerializeField]
	private	RespawnType		respawnType = RespawnType.AfterTime;
	[SerializeField]
	private	float			respawnTime = 2;

	private	BoxCollider2D	boxCollider2D;
	private	Rigidbody2D		rigid2D;
	private	Vector3			originPosition;

	private void Awake()
	{
		boxCollider2D	= GetComponent<BoxCollider2D>();
		rigid2D			= GetComponent<Rigidbody2D>();
		originPosition	= transform.position;
	}

	public override void UpdateCollision(GameObject other)
	{
		if ( IsHit == true ) return;

		IsHit = true;

		StartCoroutine(nameof(Process));
	}

	private IEnumerator Process()
	{
		// 플레이어와 발판이 충돌했을 때 발판이 흔들리는 애니메이션 재생
		yield return StartCoroutine(nameof(OnShake));

		// 발판이 아래로 추락
		OnDrop();

		// 재생성이 가능한 발판이면 respawnTime 시간 이후 재생성
		if ( respawnType == RespawnType.AfterTime )
		{
			StartCoroutine(nameof(OnRespawn));
		}
		else
		{
			Destroy(gameObject, respawnTime);
		}
	}

	private IEnumerator OnShake()
	{
		float percent = 0;
		float shakeAngle = 5;
		float shakeSpeed = 10;
		float shakeTime = 1.5f;

		while ( percent < 1 )
		{
			percent += Time.deltaTime / shakeTime;

			float z = Mathf.Lerp(-shakeAngle, shakeAngle, Mathf.PingPong(Time.time * shakeSpeed, 1));
			transform.rotation = Quaternion.Euler(0, 0, z);

			yield return null;
		}

		transform.rotation = Quaternion.identity;
	}

	private void OnDrop()
	{
		boxCollider2D.enabled	= false;
		rigid2D.isKinematic		= false;
	}

	private IEnumerator OnRespawn()
	{
		yield return new WaitForSeconds(respawnTime);

		IsHit = false;

		transform.position		= originPosition;
		boxCollider2D.enabled	= true;
		rigid2D.isKinematic		= true;
		rigid2D.velocity		= Vector2.zero;
	}
}

