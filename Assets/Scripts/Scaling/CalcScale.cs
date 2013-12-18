using UnityEngine;
using System.Collections;

public class CalcScale : MonoBehaviour
{
	
	void init() 
	{

		
	}
	
	
	// Use this for initialization
	void Awake()
	{		
		// test
		GameObject testLocalisation = GameObject.Find ("TestLocalisation");
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2");
		GPSPoint gps2 = (GPSPoint) mapPoint2.GetComponent ("GPSPoint");
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1");
		GPSPoint gps1 = (GPSPoint) mapPoint1.GetComponent ("GPSPoint");
		
		// GameObject TestCamera = GameObject.Find ("TestCamera");
		
		// Transpose.rotateMap();

		
		// Vector2 v = Transpose.fromLatLng2XZ(gps3.lat,gps3.lng);
		// Debug.Log (v);
		
		// Transpose.fromXZ2LatLng(TestLocalisation.transform.position.x,TestLocalisation.transform.position.z);
		
		Vector2 unityMp1Mp2 = new Vector2(mapPoint2.transform.position.x - mapPoint1.transform.position.x, mapPoint2.transform.position.z - mapPoint1.transform.position.z);
		Vector2 gpsMp1Mp2= new Vector2(gps2.lng - gps1.lng, gps2.lat - gps1.lat);  
		
		float unityAngle = Mathf.Atan2(unityMp1Mp2.y, unityMp1Mp2.x);
		float gpsAngle = Mathf.Atan2(gpsMp1Mp2.y, gpsMp1Mp2.x * Mathf.Cos(gps1.lat * Mathf.Deg2Rad)) ;
		
		float unityAngleDeg = unityAngle * Mathf.Rad2Deg;
		float gpsAngleDeg = gpsAngle * Mathf.Rad2Deg;
		
		float angle = (unityAngleDeg - gpsAngleDeg);
		
		GameObject testCamera = GameObject.Find("TestCamera");
		float yRotation = 360 - angle;
		
		
		Transpose.placeGameObjectAt(testLocalisation,48.676755,5.890141);
		Transpose.placeGameObjectAt(testCamera,48.676755,5.890141);
		
		testCamera.transform.eulerAngles = new Vector3(90, yRotation, 0);

		Debug.Log(yRotation);
		
	}
	
}
