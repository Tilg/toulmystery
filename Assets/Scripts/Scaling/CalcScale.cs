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
		GameObject TestLocalisation = GameObject.Find ("TestLocalisation");
		
		GameObject mapPoint3 = GameObject.Find ("MapPoint3");
		GPSPoint gps3 = (GPSPoint) mapPoint3.GetComponent ("GPSPoint");
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1");
		
		GameObject TestCamera = GameObject.Find ("TestCamera");
		
		// Scale.rotateMap();
		Scale.placeGameObjectAt(TestLocalisation,48.676755,5.890141);
		Scale.placeGameObjectAt(TestCamera,48.676755,5.890141);
		
		Vector3 v = Scale.fromLatLng2XZ(gps3.lat,gps3.lng);
		Debug.Log (v);
		
		Vector3 delta1 = new Vector3(v.x - mapPoint1.transform.position.x, 0 ,  v.z - mapPoint1.transform.position.z);
		Vector3 delta2 = new Vector3(mapPoint3.transform.position.x - mapPoint1.transform.position.x, 0 ,  mapPoint3.transform.position.z  - mapPoint1.transform.position.z);
		
        float angle1 = Mathf.Atan2(delta1.z, delta1.x) * Mathf.Rad2Deg;
		Debug.Log (angle1);
		
		
		float angle2 = Mathf.Atan2(delta2.z, delta2.x) * Mathf.Rad2Deg;
		Debug.Log (angle2);
		// Scale.fromXZ2LatLng(TestLocalisation.transform.position.x,TestLocalisation.transform.position.z);
		// Scale.(TestCamera, 48.677658,5.890704);
		// end test	
	}
	
}
