using System.Collections;
using UnityEngine;

public class EnemyFrog : EnemyBase
{
	[SerializeField]
	private	LayerMask			groundLayer;

	private	MovementRigidbody2D	movement2D;
	private	new Collider2D		collider2D;
	private	Animator			animator;
	private	SpriteRenderer		spriteRenderer;
	private	int					direction = -1;

	private void Awake()
	{
		movement2D		= GetComponent<MovementRigidbody2D>();
		collider2D		= GetComponent<Collider2D>();
		animator		= GetComponentInChildren<Animator>();
		spriteRenderer	= GetComponentInChildren<SpriteRenderer>();

		StartCoroutine(nameof(Idle));
	}

	public override void OnDie()
	{
		if ( IsDie == true ) return;

		IsDie = true;

		StopAllCoroutines();

		float destroyTime = 2;
		StartCoroutine(FadeEffect.Fade(spriteRenderer, 1, 0, destroyTime));
		Destroy(gameObject, destroyTime);
	}

	private IEnumerator Idle()
	{
		float waitTime	= 2;
		float time		= 0;

		while ( time < waitTime )
		{
			time += Time.deltaTime;

			yield return null;
		}

		movement2D.Jump();
		animator.SetTrigger("onJump");

		StartCoroutine(nameof(Jump));
	}

	private IEnumerator Jump()
	{
		yield return new WaitUntil(()=>!movement2D.IsGrounded);

		while ( true )
		{
			UpdateDirection();
			movement2D.MoveTo(direction);
			animator.SetFloat("velocityY", movement2D.Velocity.y);

			if ( movement2D.IsGrounded )
			{
				movement2D.MoveTo(0);
				animator.SetTrigger("onLanding");

				StartCoroutine(nameof(Idle));

				yield break;
			}

			yield return null;
		}
	}

	private void UpdateDirection()
	{
		Bounds	bounds	 = collider2D.bounds;
		Vector2	size	 = new Vector2(0.1f, (bounds.max.y - bounds.min.y) * 0.8f);
		Vector2	position = new Vector2(direction == -1 ? bounds.min.x : bounds.max.x, bounds.center.y);

		if ( Physics2D.OverlapBox(position, size, 0, groundLayer) )
		{
			direction *= -1;
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}
	}
}

