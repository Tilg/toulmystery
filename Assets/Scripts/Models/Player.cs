using UnityEngine;
using NotificationCenter;
using System.Collections;

public class Player : MonoBehaviour {

	Vector2 coords = new Vector2();

	// Determines the player location using the device GPS
	IEnumerator getCurrentLocation(){

		Input.location.Start();
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1) {
			// Timed out
			return false;
		}
		if (Input.location.status == LocationServiceStatus.Failed) {
			// Unable to determine device location"
			return false;
		} else {
			coords.x = Input.location.lastData.latitude;
			coords.y = Input.location.lastData.longitude;
		}
			
		Input.location.Stop();

		return true; 
	}
	
	// Places the player at the right place
	public void placePlayer(){
		// goPlay = GameObject Player (A set of GameObject)
		GameObject goPlayer = GameObject.Find("Player");
		
		// locate the player
		this.getCurrentLocation();
		
		// Player position in reality is transposed on the map in unity 
		Transpose.placeGameObjectAt(goPlayer, coords.x, coords.y);
	}
	
	// Use this for initialization
	void Start () {

		// Notification that the player strats to play
		NSNotificationCenter ns = NSNotificationCenter.defaultCenter;
		ns.postNotificationNameObjectUserInfo("player",this,null);

		this.placePlayer();
	}
	
	// Update is called once per frame
	void Update () {
		this.placePlayer();
	}

	// returns the player position (latitude) 	
	public float getPoslat(){
		return coords.x;
	}

	// returns the player position (longitude)
	public float getPoslng(){
		return coords.y;
	}

	// returns the player position (latitude + longitude) 	
	public Vector2 getPosition(){
		return coords;
	}
	
}
