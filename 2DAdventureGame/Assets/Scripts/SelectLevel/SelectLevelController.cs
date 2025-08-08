using UnityEngine;
using UnityEngine.UI;

public class SelectLevelController : MonoBehaviour
{
	[Header("Fade Effect")]
	[SerializeField]
	private	Image		imageFadeScreen;

	[Header("Level UI")]
	[SerializeField]
	private	UILevel		levelPrefab;
	[SerializeField]
	private	Transform	levelParent;

	private void Awake()
	{
		StartCoroutine(FadeEffect.Fade(imageFadeScreen, 1, 0, 1, AfterFadeEffect));
		LoadLevelData();
	}

	private void AfterFadeEffect()
	{
		imageFadeScreen.gameObject.SetActive(false);
	}

	private void LoadLevelData()
	{
		PlayerPrefs.SetInt($"{Constants.LevelUnlock}1", 1);

		for ( int i = 1; i <= Constants.MaxLevel; ++ i )
		{
			UILevel level = Instantiate(levelPrefab, levelParent);
			(bool, bool[]) levelData = Constants.LoadLevelData(i);

			level.SetLevel(i, levelData.Item1, levelData.Item2, imageFadeScreen);
		}
	}

	[ContextMenu("ResetData")]
	private void ResetData()
	{
		PlayerPrefs.DeleteAll();
	}
}

