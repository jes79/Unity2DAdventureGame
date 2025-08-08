using UnityEngine;

public class ItemProjectile : ItemBase
{
	public override void UpdateCollision(Transform target)
	{
		target.GetComponent<PlayerData>().CurrentProjectile ++;
	}
}

