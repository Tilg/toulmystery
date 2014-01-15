using UnityEngine;
using NotificationCenter;
using System.Collections;

public class Player : MonoBehaviour {

	private float posLat;
	private float posLng;
	GameObject goPlayer;

	// Determines the player location using the device GPS
	void Start() {
		// Notification: the player starts to play
		NSNotificationCenter ns = NSNotificationCenter.defaultCenter;
		ns.postNotificationNameObjectUserInfo("player",this,null);
	}
	

	// set the player position (latitude) 	
	public void setPosLat(float posLat){
		this.posLat = posLat;
	}
	
	// set the player position (longitude)
	public void setPosLng(float posLng){
		this.posLng = posLng;
	}
	
	// returns the player position (latitude) 	
	public float getPosLat(){
		return posLat;
	}

	// returns the player position (longitude)
	public float getPosLng(){
		return posLng;
	}

}
