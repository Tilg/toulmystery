using UnityEngine;
using System.Collections;

public static class Scale
{
	
	public static float EARTHRADIUS = 6371;
	
	public static float ToRadian (float angle)
	{
		return Mathf.PI * angle / 180;
	}
		
	private static float getDistWorld (GPSPoint a, GPSPoint b)
	{
		
		float latitude = ToRadian (b.lat - a.lat) / 2;
		float longitude = ToRadian (b.lng - a.lng) / 2;
	 
		float x = Mathf.Sin ((float)latitude) * Mathf.Sin ((float)latitude) + Mathf.Sin ((float)longitude) * Mathf.Sin ((float)longitude) *
	                                                         Mathf.Cos ((float)ToRadian (a.lat)) * Mathf.Cos ((float)ToRadian (b.lat));
		float y = 2 * Mathf.Atan2 ((float)Mathf.Sqrt ((float)x), Mathf.Sqrt ((float)(1 - x)));
	 
		return EARTHRADIUS * y;
	}
	
	// Returns distance between 2 GPS poins in Kilometers
	public static float getDistWorldInKm (GPSPoint a, GPSPoint b)
	{
		return getDistWorld (a, b);
	}
	
	// Returns distance between 2 GPS poins in Meters
	public static float getDistWorldInM (GPSPoint a, GPSPoint b)
	{
		return getDistWorld (a, b) * 1000;
	}
	
	// Returns the distance between 2 GameObject
	private static float getDistEucl (GameObject mapPoint1, GameObject mapPoint2)
	{
		float x1 = mapPoint1.transform.position.x;
		float x2 = mapPoint2.transform.position.x;
		float y1 = mapPoint1.transform.position.z;
		float y2 = mapPoint2.transform.position.z;
		
		return Mathf.Sqrt (Mathf.Pow ((float)(x1 - x2), 2) + Mathf.Pow ((float)(y1 - y2), 2));
	}
	
	// Returns the ratio estimated between the distance covered in Unity and the distance in reality
	public static float getScale ()
	{
		
		// Map points: equiv of GPS point in Unity;
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		
		GPSPoint gpsPoint1 = (GPSPoint)mapPoint1.GetComponent ("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");

		// Distances in real world
		float distG1G2 = getDistWorldInM (gpsPoint1, gpsPoint2);
		
		// Distances in Unity3D
		float distM1M2 = getDistEucl (mapPoint1, mapPoint2);

		// global scale
		return (distG1G2 / distM1M2);	
	}
	
	/*
	// Return a scale ratio for a longitude
	private static float getXRatio() {
		
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
	
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		// convert lat/long to radians
		float lat1 = ToRadian(gpsPoint1.lat);
		float lat2 = ToRadian(gpsPoint2.lat);
		float lng1 = ToRadian(gpsPoint1.lng);
		float lng2 = ToRadian(gpsPoint2.lng);
		
		// adjust position by radians
		lat1 -= 1.570795765134; // subtract 90 degrees (in radians)
		lat2 -= 1.570795765134; // subtract 90 degrees (in radians)
		
		// Calculus of estimate point
		float xlng1 =  (float) EARTHRADIUS * Mathf.Cos((float)lat1) * Mathf.Cos((float)lng1); 
		float xlng2 =  (float) EARTHRADIUS * Mathf.Cos((float)lat2) * Mathf.Cos((float)lng2);
		
		float x1 = mapPoint1.transform.position.x;
		float x2 = mapPoint2.transform.position.x;
		
		return (((x1/xlng1) + (x2/xlng2))/2) ;
	}
	
	
	// Return a scale ratio for a latitude
	private static float getZRatio() {
		
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		// convert lat/long to radians
		float lat1 = ToRadian(gpsPoint1.lat);
		float lat2 = ToRadian(gpsPoint2.lat);
		float lng1 = ToRadian(gpsPoint1.lng);
		float lng2 = ToRadian(gpsPoint2.lng);
		
		// adjust position by radians
		lat1 -= 1.570795765134; // subtract 90 degrees (in radians)
		lat2 -= 1.570795765134; // subtract 90 degrees (in radians)
		
		// Calculus of estimate point
		float zlat1 =  (float) EARTHRADIUS * Mathf.Cos((float)lat1) * Mathf.Sin((float)lng1); 
		float zlat2 =  (float) EARTHRADIUS * Mathf.Cos((float)lat2) * Mathf.Sin((float)lng2);
		
		float z1 = mapPoint1.transform.position.z;
		float z2 = mapPoint2.transform.position.z;
		
		return (((z1/zlat1) + (z2/zlat2))/2) ;
	}
		*/	


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

		
		// mappoint 2
		float z2 = mapPoint2.transform.position.z;
		float lat2 = gpsPoint2.lat;

		return  (lat1 - lat2) / (z1 - z2);
		
	}
	
	// locate a gameObject in the real world (returns a GPSPoint)
	public static GPSPoint getGameObjectLocation (GameObject o) 
	{
		GPSPoint coords = new GPSPoint();
		
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		float x = o.transform.position.x;
		float z = o.transform.position.z;
		
		// mappoint 2
		float x2 = mapPoint2.transform.position.x;
		float z2 = mapPoint2.transform.position.z;
		
		float lng2 = gpsPoint2.lng;
		float lat2 = gpsPoint2.lat;

		
		coords.lng = (x - x2) * ratioLng () + lng2;
		coords.lat = (z - z2) * ratioLat () + lat2;
		
		return coords;
	}
	
	// locate a gameObject on the map with a given lat and lng
	public static void placeGameObjectAt (GameObject o, double lat, double lng, double alt)
	{ 
				
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		Debug.Log (lat + "  " + lng);
		
		// mappoint 2
		float x2 = mapPoint2.transform.position.x;
		float lng2 = gpsPoint2.lng;
		float lat2 = gpsPoint2.lat;
		float z2 = mapPoint2.transform.position.z;
		
		float x = ((float)lng - lng2) * ratioX () + x2;
		float z = ((float)lat - lat2) * ratioZ () + z2;
		
		Debug.Log (x + " " + z);
		// New game object position
		o.transform.position = new Vector3 (x, (float)alt, z);
	}
	
	
	// Rotation 
	/*
	public static float getRotation(){
		
		float angle = 0;
		
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find ("MapPoint2");
		GameObject mapPoint3 = GameObject.Find ("MapPoint3");
		
		// récupération des coordonnées X/Z
		// X1/Z1
		float x1 = mapPoint1.transform.position.x;
		float z1 = mapPoint1.transform.position.z;
		
		// X2/Z2
		float x2 = mapPoint2.transform.position.x;
		float z2 = mapPoint2.transform.position.z;
		
		// X3/Z3
		float x3 = mapPoint3.transform.position.x;
		float z3 = mapPoint3.transform.position.z;	
		
		
		
		
		return angle;
	}
	*/
	
	public static string getInfo ()
	{
		
		// Map points: equiv of GPS point in Unity;
		GameObject mapPoint1 = GameObject.Find ("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		
		GPSPoint gpsPoint1 = (GPSPoint)mapPoint1.GetComponent ("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		// Distances in real world
		float distG1G2 = getDistWorldInM (gpsPoint1, gpsPoint2);
		
		// Distances in Unity3D
		float distM1M2 = getDistEucl (mapPoint1, mapPoint2);
		
		// sout
		string sout = "** Real distances between: \n";
		sout += "GPSPOINT1 and GPSPOINT2 :" + distG1G2 + "m \n";	
		sout += "\n";
		sout += "** Virtual distances between: \n";
		sout += "MAPPOINT1 and MAPPOINT2 :" + distM1M2 + "u \n";	
		sout += "\n";
		sout += "** Ratio: \n";
		sout += "R:" + distG1G2 / distM1M2 + "\n";
		sout += "\n";
		sout += "*** Estimate scale:\n";
		sout += "scale :" + (distG1G2 / distM1M2); 
		
		return sout;
	}
	
	
}

