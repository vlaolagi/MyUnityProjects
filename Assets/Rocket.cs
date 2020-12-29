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
	[SerializeField] float delay = 3f;
	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip success;
	[SerializeField] AudioClip death;
	
	enum State {Alive, Dying, Transcending};
	State state = State.Alive;

	int sceneValue = 0;
	
	// Start is called before the first frame update
    void Start()
    {
	    rigiBody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
	    GameObject.Find ("Notification").transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
	    if(state == State.Alive) 
	    {
	    	RespondToThrustInput();
		    RespondToRotateInput();
	    }
    }
    
	void OnCollisionEnter(Collision collision) 
	{
		if(state != State.Alive){return;}
		
		switch (collision.gameObject.tag)
		{
			case "Friendly":
				print("OK");
				break;
			case "Fuel":
				StartCoroutine(StartFuelingSequence());
				break;
			case "Target":
				StartSuccessSequence();
				break;
			default:
				StartDeathSequence();
				break;
		}
	}
	
	// Methods
	private void StartSuccessSequence() 
	{
		print("Congratulations!");
		state = State.Transcending;
		audioSource.Stop();
		audioSource.PlayOneShot(success);
		sceneValue = 1;
		StartCoroutine(LoadNextScene(sceneValue, delay));
	}
	
	IEnumerator StartFuelingSequence() 
	{
		print("Refueling!");
		state = State.Alive;
		GameObject.Find ("Notification").transform.localScale = new Vector3(1, 1, 1);
		yield return new WaitForSeconds (5);
		GameObject.Find ("Notification").transform.localScale = new Vector3(0, 0, 0);
	}
	
	private void StartDeathSequence() 
	{
		print("Dead!");
		state = State.Dying;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
		sceneValue = 0;
		StartCoroutine(LoadNextScene(sceneValue, delay));
	}
	
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
	
	private void RespondToThrustInput()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			ApplyThrust();
		}
		else
		{
			audioSource.Stop();
		}
	}
	
	private void ApplyThrust()
	{
		print("Thrusters Activated!");
		rigiBody.AddRelativeForce(Vector3.up * mainThrust);
			
		if(!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
		}
	}
	
	private void RespondToRotateInput()
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
