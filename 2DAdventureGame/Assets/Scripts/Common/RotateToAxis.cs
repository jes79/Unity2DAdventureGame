using UnityEngine;

public class RotateToAxis : MonoBehaviour
{
	[SerializeField]
	private	Transform	target;
	[SerializeField]
	private	Vector3		axis = Vector3.forward;
	[SerializeField]
	private	float		rotateSpeed = 200;

	private void Update()
	{
		target.Rotate(axis, rotateSpeed * Time.deltaTime);
	}
}

