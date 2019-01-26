using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TurtleThrower
{

    public class ActivateObject : MonoBehaviour
    {
        public PresenceTrigger PresenceTrigger;

	    // Use this for initialization
        void Start () 
        {
	        if (PresenceTrigger == null)
	        {
		        Debug.LogError(string.Format("PresenseTrigger not found in {0}", gameObject.name));
		        return;
	        }

	
        }

	    private void OnDestroy()
	    {
	    }

	    private void OnActivateEvent()
	    {
		    Debug.Log(string.Format("[{0}] {1} is activating.", typeof(ActivateObject), gameObject.name));

			
	    }

	    private void OnDeactivateEvent()
	    {
		    Debug.Log(string.Format("[{0}] {1} is deactivating.", typeof(ActivateObject), gameObject.name));
		    
		   
	    }
    }
}
