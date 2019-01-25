using System.Collections;
using System.Collections.Generic;
using TurtleThrower;
using UnityEditor;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	public Rigidbody2D rigidBody;

	public Transform DefaultShellPivot;
	public Transform ThrowShellPivot;
	
	[Header("Throw shell Config")]
	public Vector3 DefaultThrowDirection;
	public float DefaultThrowForce;
	
	
	private List<Interactable> interactables;

	private ShellController shellController;
	
	
	public void Move(float value)
	{
		rigidBody.velocity = Vector2.right * value * 10;
	}

	public void Jump()
	{
		rigidBody.AddForce(Vector2.up * 300);
	}

	
	/// <summary>
	/// Interact with nearable object. Can bem the shell, interrupt, item
	/// </summary>
	public void Interact()
	{
		// If holding a shell, ignore nearable interactables.
		if (shellController)
		{
			shellController.ThrowShell(DefaultThrowDirection, DefaultThrowForce);
			shellController = null;
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
	
	
	private void Start()
	{
		interactables = new List<Interactable>();
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
