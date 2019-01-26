using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleThrower
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class ShellController : MonoBehaviour
    {
	    public Rigidbody2D rb;
	    public PolygonCollider2D collider;

	    private bool physicEnabled = false;

	    public float Velocity;
	    private Vector3 lastPosition;
	
	    private void Awake()
	    {
		    rb = rb ?? GetComponent<Rigidbody2D>();
		    collider = collider ?? GetComponent<PolygonCollider2D>();

		    var interactable = GetComponent<Interactable>();
		    if (interactable)
		    {
			    GetComponentInChildren<Interactable>();
		    }
		    
	    }

	    private void FixedUpdate()
	    {
			if (!physicEnabled)
			{
				return;
			}
	    }

	    
	    
	    public void ThrowShell(Vector2 direction, float force)
	    {
		    physicEnabled = true;

		    transform.parent = null;
		    
		    rb.bodyType = RigidbodyType2D.Dynamic;
		    rb.simulated = true;
		    rb.velocity = direction * force;
		    rb.AddTorque(-0.01f, ForceMode2D.Impulse);
	    }


	    /// <summary>
	    /// When the shell is attached to turtle, rigidbody, colliders and physics must be disabled.
	    /// </summary>
	    public void SetAttachedToTurtle(Transform parentPivot)
	    {
		    physicEnabled = false;
		    
		    transform.SetParent(parentPivot);
		    transform.localPosition = Vector3.zero;

		    rb.bodyType = RigidbodyType2D.Kinematic;
		    rb.simulated = false;
	    }
	    
	    
	    
    }

}

