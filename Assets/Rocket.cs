using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{ 
    
	// Variables
	Rigidbody rigiBody;
	AudioSource audioSource;
	
	[SerializeField] float rcsThrust = 250f; // reaction control system
	[SerializeField] float mainThrust = 50f; // reaction control system
	
	// Start is called before the first frame update
    void Start()
    {
	    rigiBody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
	    Thrust();
	    Rotate();
    }
    
	void OnCollisionEnter(Collision collision) 
	{
		print("Collided!");
		switch (collision.gameObject.tag)
		{
			case "Friendly":
				print("OK");
				break;
			case "Fuel":
				print("Refueling!");
				break;
			case "Target":
				print("Congratulations!");
				break;
			default:
				print("Dead!");
				break;
			
		}
	}
	
	private void Thrust()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			print("Thrusters Activated!");
			rigiBody.AddRelativeForce(Vector3.up * mainThrust);
			
			if(!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		else
		{
			audioSource.Stop();
		}
	}
	
	private void Rotate()
	{
		
		rigiBody.freezeRotation = true;
		
		float rotationThisFrame = rcsThrust * Time.deltaTime;
		
		if(Input.GetKey(KeyCode.A))
		{
			print("Left Thruster Activated!");
			transform.Rotate(Vector3.forward * rotationThisFrame);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			print("Right Thruster Activated!");
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}
		
		rigiBody.freezeRotation = false;
	}
}
