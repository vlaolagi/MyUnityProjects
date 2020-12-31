using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    
	// Variables
	[SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
	[SerializeField] float period = 2f;
	
	[Range(0,1)] [SerializeField] float movementFactor; // 0 for no movement, 1 for full movement
	
	Vector3 startingPos; // Original position
	
	// Start is called before the first frame update
    void Start()
    {
	    startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
	    if(period <= Mathf.Epsilon) {return;} // Protect against period equal to 0
	    float cycles = Time.time / period;
	    
	    const float tau = Mathf.PI * 2f; // about 6.28
	    float rawSinWave = Mathf.Sin(cycles * tau);
	    
	    movementFactor = rawSinWave / 2f + 0.5f;
	    Vector3 offset = movementVector * movementFactor;
	    transform.position = startingPos + offset;
    }
}
