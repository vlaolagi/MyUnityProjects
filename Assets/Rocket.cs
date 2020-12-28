using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{ 
    
	// Variables
	Rigidbody rigiBody;
	AudioSource audioSource;
	
	// Start is called before the first frame update
    void Start()
    {
	    rigiBody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
	    ProcessInput();	    
    }
    
	private void ProcessInput()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			print("Thrusters Activated!");
			rigiBody.AddRelativeForce(Vector3.up);
			if(!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		else
		{
			audioSource.Stop();
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			print("Left Thruster Activated!");
			transform.Rotate(Vector3.forward);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			print("Right Thruster Activated!");
			transform.Rotate(-Vector3.forward);
		}                                                                                      
	
		
		//switch(Input.GetKey(KeyCode))
		//	{
		//	case (Space):
		//		print("Blasters Activated!");
		//		break;
		//	case (RightArrow):
		//		print("Left Thruster Activated!");
		//		break;
		//	case (LeftArrow):
		//		print("Right Thruster Activated!");
		//		break;
		//	case (UpArrow):
		//		print("Forward Thrusters Activated!");
		//		break;
		//	case (DownArrow):
		//		print("Breaks Activated!");
		//		break;
		//	default:
		//		// code block
    	//		break;
		//	}
	}
}
