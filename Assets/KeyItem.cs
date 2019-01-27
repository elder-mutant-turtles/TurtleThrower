using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleThrower
{
    [RequireComponent(typeof(Interactable))]
    public class KeyItem : MonoBehaviour 
    {

        public enum KeyColor
        {
            Red,
            Blue,
            Yellow
        }

        public KeyColor color;

        public Interactable interactable;
        
        // Use this for initialization
        void Start ()
        {
            interactable = GetComponent<Interactable>();
            interactable.InteractCallback += OnInteract;
        }

        private void OnInteract()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            interactable.InteractCallback -= OnInteract;
        }
    }
}
