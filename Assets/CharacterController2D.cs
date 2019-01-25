using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

	public Rigidbody2D rigidBody;

	public void Move(float value)
	{
		rigidBody.velocity = Vector2.right * value * 10;
	}

	public void Jump()
	{
		rigidBody.AddForce(Vector2.up * 300);
	}
}
