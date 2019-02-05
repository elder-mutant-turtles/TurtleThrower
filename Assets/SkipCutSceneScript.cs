using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipCutSceneScript : MonoBehaviour
{

	private const float SKIP_TIME = 1.5f;
	public Image SpinnerImage;

	private float timer;

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey("z") || Input.GetButton("j1a0"))
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0f;
		}

		var holdingRatio = timer / SKIP_TIME;
		
		SpinnerImage.fillAmount = holdingRatio;

		if (holdingRatio > 1.0f)
		{
			SceneManager.LoadScene("FinalLevel");
		}
	}
}
