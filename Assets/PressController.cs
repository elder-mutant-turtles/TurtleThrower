using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressController : MonoBehaviour
{

	public Animator Animator;

	public float UpTime = 2f;

	private float timer = 0f;
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > UpTime)
		{
			Animator.SetTrigger("Fall");
			timer = 0f;
		}
	}
}
