using System.Collections;
using UnityEngine;

public class PropsChest : MonoBehaviour
{
	[SerializeField]
	private	GameObject[]	itemPrefabs;				// 생성되는 아이템 프리팹들
	[SerializeField]
	private	int				itemCount;					// 상자에서 생성되는 아이템 개수
	[SerializeField]
	private	Sprite			openChestImage;				// 열린 상자 이미지

	private	SpriteRenderer	spriteRenderer;
	private	bool			isChestOpen = false;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( isChestOpen == false && collision.CompareTag("Player") )
		{
			isChestOpen				= true;
			spriteRenderer.sprite	= openChestImage;

			StartCoroutine(nameof(SpawnAllItems));
		}
	}

	private IEnumerator SpawnAllItems()
	{
		int count = 0;
		while ( count < itemCount )
		{
			int			index	= Random.Range(0, itemPrefabs.Length);
			GameObject	item	= Instantiate(itemPrefabs[index], transform.position, Quaternion.identity);
			item.GetComponent<ItemBase>().Setup();

			yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

			count ++;
		}

		float destroyTime = 2;
		StartCoroutine(FadeEffect.Fade(spriteRenderer, 1, 0, destroyTime));
		Destroy(gameObject, destroyTime);
	}
}

