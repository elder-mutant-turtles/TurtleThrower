using System;
using UnityEngine;

namespace TurtleThrower
{
    public class Interactable : MonoBehaviour
    {
        public Collider2D triggerCollider;

        public event Action InteractCallback;
        
        public void Interact()
        {
            if (InteractCallback != null)
            {
                InteractCallback.Invoke();    
            }
        }

        public string DebugInfo()
        {
            return string.Format("{0}", gameObject.name);
        }
    }
    
}