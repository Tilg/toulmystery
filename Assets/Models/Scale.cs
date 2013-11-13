using UnityEngine;
using System.Collections;

public static class Scale {

	public static double ToRadian(double angle){
	    return Mathf.PI * angle / 180.0;
	}
		
	private static double getDistWorld(GPSPoint a, GPSPoint b){
	    const double radius = 6371; // in km
	    double latitude = ToRadian(b.lat - a.lat) / 2;
	    double longitude = ToRadian(b.lng - a.lng) / 2;
	 
	    double x = Mathf.Sin((float)latitude) * Mathf.Sin((float)latitude) + Mathf.Sin((float)longitude) * Mathf.Sin((float)longitude) *
	                                                         Mathf.Cos((float)ToRadian(a.lat)) * Mathf.Cos((float)ToRadian(b.lat));
	    double y = 2 * Mathf.Atan2((float)Mathf.Sqrt((float)x), Mathf.Sqrt((float)(1 - x)));
	 
	    return radius * y;
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
	
	
	// Return a scale ratio for a latitude
	private static double getZRatio() {
		
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		
		GPSPoint gpsPoint1 = (GPSPoint) mapPoint1.GetComponent("GPSPoint");
		GPSPoint gpsPoint2 = (GPSPoint) mapPoint2.GetComponent("GPSPoint");
		
		int earthRad = 6371;
		
		// convert lat/long to radians
		double lat1 = ToRadian(gpsPoint1.lat);
		double lat2 = ToRadian(gpsPoint2.lat);
		double lng1 = ToRadian(gpsPoint1.lng);
		double lng2 = ToRadian(gpsPoint2.lng);
		
		// adjust position by radians
		lat1 -= 1.570795765134; // subtract 90 degrees (in radians)
		lat2 -= 1.570795765134; // subtract 90 degrees (in radians)
		
		// Calculus of estimate point
		double zlat1 =  (double) earthRad * Mathf.Sin((float)lat1) * Mathf.Sin((float)lng1); 
		double zlat2 =  (double) earthRad * Mathf.Sin((float)lat2) * Mathf.Sin((float)lng2);
		
		double z1 = mapPoint1.transform.position.z;
		double z2 = mapPoint2.transform.position.z;
		
		return (((z1/zlat1) + (z2/zlat2))/2) ;
	}
	
	// Return a scale ratio for a longitude
	private static double getXRatio() {
		
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		
		// float x = (float) earthRad * Mathf.Cos((float)lng) * Mathf.Sin(90 - (float)lat);
		
		int earthRad = 6371;
		
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
		double xlng1 =  (double) earthRad * Mathf.Sin((float)lat1) * Mathf.Cos((float)lng1); 
		double xlng2 =  (double) earthRad * Mathf.Sin((float)lat2) * Mathf.Cos((float)lng2);
		double x1 = mapPoint1.transform.position.x;
		double x2 = mapPoint2.transform.position.x;
		
		return (((x1/xlng1) + (x2/xlng2))/2) ;
	}
	
	// Convert a Z coordonate (Unity units) in a Latitude (in degrees)
	public static double ZAxis2Lat(double zCoord){
		// TODO: Finish this function
	}
	
	// Convert a X coordonate (Unity units) in a Longitude (in degrees)
	public static double XAxis2Lng(double xCoord){
		// TODO: Finish this function
	}
	
	// Convert a Latitude (in degrees) in a Z coordonate (Unity units)
	public static double Lat2Zaxis(double lat){
		// TODO: Finish this function
	}
	
	// Convert a Latitude (in degrees) in a X coordonate (Unity units)
	public static double Lng2Xaxis(double lng){
		// TODO: Finish this function
	}
	
	// locate a gameObject on the map with a given lat and lng
	public static void placeGameobjectAt(GameObject o, double lat, double lng){ 
		o.transform.position = new Vector3((float)Lng2Xaxis(lng),10,(float)Lat2Zaxis(lat));
	}
	
	// locate a gameObject in the real world (returns a GPSPoint)
	public static GPSPoint getGameobjectLocation(GameObject o){
		return new GPSPoint(
				ZAxis2Lat(o.transform.position.z),
				XAxis2Lng(o.transform.position.x)
			);
	}
	
	
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
