using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField]
	private	GameObject	projectile;
	[SerializeField]
	private	Transform	spawnPoint;

	public void StartFire(int direction)
	{
		GameObject clone = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
		clone.GetComponent<PlayerProjectile>().Setup(direction);
	}
}

