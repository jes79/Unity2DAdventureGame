using UnityEngine;

[System.Serializable]
public struct BackgroundData
{
	public	Renderer	background;
	public	float		speed;
}

public class ParallaxBackground : MonoBehaviour
{
	[SerializeField]
	private	Transform			targetCamera;
	[SerializeField]
	private	BackgroundData[]	backgrounds;
	[SerializeField]
	private	BackgroundData		backgroundCloud;

	private	float				targetStartX;

	private void Awake()
	{
		targetStartX = targetCamera.position.x;
	}

	private void Update()
	{
		float cloudOffset = Time.time * backgroundCloud.speed;
		backgroundCloud.background.material.mainTextureOffset = new Vector2(cloudOffset, 0);

		if ( targetCamera == null ) return;

		float x = targetCamera.position.x - targetStartX;
		for ( int i = 0; i < backgrounds.Length; ++ i )
		{
			float offset = x * backgrounds[i].speed;
			backgrounds[i].background.material.mainTextureOffset = new Vector2(offset, 0);
		}
	}
}

