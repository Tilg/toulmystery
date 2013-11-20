using UnityEngine;
using System.Collections;

public static class Scale {
	
	private const double EARTHRADIUS = (6378.2492 + 6356.515) /2; // Clarke 1880 IGN
	
	public static double ToRadian(double angle){
	    return Mathf.PI * angle / 180.0;
	}
	
		
	private static double getDistWorld(GPSPoint a, GPSPoint b){
		
	    double latitude = ToRadian(b.lat - a.lat) / 2;
	    double longitude = ToRadian(b.lng - a.lng) / 2;
	 
	    double x = Mathf.Sin((float)latitude) * Mathf.Sin((float)latitude) + Mathf.Sin((float)longitude) * Mathf.Sin((float)longitude) *
	                                                         Mathf.Cos((float)ToRadian(a.lat)) * Mathf.Cos((float)ToRadian(b.lat));
	    double y = 2 * Mathf.Atan2((float)Mathf.Sqrt((float)x), Mathf.Sqrt((float)(1 - x)));
	 
	    return EARTHRADIUS * y;
	}
	
	// Returns distance between 2 GPS poins in Kilometers
	public static double getDistWorldInKm(GPSPoint a, GPSPoint b){
		return getDistWorld(a,b);
	}
	
	// Returns distance between 2 GPS poins in Meters
	public static double getDistWorldInM(GPSPoint a, GPSPoint b){
		return getDistWorld(a,b) * 1000;
	}
	
	// Returns the distance between 2 GameObject
	private static double getDistEucl(GameObject mapPoint1, GameObject mapPoint2){
		double x1 = mapPoint1.transform.position.x;
		double x2 = mapPoint2.transform.position.x;
		double y1 = mapPoint1.transform.position.z;
		double y2 = mapPoint2.transform.position.z;
		
		return Mathf.Sqrt(Mathf.Pow((float)(x1-x2),2) + Mathf.Pow((float)(y1-y2),2));
	}
	
	// Returns the ratio estimated between the distance covered in Unity and the distance in reality
	public static double getScale(){
		
		// Map points: equiv of GPS point in Unity;
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");

		// Distances in real world
		double distG1G2 = getDistWorldInM(gpsPoint1,gpsPoint2);
		
		// Distances in Unity3D
		double distM1M2 = getDistEucl(mapPoint1,mapPoint2);

		// global scale
		return (distG1G2 / distM1M2);	
	}
	
	/*
	// Return a scale ratio for a longitude
	private static double getXRatio() {
		
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
	
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		// convert lat/long to radians
		double lat1 = ToRadian(gpsPoint1.lat);
		double lat2 = ToRadian(gpsPoint2.lat);
		double lng1 = ToRadian(gpsPoint1.lng);
		double lng2 = ToRadian(gpsPoint2.lng);
		
		// adjust position by radians
		lat1 -= 1.570795765134; // subtract 90 degrees (in radians)
		lat2 -= 1.570795765134; // subtract 90 degrees (in radians)
		
		// Calculus of estimate point
		double xlng1 =  (double) EARTHRADIUS * Mathf.Cos((float)lat1) * Mathf.Cos((float)lng1); 
		double xlng2 =  (double) EARTHRADIUS * Mathf.Cos((float)lat2) * Mathf.Cos((float)lng2);
		
		double x1 = mapPoint1.transform.position.x;
		double x2 = mapPoint2.transform.position.x;
		
		return (((x1/xlng1) + (x2/xlng2))/2) ;
	}
	
	
	// Return a scale ratio for a latitude
	private static double getZRatio() {
		
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		// convert lat/long to radians
		double lat1 = ToRadian(gpsPoint1.lat);
		double lat2 = ToRadian(gpsPoint2.lat);
		double lng1 = ToRadian(gpsPoint1.lng);
		double lng2 = ToRadian(gpsPoint2.lng);
		
		// adjust position by radians
		lat1 -= 1.570795765134; // subtract 90 degrees (in radians)
		lat2 -= 1.570795765134; // subtract 90 degrees (in radians)
		
		// Calculus of estimate point
		double zlat1 =  (double) EARTHRADIUS * Mathf.Cos((float)lat1) * Mathf.Sin((float)lng1); 
		double zlat2 =  (double) EARTHRADIUS * Mathf.Cos((float)lat2) * Mathf.Sin((float)lng2);
		
		double z1 = mapPoint1.transform.position.z;
		double z2 = mapPoint2.transform.position.z;
		
		return (((z1/zlat1) + (z2/zlat2))/2) ;
	}
		*/
	
	
	// locate a gameObject on the map with a given lat and lng
	public static void placeGameobjectAt(GameObject o, double lat, double lng){ 
		
		/*
		// convert lat/long to radians
		lat = ToRadian(lat);
		lng = ToRadian(lng);
		
		// adjust position by radians
		lat -= 1.570795765134; // subtract 90 degrees (in radians)

		// calculus of estimate point (reality)
		double x =  (double) EARTHRADIUS * Mathf.Cos((float)lat) * Mathf.Cos((float)lng); 
		double z =  (double) EARTHRADIUS * Mathf.Cos((float)lat) * Mathf.Sin((float)lng);
		
		// calculus of estimate point (unity)
		x = x * getXRatio();
		z = z * getZRatio();
		*/
		
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		Debug.Log (lat + "  " +  lng);
		
		// mappoint 1
		 double x1 = mapPoint1.transform.position.x;
		 double lng1 = gpsPoint1.lng;
		double z1 = mapPoint1.transform.position.z;
		
		// mappoint 2
		 double x2 = mapPoint2.transform.position.x;
		 double lng2 = gpsPoint2.lng;
		double lat2 = gpsPoint2.lat;
		double z2 = mapPoint2.transform.position.z;
		
		double x = (lng-lng2) * ratioX() + x2;
		double z = (lat-lat2) * ratioZ() + z2;
		
		Debug.Log (x +" "+ z);
		// et the new game object position
		o.transform.position = new Vector3((float)x,20,(float)z);
	}
	
	
	// locate a gameObject in the real world (returns a GPSPoint)
	public static GPSPoint getGameobjectLocation(GameObject o){
		// TODO: finisih this function
		return new GPSPoint();
		
	}
	

	private static double ratioX() {
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		// mappoint 1
		 double x1 = mapPoint1.transform.position.x;
		 double lng1 = gpsPoint1.lng;

		
		// mappoint 2
		 double x2 = mapPoint2.transform.position.x;
		 double lng2 = gpsPoint2.lng;

		return (x1-x2)/(lng1-lng2);
		
	}
	
	private static double ratioZ() {
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		// mappoint 1
		 double z1 = mapPoint1.transform.position.z;
		 double lat1 = gpsPoint1.lat;

		
		// mappoint 2
		 double z2 = mapPoint2.transform.position.z;
		 double lat2 = gpsPoint2.lat;

		return (z1-z2)/(lat1-lat2);
		
	}
	
	/*
	
	private static double getZ() {
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		
	}
	*/
	
	
	public static string getInfo(){
		
		// Map points: equiv of GPS point in Unity;
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		// Distances in real world
		double distG1G2 = getDistWorldInM(gpsPoint1,gpsPoint2);
		
		// Distances in Unity3D
		double distM1M2 = getDistEucl(mapPoint1,mapPoint2);
		
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
		sout += "scale :" + (distG1G2 / distM1M2) ; 
		
		return sout;
	}
	
	
}
