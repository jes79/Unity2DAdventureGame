using UnityEngine;

public class PlayerData : MonoBehaviour
{
	[SerializeField]
	private	UIPlayerData	uiPlayerData;

	private	int				coin = 0;
	private	int				projectile = 0;
	private	bool[]			stars = new bool[3] { false, false, false };

	public int Coin
	{
		set
		{
			coin = Mathf.Clamp(value, 0, 9999);

			uiPlayerData.SetCoin(coin);
		}
		get => coin;
	}

	// get만 가능한 property이기 때문에 readonly/const와 같이 수정 불가능
	public int MaxProjectile { get; } = 10;

	public int CurrentProjectile
	{
		set
		{
			projectile = Mathf.Clamp(value, 0, MaxProjectile);

			uiPlayerData.SetProjectile(projectile, MaxProjectile);
		}
		get => projectile;
	}

	public bool[] Stars => stars;

	public void GetStar(int index)
	{
		stars[index] = true;

		uiPlayerData.SetStar(index);
	}

	private void Awake()
	{
		CurrentProjectile = 0;
	}
}

