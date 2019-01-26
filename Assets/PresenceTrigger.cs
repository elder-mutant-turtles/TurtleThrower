using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleThrower
{

    public class PresenceTrigger : MonoBehaviour
    {
        public Collider2D trigger;
        
        public event Action OnEnter;
        public event Action OnExit;

        public bool Turtle;
        public bool Shell;

        private void Awake()
        {
            if (trigger == null)
            {
                trigger = GetComponent<Collider2D>();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (OnEnter == null)
            {
                return;
            }

            bool dispatchOnEnter = false;
            
            CharacterController2D turtleController2D = other.GetComponent<CharacterController2D>();
            if (turtleController2D && Turtle)
            {
                dispatchOnEnter = true;
            }

            ShellController shellController = other.GetComponent<ShellController>();
            if (shellController && Shell)
            {
                dispatchOnEnter = true;
            }

            if (dispatchOnEnter)
            {
                OnEnter.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (OnExit == null)
            {
                return;
            }
            
            bool shouldExit = false;
            
            CharacterController2D turtleController2D = other.GetComponent<CharacterController2D>();
            if (turtleController2D && Turtle)
            {
                shouldExit = true;
            }

            ShellController shellController = other.GetComponent<ShellController>();
            if (shellController && Shell)
            {
                shouldExit = true;
            }

            if (shouldExit)
            {
                OnExit.Invoke();
            }
        }
    }


}
