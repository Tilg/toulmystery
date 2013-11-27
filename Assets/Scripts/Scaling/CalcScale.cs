using UnityEngine;
using System.Collections;

public class CalcScale : MonoBehaviour {

	// Use this for initialization
	void Start () {		
		print(Scale.getInfo());
		
		// Petit test
		GameObject TestLocalisation = GameObject.Find("TestLocalisation");
		GameObject TestCamera = GameObject.Find ("TestCamera");
		
		// Scale.placeGameObjectAt(TestLocalisation,48.678607,5.891568,25);
		// Scale.placeGameObjectAt (TestCamera,48.678607,5.891568,40);
		
		
		// GameObject test = GameObject.Find("test");
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GameObject mapPoint3 = GameObject.Find ("MapPoint3"); 
		
		 GPSPoint p1 = Scale.getGameObjectLocation(mapPoint3);
		 GPSPoint p1bis = (GPSPoint)mapPoint3.GetComponent ("GPSPoint");
		
		 Debug.Log("coordonnees de test--> " + p1.lat + "," + p1.lng);
		 Debug.Log("coordonnees attendues --> " + p1bis.lat + "," + p1bis.lng);
		
		float dist = Scale.getDistWorldInM (p1, p1bis);
		
		float angle = Mathf.Atan(dist);
		Debug.Log ("Distance:"  + dist);
		Debug.Log ("Facteur correcteur:"  + angle);
		
		GameObject racine = GameObject.Find ("racine");
		
		angle = racine.transform.eulerAngles.y + angle;
		Debug.Log ("Rotation:"  + angle);
		racine.transform.eulerAngles = new Vector3(0,angle,0);
		 
		 p1 = Scale.getGameObjectLocation(mapPoint3);
		 p1bis = (GPSPoint)mapPoint3.GetComponent ("GPSPoint");
	
		 Debug.Log("coordonnees de test--> " + p1.lat + "," + p1.lng);
		 Debug.Log("coordonnees attendues --> " + p1bis.lat + "," + p1bis.lng);
		 
		Scale.placeGameObjectAt(TestLocalisation,48.678607,5.891568,25);
		Scale.placeGameObjectAt (TestCamera,48.678607,5.891568,40);
		
		
	}
	
}
