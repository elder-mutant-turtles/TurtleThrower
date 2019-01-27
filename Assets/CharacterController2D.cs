using System.Collections.Generic;
using DG.Tweening;
using TurtleThrower;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CharacterController2D : MonoBehaviour
{
	[Header("Pivots")]
	public Transform DefaultShellPivot;
	public Transform ThrowShellPivot;
	
	[Header("Throw shell Config")]
	public Vector3 DefaultThrowDirection;
	public float DefaultThrowForce;

	[Header("Animation")] 
	public Animator Animator;
	
	private List<Interactable> interactables;
	private ShellController shellController;
	
	public float WalkVelocity = 1f;
	public float JumpHeight = 5f;
	public float Gravity = 3f;
	public float MovementAcceleration = 5f;

	public Transform Shell;
	
	public Transform Foot;
	public Rigidbody2D rigidBody;

	private bool lifting;

	public LayerMask groundMask;
	private RaycastHit2D hit;

	private float footRayDistance = 0f;

	private bool facingRight = true;

	private RaycastHit2D[] results;

	private bool grounded;

	private bool shellEquipped;
	
	Vector2 m_PreviousPosition;
	Vector2 m_CurrentPosition;
	Vector2 m_NextMovement;

	private Vector2 m_Movement;
	
	public Vector2 Velocity { get; protected set; }

	public void Move(float value)
	{
		var scaledVelocity = value * WalkVelocity * (shellEquipped ? 0.5f : 1f);
		
		Animator.SetFloat("Velocity", Mathf.Abs(scaledVelocity));

		m_Movement.x = Mathf.MoveTowards(m_Movement.x, scaledVelocity, MovementAcceleration * Time.deltaTime);	
		
		var direction = value > 0;
		if (Mathf.Abs(value) > 0.05)
		{
			facingRight = direction;
			Flip(facingRight);
		}
	}
	
	

	/// <summary>
	/// Apply movement vector to next physic step.
	/// </summary>
	/// <param name="movement"></param>
	public void Move(Vector2 movement)
	{
		m_NextMovement += movement * Time.deltaTime;
	}
	
	public bool IsLifting()
	{
		return lifting;
	}

	public bool ShellIsEquipped()
	{
		return shellEquipped;
	}
	
	public bool IsGrounded()
	{
		var resultsCount = Physics2D.RaycastNonAlloc(transform.position, Vector3.down, results, footRayDistance, groundMask);
		return resultsCount > 0;
	}

	public void Jump()
	{
		if (IsGrounded())
		{
			m_Movement.y = JumpHeight;
			Animator.SetTrigger("Jump");
		}
	}

	public void FinishThrowAnimation()
	{
		var throwDirection = DefaultThrowDirection;
		throwDirection.x = facingRight ? throwDirection.x : throwDirection.x * -1;
		shellController.ThrowShell(throwDirection, DefaultThrowForce);
		shellController = null;
		Lift(false);
		shellEquipped = false;

		Shell.GetComponent<SpriteRenderer>().sortingOrder  = - 2;
	}
	
	/// <summary>
	/// Interact with nearable object. Can bem the shell, interrupt, item
	/// </summary>
	public void Interact()
	{	
		// Check proximity.
		foreach (var interactable in interactables)
		{
			interactable.Interact();
			var scInteractable = interactable.GetComponentInParent<ShellController>();
			if (scInteractable)
			{
				scInteractable.SetAttachedToTurtle(DefaultShellPivot, FinishEquipShell);
				this.shellController = scInteractable;
			}
		}
	}

	public void Lift(bool lift)
	{
		Animator.SetBool("Lifting", lift);
		Animator.ResetTrigger("Throw");
		lifting = lift;
	}

	public void Throw()
	{
		Animator.SetTrigger("Throw");
	}

	private void Flip(bool towardRight)
	{
		var currentScale = transform.localScale;
		var isFacingRight = currentScale.x < 0;
		currentScale.x = isFacingRight != towardRight ? -1 * currentScale.x : currentScale.x;
		transform.localScale = currentScale;
	}
	
	private void Awake()
	{
		footRayDistance = Foot.localPosition.magnitude * transform.localScale.y;

		results = new RaycastHit2D[1];
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
			Debug.Log(string.Format("[{0}] interactable near {1}", typeof(CharacterController2D), interactableEnter.DebugInfo()));
		}
		
		if (!shellEquipped && other.gameObject.tag.Equals("Deadly"))
		{
			Die();
		}
	}
	
	private void FinishEquipShell()
	{
		shellEquipped = true;
	}

	private void Die()
	{
		Animator.SetTrigger("Die");
		
		transform.DOMove(Shell.position, 1.0f);
		
		Invoke("Respawn", 1.5f);
	}

	private void Respawn()
	{
		Animator.SetTrigger("Respawn");
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Interactable interactableExit = other.GetComponent<Interactable>();
		if (interactableExit != null)
		{
			interactables.Remove(interactableExit);
			
			Debug.Log(string.Format("[{0}] interactable far {1}", typeof(CharacterController2D), interactableExit.DebugInfo()));
		}
	}

	private void ApplyGravity()
	{
		if (IsGrounded())
		{
			return;
		}
		var increment = Gravity * Time.deltaTime;
		m_Movement += Vector2.down * increment;
	}
	
	private void FixedUpdate()
	{
		var nowGrounded = IsGrounded();
		if (grounded != nowGrounded)
		{
			grounded = nowGrounded;
			Animator.SetBool("Grounded", grounded);
			Animator.ResetTrigger("Jump");
		}
		
		ApplyGravity();
		
		Move(m_Movement);
		
		m_PreviousPosition = rigidBody.position;
		m_CurrentPosition = m_PreviousPosition + m_NextMovement;
		Velocity = (m_CurrentPosition - m_PreviousPosition) / Time.deltaTime;

		rigidBody.MovePosition(m_CurrentPosition);
		m_NextMovement = Vector2.zero;
	}
}
