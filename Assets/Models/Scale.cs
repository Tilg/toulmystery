using UnityEngine;
using System.Collections;

public static class Scale
{
	private static double _eQuatorialEarthRadius = 6378.1370D;
	private static double _d2r = (System.Math.PI / 180D);
	
	public static float ToRadian (float angle)
	{
		return Mathf.PI * angle / 180;
	}
		
	
	// Returns distance between 2 point (with a given lat and lng for each point)
	public static float getDistWorld (float latA, float lngA, float latB,  float lngB)
	{
		
		  	double dlong = (lngB - lngA) * _d2r;
		    double dlat = (latB - latA) * _d2r;
				
		    double a = System.Math.Pow(System.Math.Sin(dlat / 2D), 2D) + System.Math.Cos(latA * _d2r) * System.Math.Cos(latB * _d2r) * System.Math.Pow(System.Math.Sin(dlong / 2D), 2D);
		    double c = 2D * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1D - a));
		    double d = _eQuatorialEarthRadius * c;
		
		    return (float)d;
	}
	
	// // Returns distance between 2 GPS poins in Kilometers
	public static float getDistWorldInKm (GPSPoint a, GPSPoint b)
	{
		return getDistWorld(a.lat,a.lng,b.lat,b.lng);
	}
	
	// Returns distance between 2 GPS poins in Meters
	public static float getDistWorldInM (GPSPoint a, GPSPoint b)
	{
		return (getDistWorldInKm(a, b)) * 1000f;
	}
	
	// Returns the distance between 2 GameObject
	public static float getDistEucl (float x1, float x2, float y1, float y2)
	{
		return Mathf.Sqrt (Mathf.Pow ((float)(x1 - x2), 2) + Mathf.Pow ((float)(y1 - y2), 2));
	}
	
	
	// Returns the distance between 2 GameObject
	public static float getDistEuclBtwMapPoint (GameObject mapPoint1, GameObject mapPoint2)
	{
		float x1 = mapPoint1.transform.position.x;
		float x2 = mapPoint2.transform.position.x;
		float y1 = mapPoint1.transform.position.z;
		float y2 = mapPoint2.transform.position.z;
		
		return getDistEucl (x1, x2, y1, y2);
	}
	
	
	private static float ratioX ()
	{
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint)mapPoint1.GetComponent ("GPSPoint");
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		// mappoint 1
		float x1 = mapPoint1.transform.position.x;
		float lng1 = gpsPoint1.lng;

		
		// mappoint 2
		float x2 = mapPoint2.transform.position.x;
		float lng2 = gpsPoint2.lng;

		return (x1 - x2) / (lng1 - lng2);
		
	}
	
	private static float ratioZ ()
	{
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint)mapPoint1.GetComponent ("GPSPoint");
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		// mappoint 1
		float z1 = mapPoint1.transform.position.z;
		float lat1 = gpsPoint1.lat;

		
		// mappoint 2
		float z2 = mapPoint2.transform.position.z;
		float lat2 = gpsPoint2.lat;

		return (z1 - z2) / (lat1 - lat2);
		
	}
	
	private static float ratioLng ()
	{
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint)mapPoint1.GetComponent ("GPSPoint");
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		// mappoint 1
		float x1 = mapPoint1.transform.position.x;
		float lng1 = gpsPoint1.lng;

		
		// mappoint 2
		float x2 = mapPoint2.transform.position.x;
		float lng2 = gpsPoint2.lng;

		return (lng1 - lng2) / (x1 - x2) ;
		
	}
	
	private static float ratioLat ()
	{
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint)mapPoint1.GetComponent ("GPSPoint");
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		// mappoint 1
		float z1 = mapPoint1.transform.position.z;
		float lat1 = gpsPoint1.lat;

		
		// mappoint 3
		float z2 = mapPoint2.transform.position.z;
		float lat2 = gpsPoint2.lat;

		return  (lat1 - lat2) / (z1 - z2);
		
	}	
	
	// locate a gameObject in the real world (returns an array)
	public static float[] fromXZ2LatLng (float x, float z)
	{
		float[] coords = {.0f,.0f};
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		
		// mappoint 2
		float x2 = mapPoint2.transform.position.x;
		float z2 = mapPoint2.transform.position.z;
		
		float lng2 = gpsPoint2.lng;
		float lat2 = gpsPoint2.lat;
		
		coords [0] = (z - z2) * ratioLat () + lat2;
		coords [1] = (x - x2) * ratioLng () + lng2;
		
		Debug.Log ("latlng:" + coords [0] + "," + coords [1]);
		
		return coords;
	}
	
	// locate a gameObject in the real world (returns a GPSPoint)
	/*
	public static GPSPoint getGameObjectLocationGPSPoint (GameObject o)
	{
		float[] coordsArray = getGameObjectLocationArray(o); 
		return new GPSPoint (coordsArray [0], coordsArray [1]);
	}
	*/
	/*
	public static void rotateMap ()
	{
		GameObject racine = GameObject.Find ("racine");
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		
		
		GPSPoint A = (GPSPoint)mapPoint1.GetComponent ("GPSPoint"); // real coordonates		
		GPSPoint B = (GPSPoint)mapPoint2.GetComponent ("GPSPoint"); // real coordonates
		float[] C = fromXZ2LatLng(mapPoint2); // estimated coordonates
		
		Debug.Log ("coordonnees attendues --> " + B.lat + "," + B.lng);
		Debug.Log ("coordonnees de test--> " + C[0] + "," + C[1]);
		
		float a = Scale.getDistWorld(B.lat,B.lng, C[0], C[1]);
		float b = Scale.getDistWorld(A.lat,A.lng, C[0], C[1]);
		float c = Scale.getDistWorldInKm(A, B);
		
		Debug.Log ("Distance (BC): " + a);
		Debug.Log ("Distance (AC): " + b);
		Debug.Log ("Distance (AB): " + c);
		
		
		// Théorème d'Al Kashi
		float cosA = ((b * b) + (c * c) - (a * a)) / (2 * b * c); 
		Debug.Log ("cos A: " + cosA);
		float AcosA = Mathf.Acos (cosA);
		
		float angle = (180 * AcosA / Mathf.PI)/ 2f; 
		
		// Debug.Log ("Distance:" + dist);
		Debug.Log ("Angle (degres):" + angle);
		
		angle /= 2f;
		angle = Mathf.Abs(racine.transform.eulerAngles.y) + angle;
		
		
		Debug.Log ("Rotation de:" + angle);
		racine.transform.eulerAngles = new Vector3 (0, angle, 0);
		
		
	}
	*/
	
	// Set the position of a gameObject in Unity
	public static Vector3 fromLatLng2XZ(double lat, double lng)
	{
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		// mappoint 2
		float x2 = mapPoint2.transform.position.x;
		float lng2 = gpsPoint2.lng;
		float lat2 = gpsPoint2.lat;
		float z2 = mapPoint2.transform.position.z;
		
		float x = ((float)lng - lng2) * ratioX () + x2;
		float z = ((float)lat - lat2) * ratioZ () + z2;
	
		return new Vector3 (x, 0, z);
		
	}
	
	
	// locate a gameObject on the map with a given lat and lng
	public static void placeGameObjectAt(GameObject o, double lat, double lng)
	{ 
		// New game object position
		Vector3 v = fromLatLng2XZ(lat,lng);
		Debug.Log (v);
		v.y = o.transform.position.y;
		o.transform.position = v;
	}
	
	
}

