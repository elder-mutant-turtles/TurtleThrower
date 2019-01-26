using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

	public CharacterController2D controller;
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		var hAxis = Input.GetAxis("j1axis1");

		controller.Move(hAxis);

		if (Input.GetButtonDown("j1a0"))
		{
			controller.Jump();
		}

		if (Input.GetButtonDown("j1a1"))
		{
			controller.Interact();
		}
	}
}
