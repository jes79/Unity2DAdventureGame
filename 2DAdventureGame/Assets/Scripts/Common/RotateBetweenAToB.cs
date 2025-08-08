using UnityEngine;

public class RotateBetweenAToB : MonoBehaviour
{
	[SerializeField]
	private	Transform	target;
	[SerializeField]
	private	float		rotateAngle = 40;
	[SerializeField]
	private	float		rotateSpeed = 2;

	private void Update()
	{
		float angle = rotateAngle * Mathf.Sin(Time.time * rotateSpeed);

		target.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}

