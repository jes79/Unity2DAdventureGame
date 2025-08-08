using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
	public bool IsDie { protected set; get; } = false;

	public abstract void OnDie();
}

