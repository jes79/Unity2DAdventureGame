using UnityEngine;

public class ItemCoin : ItemBase
{
	public override void UpdateCollision(Transform target)
	{
		target.GetComponent<PlayerData>().Coin ++;
	}
}

