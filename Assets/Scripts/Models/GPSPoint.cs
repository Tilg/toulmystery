using UnityEngine;
using System.Collections;

public class GPSPoint : MonoBehaviour {
	
	public float lat = 0;
	public float lng = 0;
	
	public GPSPoint (){}
	
	public GPSPoint (float lat, float lng)
	{
		this.lat = lat;
		this.lng = lng;
	}
	
	private float degreesToRadians(float angle) {
   		return (Mathf.PI * angle / 180);
	}
	
	public void convertToRadians(){
		this.lat = degreesToRadians(this.lat);
		this.lng = degreesToRadians(this.lng);
	}
	
}
