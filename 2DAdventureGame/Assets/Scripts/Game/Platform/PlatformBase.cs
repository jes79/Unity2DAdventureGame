using UnityEngine;

public abstract class PlatformBase : MonoBehaviour
{
	// 플랫폼과 플레이어가 충돌했는지 체크 (일정시간동안 다시 충돌체크를 하지 않도록)
	public	bool IsHit { protected set; get; } = false;

	public abstract void UpdateCollision(GameObject other);
}

