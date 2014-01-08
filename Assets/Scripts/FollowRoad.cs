using UnityEngine;

public class FollowRoad : MonoBehaviour
{
	public string roadName = "road name";
    public Transform[] waypoints;
    public float steerFactor = 1.0f;
    public bool loop = false;
    public float speed = 2.0f;
    public bool faceHeading = true;
    public bool showRoadsOnScene = true;

    private Vector3 currentHeading,targetHeading;
    private int targetwaypoint;
    private Transform xform;
    
    
    public void EnableMoving (bool val) {
  	 	enabled = val;
    	if (val) {
	        if(waypoints.Length<=1)  {
	            Debug.Log("No road for "+roadName);
	            enabled = false;
	        }
 	 		transform.position = waypoints[0].position;
 	 		
 	 		// avoid tilt => y must stay at same value
 	 		Vector3 v = waypoints[1].position;
 	 		v.y=transform.position.y;
 	 		transform.LookAt(v);
 	 		
	 		Camera.main.transform.LookAt(waypoints[1].position);
	 		 	 		Camera.main.transform.Rotate(60.0f,0.0f,0.0f);

 	   		xform = transform;
	        currentHeading = xform.forward;
	        targetwaypoint = 1;
    	}
 //   	enabled = false;
    	
    	
    }

    // calculates a new heading
    protected void FixedUpdate ()
    {
    	
        targetHeading = (waypoints[targetwaypoint].position - xform.position);
        targetHeading.Normalize();
//        if (targetHeading.magnitude>1.0f) targetHeading.Normalize();
//        else targetHeading = targetHeading /10.0f;
       	currentHeading = Vector3.Lerp(currentHeading,targetHeading,Time.deltaTime*steerFactor);
    }
 
 
     protected void Update()
    {

	   	float k = 1.0f;
 		if ((Input.GetKey (KeyCode.LeftShift)) ||  (Input.GetKey (KeyCode.RightShift))) k = 2.0f;
 		
 		 if(faceHeading) {
 			float f =  Vector3.Angle(currentHeading,targetHeading);
 			f=(180f-f)/180f;
 			f=f*f*f*f;
 			k=k*f;
 		 }
 		  		
 		Vector3 newPos = xform.position +currentHeading * Time.deltaTime * speed * k;

        Vector3 v1=waypoints[targetwaypoint].position-newPos;
        Vector3 v2=waypoints[targetwaypoint].position-xform.position;
        Vector3 roadOrientation = waypoints[targetwaypoint].position-(waypoints[targetwaypoint-1].position);
        
         v1 = Vector3.Project(v1, roadOrientation);
         v2 = Vector3.Project(v2, roadOrientation);
        
   		if (Vector3.Angle(v1,v2) >179.0f) { 		
             targetwaypoint++;
            if(targetwaypoint>=waypoints.Length) {
                targetwaypoint = 0;
                if(!loop) enabled = false;
            }
          	targetHeading = (waypoints[targetwaypoint].position - xform.position);
       		targetHeading.Normalize();
       		currentHeading = Vector3.Lerp(currentHeading,targetHeading,Time.deltaTime*steerFactor);
          	newPos = xform.position +currentHeading * Time.deltaTime * speed * k;
        }

		
       	xform.position = newPos;

       	 if(faceHeading)  {
 	 			// avoid tilt => y must stay at same value
 	 			//Vector3 v = xform.position+currentHeading;
 	 			Vector3 v = xform.position+currentHeading;
 	 			v.y=transform.position.y;
        		xform.LookAt(v);
       		}
       	
    }

 
    protected void UpdateOLD()
    {
    	float k = 1.0f;
 		if ((Input.GetKey (KeyCode.LeftShift)) ||  (Input.GetKey (KeyCode.RightShift))) k = 2.0f;
       	xform.position +=currentHeading * Time.deltaTime * speed * k;
        if(faceHeading)  {
 	 		// avoid tilt => y must stay at same value
 	 		Vector3 v = xform.position+currentHeading;
 	 		v.y=transform.position.y;
        	xform.LookAt(v);
        }
   		float currentDistance = Vector3.Distance(xform.position,waypoints[targetwaypoint].position);
        if(currentDistance<0.25f) {
             targetwaypoint++;
            if(targetwaypoint>=waypoints.Length) {
                targetwaypoint = 0;
                if(!loop) enabled = false;
            }
        }
    }
      
   // draws road as red lines in scene
    public void OnDrawGizmos()
    {
     	if (showRoadsOnScene) {
    		if(waypoints==null) return;
  			if (waypoints.Length<=0) return;
 	        Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (waypoints[0].position, 1);
			if (waypoints.Length<=1) return;
	        for (int i=1;i< waypoints.Length;i++)  {
	        	if ((waypoints[i-1]!=null) && (waypoints[i]!=null))
	            	Gizmos.DrawLine(waypoints[i-1].position,waypoints[i].position);
	        }
	        if ((loop) &  ((waypoints[waypoints.Length-1]!=null) && (waypoints[0]!=null)))
	        	Gizmos.DrawLine(waypoints[waypoints.Length-1].position,waypoints[0].position);
    	}
    }

}
