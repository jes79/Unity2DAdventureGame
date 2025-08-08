using UnityEngine;

public class MovementTransform2D : MonoBehaviour
{
	[SerializeField]
	private	float	moveSpeed = 0;
	[SerializeField]
	private	Vector3	moveDirection = Vector3.zero;

	private void Update()
	{
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}

	public void MoveTo(Vector3 direction)
	{
		moveDirection = direction;
	}
}

