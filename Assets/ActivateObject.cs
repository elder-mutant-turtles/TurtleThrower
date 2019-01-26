using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TurtleThrower
{

    public class ActivateObject : MonoBehaviour
    {
        public PresenceTrigger PresenceTrigger;

	    private Sequence activateSequence;
	    private Sequence deactivateSequence;
	    
        // Use this for initialization
        void Start () 
        {
	        if (PresenceTrigger == null)
	        {
		        Debug.LogError(string.Format("PresenseTrigger not found in {0}", gameObject.name));
		        return;
	        }

	        PresenceTrigger.OnEnter += OnActivateEvent;
	        PresenceTrigger.OnExit += OnDeactivateEvent;
        }

	    private void OnDestroy()
	    {
		    PresenceTrigger.OnEnter -= OnActivateEvent;
		    PresenceTrigger.OnExit -= OnDeactivateEvent;
	    }

	    private void OnActivateEvent()
	    {
		    Debug.Log(string.Format("[{0}] {1} is activating.", typeof(ActivateObject), gameObject.name));

		    if (deactivateSequence != null && deactivateSequence.IsActive())
		    {
			    deactivateSequence.Kill();
		    }
	    }

	    private void OnDeactivateEvent()
	    {
		    Debug.Log(string.Format("[{0}] {1} is deactivating.", typeof(ActivateObject), gameObject.name));
		    
		    if (activateSequence.IsActive())
		    {
			    activateSequence.Rewind();    
		    }
	    }
    }
}
