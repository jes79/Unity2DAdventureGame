using System.Collections;
using UnityEngine;

public class PlatformEffectorExtension : MonoBehaviour
{
	private PlatformEffector2D effector;

	private void Awake()
	{
		effector = GetComponent<PlatformEffector2D>();
	}

	public void OnDownWay()
	{
		StartCoroutine(nameof(ReverseRotationalOffset));
	}

	private IEnumerator ReverseRotationalOffset()
	{
		effector.rotationalOffset = 180;

		yield return new WaitForSeconds(0.5f);

		effector.rotationalOffset = 0;
	}
}

