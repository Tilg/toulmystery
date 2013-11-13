using UnityEngine;
using System.Collections;

public class GPSPoint : MonoBehaviour {
	
	public double lat = .0;
	public double lng = .0;
	
	public GPSPoint (double lat, double lng)
	{
		this.lat = lat;
		this.lng = lng;
	}
	
	private double degreesToRadians(double angle) {
   		return (Mathf.PI * angle / 180.0);
	}
	
	public void convertToRadians(){
		this.lat = degreesToRadians(this.lat);
		this.lng = degreesToRadians(this.lng);
	}
	
	public string toString(){
		return "GPSPoint[lat:" + this.lat + ", lng:"+ this.lng + "]";
	}
	
	public void print(){
		print(this.toString());
	}
	
	void Start () 
    {
       // TODO
    }
 
    void Update () 
    {
       // TODO
    }
	
	
	
}
