using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

	private const string BUTTON_1 = "j1a1";
	private const string BUTTON_0 = "j1a0";
	private const string BUTTON_5 = "j1a5";
	private const string KEY_UP = "up";
	private const string KEY_RIGHT = "right";
	private const string KEY_LEFT = "left";
	private const string KEY_Z = "z";
	private const string KEY_SPACE = "space";
	private const string AXIS_H = "j1axis1";
	
	public CharacterController2D controller;

	private bool jump; 
	
	// Update is called once per frame
	private void Update()
	{
		if (JumpInputPressed())
		{
			jump = true;
		}

		if (ActionInputPressed())
		{
			controller.Interact();
		}

		if (controller.ShellIsEquipped() && AimInputPressed())
		{
			controller.Lift(true);
		} 
		else if (controller.IsLifting() && AimInputUp())
		{
			controller.Lift(false);
		}

		if (controller.IsLifting() && ActionInputPressed())
		{
			controller.Throw();
		}
	}

	private void FixedUpdate()
	{
		var hAxis = HorizontalAxisMove();

		controller.Move(hAxis);	
		
		if (jump)
		{
			controller.Jump();
			jump = false;
		}
	}

	private bool JumpInputPressed()
	{
		return Input.GetButtonDown(BUTTON_1) || Input.GetKeyDown(KEY_SPACE);
	}

	private bool ActionInputPressed()
	{
		return Input.GetButtonDown(BUTTON_0) || Input.GetKeyDown(KEY_Z);
	}

	private bool AimInputPressed()
	{
		return Input.GetButtonDown(BUTTON_5) || Input.GetKeyDown(KEY_UP);
	}

	private bool AimInputUp()
	{
		return Input.GetButtonUp(BUTTON_5) || Input.GetKeyUp(KEY_UP);
	}

	private float HorizontalAxisMove()
	{
		var jAxis = Input.GetAxis(AXIS_H);

		var kAxis = Input.GetKey(KEY_RIGHT) ? 1 : 0;

		kAxis += Input.GetKey(KEY_LEFT) ? -1 : 0;

		var value = kAxis + jAxis;

		return Mathf.Clamp(value, -1f, 1f);
	}
}
