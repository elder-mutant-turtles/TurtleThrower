using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

	public float ShellMovementScale = 1f;
	public float JumpScale = 5f;
	
	public Transform Foot;
	public Rigidbody2D rigidBody;

	private int groundMask;
	private RaycastHit2D hit;

	private float footRayDistance = 0f;

	public bool IsGrounded()
	{
		var result = Physics2D.Raycast(transform.position, Foot.localPosition, footRayDistance, groundMask);
		return result.collider != null;
	}

	private void Awake()
	{
		groundMask = 1 << LayerMask.NameToLayer("Ground");

		footRayDistance = Foot.localPosition.magnitude;
	}

	public void Move(float value)
	{
		rigidBody.velocity = Vector2.right * value * ShellMovementScale;
	}

	public void Jump()
	{
		if (IsGrounded())
		{
			rigidBody.velocity = Vector2.up * JumpScale;	
		}
	}
}
