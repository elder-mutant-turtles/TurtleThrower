using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleThrower
{
    [RequireComponent(typeof(Interactable))]
    public class CollectableItem : MonoBehaviour 
    {

        public enum ItemID
        {
            RedKey,
            BlueKey,
            YellowKey,
            CollectableBottle,
            CollectableCrocs,
            CollectableFrame,
            CollectableHat,
            CollectableVhs,
            GreenKey,
            CyanKey
        }

        public ItemID id;

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
