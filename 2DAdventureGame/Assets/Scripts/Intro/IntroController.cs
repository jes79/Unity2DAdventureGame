using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroController : MonoBehaviour
{
	[SerializeField]
	private	TextMeshProUGUI	textPressAnyKey;
	[SerializeField]
	private	float			textBlinkTime = 0.5f;
	[SerializeField]
	private	Image			imageFadeScreen;

	private	bool			isKeyDown = false;

	private IEnumerator Start()
	{
		while ( true )
		{
			yield return StartCoroutine(FadeEffect.Fade(textPressAnyKey, 1, 0, textBlinkTime));

			yield return StartCoroutine(FadeEffect.Fade(textPressAnyKey, 0, 1, textBlinkTime));
		}
	}

	private void Update()
	{
		if ( isKeyDown == true ) return;

		if ( Input.anyKeyDown )
		{
			isKeyDown = true;

			StartCoroutine(FadeEffect.Fade(imageFadeScreen, 0, 1, 1, AfterFadeEffect));
		}
	}

	private void AfterFadeEffect()
	{
		Utils.LoadScene(SceneNames.SelectLevel);
	}
}

