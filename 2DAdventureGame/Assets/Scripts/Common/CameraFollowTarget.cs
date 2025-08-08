using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
	[SerializeField]
	private	Transform	target;
	[SerializeField]
	private	bool		x, y, z;

	private	StageData	stageData;
	private	float		offsetY;

	public void Setup(StageData stageData)
	{
		this.stageData		= stageData;
		transform.position	= new Vector3(stageData.CameraPosition.x, stageData.CameraPosition.y, -10);
	}

	private void Awake()
	{
		offsetY = Mathf.Abs(transform.position.y - target.position.y);
	}

	private void LateUpdate()
	{
		// true 축만 target의 좌표를 따라가도록 설정
		transform.position = new Vector3((x ? target.position.x : transform.position.x),
										 (y ? target.position.y + offsetY : transform.position.y),
										 (z ? target.position.z : transform.position.z));

		// 카메라의 좌/우측 이동 범위를 넘어가지 않도록 설정
		Vector3 position = transform.position;
		position.x = Mathf.Clamp(transform.position.x, stageData.CameraLimitMinX, stageData.CameraLimitMaxX);
		transform.position = position;
	}
}

