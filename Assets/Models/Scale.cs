using UnityEngine;
using System.Collections;

public class Scale {

	private GPSPoint gpsPoint1;
	private GPSPoint gpsPoint2;
	private GPSPoint gpsPoint3;
	
	public Scale (GPSPoint gpsPoint1, GPSPoint gpsPoint2, GPSPoint gpsPoint3)
	{
		this.gpsPoint1 = gpsPoint1;
		this.gpsPoint2 = gpsPoint2;
		this.gpsPoint3 = gpsPoint3;
	}
	
	public static double ToRadian(double angle){
	    return Mathf.PI * angle / 180.0;
	}
		
	public static double getDistWorld(GPSPoint a, GPSPoint b){
	    const double radius = 6371 * 1000; // in km
	    double latitude = ToRadian(b.getLat() - a.getLat()) / 2;
	    double longitude = ToRadian(b.getLng() - a.getLng()) / 2;
	 
	    double x = Mathf.Sin((float)latitude) * Mathf.Sin((float)latitude) + Mathf.Sin((float)longitude) * Mathf.Sin((float)longitude) *
	                                                         Mathf.Cos((float)ToRadian(a.getLat())) * Mathf.Cos((float)ToRadian(b.getLat()));
	    double y = 2 * Mathf.Atan2((float)Mathf.Sqrt((float)x), Mathf.Sqrt((float)(1 - x)));
	 
	    return radius * y;
	}	

	
	private double getDistEucl(GameObject mapPoint1, GameObject mapPoint2){
		double x1 = mapPoint1.transform.position.x;
		double x2 = mapPoint2.transform.position.x;
		double y1 = mapPoint1.transform.position.z;
		double y2 = mapPoint2.transform.position.z;
		
		return Mathf.Sqrt(Mathf.Pow((float)(x1-x2),2) + Mathf.Pow((float)(y1-y2),2));
	}
	
	// returns the ratio estimated between the distance covered in Unity and the distance in reality
	public double getScale(){
		
		// Map points: equiv of GPS point in Unity;
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		GameObject mapPoint3 = GameObject.Find("MapPoint3"); 
		
		// Distances in real world
		double distG1G2 = getDistWorld(gpsPoint1,gpsPoint2);
		double distG2G3 = getDistWorld(gpsPoint2,gpsPoint3);
		double distG3G1 = getDistWorld(gpsPoint3,gpsPoint1);
		
		// Distances in Unity3D
		double distM1M2 = getDistEucl(mapPoint1,mapPoint2);
		double distM2M3 = getDistEucl(mapPoint2,mapPoint3);
		double distM3M1 = getDistEucl(mapPoint3,mapPoint1);
		
		// 3 Ratios (each edge of the triangle)
		double r1 = distG1G2 / distM1M2;	
		double r2 = distG2G3 / distM2M3;	
		double r3 = distG3G1 / distM3M1;	
		
		return ((r1+r2+r3)/3);
	}
	
	public string getInfo(){
		
		// Map points: equiv of GPS point in Unity;
		GameObject mapPoint1 = GameObject.Find("MapPoint1"); 
		GameObject mapPoint2 = GameObject.Find("MapPoint2"); 
		GameObject mapPoint3 = GameObject.Find("MapPoint3"); 
		
		// Distances in real world
		double distG1G2 = getDistWorld(gpsPoint1,gpsPoint2);
		double distG2G3 = getDistWorld(gpsPoint2,gpsPoint3);
		double distG3G1 = getDistWorld(gpsPoint3,gpsPoint1);
		
		// Distances in Unity3D
		double distM1M2 = getDistEucl(mapPoint1,mapPoint2);
		double distM2M3 = getDistEucl(mapPoint2,mapPoint3);
		double distM3M1 = getDistEucl(mapPoint3,mapPoint1);
		
		// 3 Ratios (each edge of the triangle)
		double r1 = distG1G2 / distM1M2;	
		double r2 = distG2G3 / distM2M3;	
		double r3 = distG3G1 / distM3M1;	
		
		// sout
		string sout = "** Real distances between: \n";
		sout += "GPSPOINT1 and GPSPOINT2 :" + distG1G2 + "m \n";	
		sout += "GPSPOINT2 and GPSPOINT3 :" + distG2G3 + "m \n";	
		sout += "GPSPOINT3 eand GPSPOINT1 :" + distG3G1 + "m \n";
		sout += "\n";
		sout += "** Virtual distances between: \n";
		sout += "MAPPOINT1 and MAPPOINT2 :" + distM1M2 + "u \n";	
		sout += "MAPPOINT2 and MAPPOINT3 :" + distM2M3 + "u \n";	
		sout += "MAPPOINT3 and MAPPOINT1 :" + distM3M1 + "u \n";
		sout += "\n";
		sout += "** Ratios: \n";
		sout += "R1 :" + distG1G2 / distM1M2 + "\n";	
		sout += "R2 :" + distG2G3 / distM2M3 + "\n";
		sout += "R3 :" + distG3G1 / distM3M1 + "\n";
		sout += "\n";
		sout += "*** Estimate scale:\n";
		sout += "scale :" + ((r1+r2+r3)/3) ; 
		
		return sout;
	}
	
	
}
