// Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{ 
    
	// Variables
	Rigidbody rigiBody;
	AudioSource audioSource;
	
	[SerializeField] float rcsThrust = 250f; // reaction control system
	[SerializeField] float mainThrust = 50f; // reaction control system
	
	enum State {Alive, Dying, Transcending};
	State state = State.Alive;
	
	float delay = 1f;
	
	// Start is called before the first frame update
    void Start()
    {
	    rigiBody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
	    if(state == State.Alive) 
	    {
	    	Thrust();
		    Rotate();
	    }
    }
    
	void OnCollisionEnter(Collision collision) 
	{
		if(state != State.Alive){return;}
		
		print("Collided!");
		int sceneValue = 0;
		switch (collision.gameObject.tag)
		{
			case "Friendly":
				print("OK");
				break;
			case "Fuel":
				print("Refueling!");
				state = State.Alive;
				break;
			case "Target":
				print("Congratulations!");
				state = State.Transcending;
				sceneValue = 1;
				StartCoroutine(LoadNextScene(sceneValue, delay));
				break;
			default:
				print("Dead!");
				state = State.Dying;
				sceneValue = 0;
				StartCoroutine(LoadNextScene(sceneValue, delay));
				break;
		}
	}
	
	// Methods
	IEnumerator LoadNextScene(int val, float delay)
	{
		switch (val)
		{
		case 0:
			yield return new WaitForSeconds(delay);
			SceneManager.LoadScene(0);
			break;
		case 1:
			yield return new WaitForSeconds(delay);
			SceneManager.LoadScene(1);
			break;
		default:
			yield return new WaitForSeconds(delay);
			SceneManager.LoadScene(1);
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
