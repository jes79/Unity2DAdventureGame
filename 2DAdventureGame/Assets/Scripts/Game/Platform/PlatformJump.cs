using UnityEngine;

public class PlatformJump : PlatformBase
{
	[SerializeField]
	private	float	jumpForce = 22;
	[SerializeField]
	private	float	resetTime = 0.5f;

	private	Animator	animator;
	private	GameObject	other;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public override void UpdateCollision(GameObject other)
	{
		if ( IsHit == true ) return;

		IsHit		= true;
		this.other	= other;

		animator.SetTrigger("onJump");
	}

	public void JumpAction()
	{
		other.GetComponent<MovementRigidbody2D>().JumpTo(jumpForce);
		other = null;

		Invoke(nameof(Reset), resetTime);
	}

	private void Reset()
	{
		IsHit = false;
	}
}

