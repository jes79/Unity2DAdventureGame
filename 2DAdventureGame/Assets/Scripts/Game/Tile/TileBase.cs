using System.Collections;
using UnityEngine;

public class TileBase : MonoBehaviour
{
	[SerializeField]
	private	bool	canBounce = false;	// Bounce 가능 여부
	private	float	startPositionY;		// 타일의 최초 y 위치

	// 타일과 플레이어가 충돌했는지 체크 (일정시간동안 다시 충돌체크를 하지 않도록)
	public	bool	IsHit { private set; get; } = false;

	private void Awake()
	{
		startPositionY = transform.position.y;
	}

	public virtual void UpdateCollision()
	{
		if ( canBounce == true )
		{
			IsHit = true;

			StartCoroutine(nameof(OnBounce));
		}
	}

	private IEnumerator OnBounce()
	{
		float maxBounceAmount = 0.35f;	// 타일이 충돌해 올라가는 최대 높이

		yield return StartCoroutine(MoveToY(startPositionY, startPositionY + maxBounceAmount));

		yield return StartCoroutine(MoveToY(startPositionY + maxBounceAmount, startPositionY));

		IsHit = false;
	}

	private IEnumerator MoveToY(float start, float end)
	{
		float percent	 = 0;
		float bounceTime = 0.2f;

		while ( percent < 1 )
		{
			percent += Time.deltaTime / bounceTime;

			Vector3 position = transform.position;
			position.y = Mathf.Lerp(start, end, percent);
			transform.position = position;

			yield return null;
		}
	}
}

