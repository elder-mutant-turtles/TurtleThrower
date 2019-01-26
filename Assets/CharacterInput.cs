using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

	public CharacterController2D controller;

	private bool jump; 
	
	// Update is called once per frame
	private void Update()
	{
		if (Input.GetButtonDown("j1a0"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("j1a1"))
		{
			controller.Interact();
		}

		if (controller.ShellIsEquipped() && Input.GetButtonDown("j1a1"))
		{
			controller.Lift(true);
		} 
		else if (controller.IsLifting() && Input.GetButtonUp("j1a1"))
		{
			controller.Lift(false);
		}

		if (controller.IsLifting() && Input.GetButtonDown("j1a5"))
		{
			controller.Throw();
		}
	}

	private void FixedUpdate()
	{
		var hAxis = Input.GetAxis("j1axis1");

		controller.Move(hAxis);	
		
		if (jump)
		{
			controller.Jump();
			jump = false;
		}
	}
}
