using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDebugger : MonoBehaviour
{

	public Text InputGot; 
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (Input.GetButtonDown("j1a0"))
		{
			InputGot.text = "button 0";
		}
		
		if (Input.GetButtonDown("j1a1"))
		{
			InputGot.text = "button 1";
		}
		
		if (Input.GetButtonDown("j1a2"))
		{
			InputGot.text = "button 2";
		}
		
		if (Input.GetButtonDown("j1a3"))
		{
			InputGot.text = "button 3";
		}
	}
}
