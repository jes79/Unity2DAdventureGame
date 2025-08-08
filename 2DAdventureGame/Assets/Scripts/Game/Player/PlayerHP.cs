using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
	[SerializeField]
	private	UIPlayerData		uiPlayerData;

	[SerializeField]
	private	int					max = 3;					// 최대 체력
	private	int					current;					// 현재 체력
	
	private	PlayerController	playerController;
	private	SpriteRenderer		spriteRenderer;				// 플레이어 피격 시 색상 변경을 위해
	private	Color				originColor;				// 플레이어의 초기 색상

	private	float				invincibilityTime = 0;		// 무적 지속시간
	private	bool				isInvincibility = false;	// 무적 여부

	private void Awake()
	{
		current			 = max;
		playerController = GetComponent<PlayerController>();
		spriteRenderer	 = GetComponentInChildren<SpriteRenderer>();
		originColor		 = spriteRenderer.color;
	}

	public void DecreaseHP()
	{
		// 무적 상태일 때는 체력이 감소하지 않는다
		if ( isInvincibility == true ) return;

		// 체력이 감소하면 일정시간동안 무적 상태
		OnInvincibility(1);

		if ( current > 1 )
		{
			current --;
			uiPlayerData.SetHP(current, false);
		}
		else
		{
			playerController.OnDie();
		}
	}

	public void IncreaseHP()
	{
		if ( current < max )
		{
			uiPlayerData.SetHP(current, true);
			current ++;
		}
	}

	public void OnInvincibility(float time)
	{
		if ( isInvincibility == true )
		{
			invincibilityTime += time;
		}
		else
		{
			invincibilityTime = time;
			StartCoroutine(nameof(Invincibility));
		}
	}

	private IEnumerator Invincibility()
	{
		isInvincibility = true;

		float blinkSpeed = 10;

		while ( invincibilityTime > 0 )
		{
			invincibilityTime -= Time.deltaTime;

			Color color = spriteRenderer.color;
			color.a		= Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time * blinkSpeed, 1));
			spriteRenderer.color = color;

			yield return null;
		}

		spriteRenderer.color = originColor;
		isInvincibility		 = false;
	}
}

