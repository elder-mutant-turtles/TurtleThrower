using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleThrower
{


    public class SawRotate : MonoBehaviour
    {

        public Transform sawObject;

        public float RotateSpeed = 1.0f;
        
        // Use this for initialization
        void Start () 
        {
		
        }
	
        // Update is called once per frame
        void Update () 
        {
		    sawObject.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime);
        }
        
        
    }

}
