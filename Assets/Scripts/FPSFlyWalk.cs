using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class FPSFlyWalk : MonoBehaviour {

	public float speed = 6.0f;
	public float gravity = 9.8f;
	public bool isFlying = true;
	
	private Vector3 moveDirection = Vector3.zero;
	
	public Terrain TerrainArbres;

	
//	private bool grounded = false;
//	private CharacterController controller;
//
//	void Awake () {
//    	controller  = GetComponent<CharacterController>(); 
//   	}
	
	
	void Start () {
		// assume we are flying
		
			Vector3 p = Camera.main.transform.localPosition;
			p.y=0f;
			Camera.main.transform.localPosition = p;
			TerrainArbres.treeMaximumFullLODCount=0;

	       	Vector3 pos = TerrainArbres.transform.position;
	       	pos.y=6.25f;
	       	TerrainArbres.transform.position=pos;
	}

	
	
	void Fly() {
		float xAxis = (Input.GetKey(KeyCode.RightArrow)?1f:0f)+(Input.GetKey(KeyCode.LeftArrow)?-1f:0f);
		float zAxis = (Input.GetKey(KeyCode.UpArrow)?1f:0f)+(Input.GetKey(KeyCode.DownArrow)?-1f:0f);
		moveDirection = new Vector3(xAxis, 0, zAxis);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
   		if ((Input.GetKey (KeyCode.LeftShift)) ||  (Input.GetKey (KeyCode.RightShift))) moveDirection *= 2.0f;
		moveDirection.y=0;
		
		// Move the controller
		CharacterController controller = GetComponent(typeof(CharacterController)) as CharacterController;
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void Walk() {	
		CharacterController controller = GetComponent<CharacterController>(); 
	    if (controller.isGrounded) { // We are grounded, so recalculate move direction directly from axes
	        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        moveDirection = transform.TransformDirection(moveDirection);
	        moveDirection *= speed/4.0f;
	        
	        if (TerrainArbres.treeMaximumFullLODCount==0) {
	        	TerrainArbres.treeMaximumFullLODCount=1000;
	    		Vector3 pos = TerrainArbres.transform.position;
		       	pos.y=6.0f;
	 	      	TerrainArbres.transform.position=pos;
	        }
	        
	    	if ((Input.GetKey (KeyCode.LeftShift)) ||  (Input.GetKey (KeyCode.RightShift))) moveDirection *= 2.0f;
	    }
	    else {
	    		Vector3 pos = TerrainArbres.transform.position;
		       	pos.y-= 0.25f* Time.deltaTime;
		       	if (pos.y<6.0f) pos.y=6.0f;
	 	      	TerrainArbres.transform.position=pos;

	    }
	    
	    moveDirection.y -= gravity * Time.deltaTime;     // Apply gravity
	    controller.Move(moveDirection * Time.deltaTime);  // Move the controller
	}
	
	public void SwitchMode () {
		
		isFlying = !isFlying;
		if (isFlying) {
			Vector3 p = Camera.main.transform.localPosition;
			p.y=0f;
			Camera.main.transform.localPosition = p;
			TerrainArbres.treeMaximumFullLODCount=0;

	       	Vector3 pos = TerrainArbres.transform.position;
	       	pos.y=6.25f;
	       	TerrainArbres.transform.position=pos;

		}
	}

	public void SwitchToFly() {
		if (!isFlying) SwitchMode();
	}
	
	void FixedUpdate() {
		if (isFlying) Fly();
		else Walk();
	}
}



