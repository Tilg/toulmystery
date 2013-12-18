using UnityEngine;
using System.Collections;

public static class Transpose
{
	
	
	// Returns distance between 2 point (with a given lat and lng for each point)
	public static float getDistWorld (float latA, float lngA, float latB, float lngB)
	{
		double _eQuatorialEarthRadius = 6378.1370D;
		double _d2r = (System.Math.PI / 180D);
		
		
		double dlong = (lngB - lngA) * _d2r;
		double dlat = (latB - latA) * _d2r;
				
		double a = System.Math.Pow (System.Math.Sin (dlat / 2D), 2D) + System.Math.Cos (latA * _d2r) * System.Math.Cos (latB * _d2r) * System.Math.Pow (System.Math.Sin (dlong / 2D), 2D);
		double c = 2D * System.Math.Atan2 (System.Math.Sqrt (a), System.Math.Sqrt (1D - a));
		double d = _eQuatorialEarthRadius * c;
		
		return (float)d;
	}
	
	
	// Returns distance between 2 GPS poins in Kilometers
	public static float getDistWorldInKm (GPSPoint a, GPSPoint b)
	{
		return getDistWorld (a.lat, a.lng, b.lat, b.lng);
	}
	
	
	// Returns distance between 2 GPS poins in Meters
	public static float getDistWorldInM (GPSPoint a, GPSPoint b)
	{
		return (getDistWorldInKm (a, b)) * 1000f;
	}
	
	
	// used for the calculus in function fromLatLng2XZ() 
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
	
	
	// used for the calculus in function fromLatLng2XZ() 
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
	
	
	// used for the calculus in function fromXZ2LatLng() 
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
	
	
	// used for the calculus in function fromXZ2LatLng() 
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
	
	
	// convert a latitude and a longitude to a 2D vector (x,y)
	public static Vector2 fromLatLng2XZ (double lat, double lng)
	{
		GameObject mapPoint2 = GameObject.Find ("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint)mapPoint2.GetComponent ("GPSPoint");
		
		// mappoint 2
		float x2 = mapPoint2.transform.position.x;
		float z2 = mapPoint2.transform.position.z;
		
		float lng2 = gpsPoint2.lng;
		float lat2 = gpsPoint2.lat;

		
		float x = ((float)lng - lng2) * ratioX () + x2;
		float z = ((float)lat - lat2) * ratioZ () + z2;
	
		return new Vector2 (x, z);
	}
	
	
	// locate a gameObject on the map with a given lat and lng
	public static void placeGameObjectAt (GameObject o, double lat, double lng)
	{ 
	
		// New game object position
		Vector2 posXZ = fromLatLng2XZ (lat, lng);
		Vector3 position = new Vector3 ();
		
		position.x = posXZ.x;		
		
		position.y = o.transform.position.y;
		position.z = posXZ.y;
		
		o.transform.position = position;
	}
	
	
}

