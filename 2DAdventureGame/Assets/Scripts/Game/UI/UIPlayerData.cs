using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerData : MonoBehaviour
{
	[Header("HP")]
	[SerializeField]
	private	Image[]			hpImages;

	[Header("COIN")]
	[SerializeField]
	private	TextMeshProUGUI	textCoin;

	[Header("PROJECTILE")]
	[SerializeField]
	private	TextMeshProUGUI	textProjectile;

	[Header("STAR")]
	[SerializeField]
	private	GameObject[]	starObjects;

	public void SetHP(int index, bool isActive)
	{
		hpImages[index].color = isActive == true ? Color.white : Color.black;
	}

	public void SetCoin(int coinCount)
	{
		textCoin.text = $"x {coinCount}";
	}

	public void SetProjectile(int current, int max)
	{
		textProjectile.text = $"{current}/{max}";

		if ( ((float)current / max) <= 0.3f )	textProjectile.color = Color.red;
		else									textProjectile.color = Color.white;
	}

	public void SetStar(int index)
	{
		starObjects[index].SetActive(true);
	}
}

