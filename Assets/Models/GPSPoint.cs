using UnityEngine;
using System.Collections;

public class GPSPoint {
	
	private double lat;
	private double lng;
	
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

	public double getLat(){
		return this.lat;
	}
	
	
	public double getLng(){
		return this.lng;
	}
	
	public void setLat(double lat){
		this.lat = lat;
	}
	
	public void setLng(double lng){
		this.lng = lng;
	}
	
	
}
