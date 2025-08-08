using UnityEngine;

public class MovementEffects : MonoBehaviour
{
	private	MovementRigidbody2D				movement;

	// 플레이어가 이동할 때 발 밑에 나오는 이펙트
	[SerializeField]
	private	ParticleSystem					footStepEffect;
	private	ParticleSystem.EmissionModule	footEmission;

	// 플레이어가 공중에서 바닥으로 착지할 때 나오는 이펙트
	[SerializeField]
	private	ParticleSystem					landingEffect;
	private	bool							wasOnGround;

	private void Awake()
	{
		movement	 = GetComponentInParent<MovementRigidbody2D>();
		footEmission = footStepEffect.emission;
	}

	private void Update()
	{
		// 플레이어가 바닥을 밟고 있고, 좌/우 이동속도가 0이 아니면
		if ( movement.IsGrounded && movement.Velocity.x != 0 )
		{
			footEmission.rateOverTime = 30;
		}
		else
		{
			footEmission.rateOverTime = 0;
		}

		// 바로 직전 프레임에 공중에 있었고, 이번 프레임에 바닥을 밟고 있고,
		// y 속력이 0 이하일 때 바닥에 "착지"로 판단하고 이펙트 재생
		if ( !wasOnGround && movement.IsGrounded && movement.Velocity.y <= 0 )
		{
			landingEffect.Stop();
			landingEffect.Play();
		}

		wasOnGround = movement.IsGrounded;
	}
}

