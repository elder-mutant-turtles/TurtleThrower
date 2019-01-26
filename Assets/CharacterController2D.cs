using System.Collections;
using System.Collections.Generic;
using TurtleThrower;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	[Header("Pivots")]
	public Transform DefaultShellPivot;
	public Transform ThrowShellPivot;
	
	[Header("Throw shell Config")]
	public Vector3 DefaultThrowDirection;
	public float DefaultThrowForce;
	
	private List<Interactable> interactables;
	private ShellController shellController;
	
	public float ShellMovementScale = 1f;
	public float JumpScale = 5f;
	
	public Transform Foot;
	public Rigidbody2D rigidBody;

	private int groundMask;
	private RaycastHit2D hit;

	private float footRayDistance = 0f;

	private bool facingRight = true;

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
	
	private void Start()
	{
		interactables = new List<Interactable>();
	}

	public void Move(float value)
	{
		rigidBody.velocity = Vector2.right * value * ShellMovementScale;
		facingRight = value >= 0;
	}

	public void Jump()
	{
		if (IsGrounded())
		{
			rigidBody.velocity = Vector2.up * JumpScale;
		}
	}

	/// <summary>
	/// Interact with nearable object. Can bem the shell, interrupt, item
	/// </summary>
	public void Interact()
	{
		// If holding a shell, ignore nearable interactables.
		if (shellController)
		{
			shellController.ThrowShell(Vector3.Scale(DefaultThrowDirection, Vector3.right * (facingRight ? 1 : -1)), DefaultThrowForce);
			shellController = null;
			return;
		}
		
		
		// Check proximity.
		foreach (var interactable in interactables)
		{
			interactable.Interact();
			var scInteractable = interactable.GetComponentInParent<ShellController>();
			if (scInteractable)
			{
				scInteractable.SetAttachedToTurtle(DefaultShellPivot);
				this.shellController = scInteractable;
			}
		}
	}
	
	
	

	private void OnTriggerEnter2D(Collider2D other)
	{
		Interactable interactableEnter = other.GetComponent<Interactable>();
		if (interactableEnter != null)
		{
			if (interactables.Contains(interactableEnter))
			{
				return;
			}
			interactables.Add(interactableEnter);
			Debug.Log(interactableEnter.DebugInfo());
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		Interactable interactableExit = other.GetComponent<Interactable>();
		if (interactableExit != null)
		{
			interactables.Remove(interactableExit);
			Debug.Log(string.Format("OnTriggerExit {0}", interactableExit.DebugInfo()));
		}
	}
}
