using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameWinScreen : MonoBehaviour
{

    public CanvasGroup CanvasAlpha;

    public static GameWinScreen Instance;

    private bool shown = false;

    private void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        CanvasAlpha.DOFade(1, 0.3f).SetUpdate(true);

        Time.timeScale = 0f;

        shown = true;
    }

    public void Hide()
    {
        CanvasAlpha.DOFade(0f, 0.3f).SetUpdate(true);
        Application.Quit();

        shown = false;
        
        Application.OpenURL("https://www.washingtonpost.com/news/animalia/wp/2016/09/19/with-800-offspring-very-sexually-active-tortoise-saves-species-from-extinction/?noredirect=on&utm_term=.20b64c7a9d5c");
    }

    private void Update()
    {
        if (shown && Input.GetKeyDown("f"))
        {
            Hide();
        }
    }
}
