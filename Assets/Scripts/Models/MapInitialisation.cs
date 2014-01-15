using UnityEngine;
using System.Collections;

public class MapInitialisation : MonoBehaviour {
	
	
	// Returns the angle for a "north orientation" (in Degrees)
	// Angle in counter clockwise
	public float getRotationAngleDeg() {
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2");
		GPSPoint gps2 = (GPSPoint) mapPoint2.GetComponent ("GPSPoint");
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1");
		GPSPoint gps1 = (GPSPoint) mapPoint1.GetComponent ("GPSPoint");
			
		Vector2 unityMp1Mp2 = new Vector2(mapPoint2.transform.position.x - mapPoint1.transform.position.x, mapPoint2.transform.position.z - mapPoint1.transform.position.z);
		Vector2 gpsMp1Mp2= new Vector2(gps2.lng - gps1.lng, gps2.lat - gps1.lat);  
		
		float unityAngle = Mathf.Atan2(unityMp1Mp2.y, unityMp1Mp2.x);
		float gpsAngle = Mathf.Atan2(gpsMp1Mp2.y, gpsMp1Mp2.x * Mathf.Cos(gps1.lat * Mathf.Deg2Rad)) ;
		
		float unityAngleDeg = unityAngle * Mathf.Rad2Deg;
		float gpsAngleDeg = gpsAngle * Mathf.Rad2Deg;
		
		return (unityAngleDeg - gpsAngleDeg);
	}
	
	
		
	// initialize map rotation
	void Awake () {
		
		// map rotation
		
		GameObject racine = GameObject.Find("Racine");
		float newAngle = racine.transform.eulerAngles.y + getRotationAngleDeg();
		
		//Debug.Log("Rotation map:" + newAngle );
		racine.transform.eulerAngles = new Vector3(0, newAngle,0);

	

	}
	
}
