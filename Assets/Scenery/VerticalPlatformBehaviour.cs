using UnityEngine;

namespace TurtleThrower
{
    public class VerticalPlatformBehaviour : MonoBehaviour
    {
        public Vector3 InitialPosition;
        public Vector3 FinalPosition;
        public float WaitTime;

        private int moving;

        private float elapseTimeIdle;

        public Rigidbody2D rg;
        
        private void Awake()
        {
            
        }

        private void Update()
        {
            if (moving == 0)
            {
                elapseTimeIdle += Time.deltaTime;
            }

            if (elapseTimeIdle >= WaitTime)
            {
                elapseTimeIdle = 0;
                moving = 1;
            }
        }
        
        private void FixedUpdate()
        {
            if (moving == 0)
            {
                return;
            }

            // Going up
            if ((moving == 1) && (transform.position - FinalPosition).magnitude < 0.01f)
            {
                rg.MovePosition(FinalPosition);
                moving = 0;
            }
            
            // Going down
            if ((moving == -1) && (transform.position - InitialPosition).magnitude < 0.01f)
            {
                rg.MovePosition(InitialPosition);
                moving = 0;
            }
        }
        
    }
}