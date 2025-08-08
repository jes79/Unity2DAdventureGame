using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
	[SerializeField]
	private	Transform	target;				// 실제 이동하는 발판의 Transform

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.transform.SetParent(target.transform);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		collision.transform.SetParent(null);
	}
}

