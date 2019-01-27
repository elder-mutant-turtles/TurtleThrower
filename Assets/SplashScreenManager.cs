using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{

	public CanvasGroup canvas;
	
	// Use this for initialization
	void Start ()
	{
		Sequence seq = DOTween.Sequence();
		
		seq.Append(canvas.DOFade(1, 0.4f));
		seq.Insert(2f, canvas.DOFade(0, 0.5f));

		seq.onComplete += Next;
	}

	private void Next()
	{
		SceneManager.LoadScene("PaivaScene");
	}
}
