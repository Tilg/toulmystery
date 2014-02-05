using UnityEngine;
using NotificationCenter;
using System.Collections;

public class GPSManager : MonoBehaviour {

	public bool gpsInit = false;

	GameObject goPlayer;
	Player player;
	NSNotificationCenter ns = NSNotificationCenter.defaultCenter;
	public float lat;
	public float lng;

	void Start () {

		goPlayer = GameObject.Find ("Player");
		player = (Player)goPlayer.GetComponent("Player");

		#if PC
			Debug.Log("Sur PC, pas de GPS");
		#elif !PC

		// Start of GPS Location Service  
		Input.location.Start(5f); // Accuracy of 5 m
		int wait = 3000;

		// Checks if the GPS is enabled by the user (-> Allow location ) 
		if(Input.location.isEnabledByUser) {
			while(Input.location.status == LocationServiceStatus.Initializing && wait>0){
				wait--;
			}

			if (Input.location.status == LocationServiceStatus.Failed) {
				Debug.Log("Echec de l'optention des coordonées GPS.");
			}
			else {	
				gpsInit = true;
				// We start the timer to check each tick (every 3 sec) the current gps position
				InvokeRepeating("RetrieveGPSData", 0, 3);

			}
		}
		else {
			// Get the last known position
			//Debug.Log("Echec lors de l'activation du GPS.");
		}
		#endif  
	}

	void RetrieveGPSData() {
		//Debug.Log("Appel a GPSData"); 
		//LocationInfo currentGPSPosition = Input.location.lastData;

		Point currentGPSPosition = new Point( lat, lng);

		float currentPlayerLat = player.getPosLat();
		float currentPlayerLong =  player.getPosLng();

		//Debug.Log("currentPlayerLat : "+ currentPlayerLat);
		//Debug.Log("currentGPSPosition.latitude : "+ currentGPSPosition.latitude);

		//Debug.Log("currentPlayerLong : "+ currentPlayerLong);
		//Debug.Log("currentGPSPosition.longitude : "+ currentGPSPosition.longitude);

		if ( currentPlayerLat != currentGPSPosition.latitude || currentPlayerLong != currentGPSPosition.longitude ){
			//Debug.Log("le joueur a bougé"); 


			Hashtable additionnalDataTable = new Hashtable();

			additionnalDataTable.Add("newLatitude", currentGPSPosition.latitude); 
			additionnalDataTable.Add("newLongitude", currentGPSPosition.longitude);

			//notificate that the player move ( listen by the gameManager ) 
			ns.postNotificationNameObjectUserInfo("GPSManager",this,additionnalDataTable);

			//update player position data
			player.setPosLat(currentGPSPosition.latitude);
			player.setPosLng(currentGPSPosition.longitude);
			
			//Player position in reality is transposed on the map in unity 
			Transpose.placeGameObjectAt(goPlayer, this.player.getPosLat(), this.player.getPosLng());

		}
	}


	void OnGUI(){
		// Make a background box 
		GUI.Box(new Rect(10,10,500,100), "Position actuelle"); 
		GUI.Label (new Rect(20,30,300,50), "Latitude : " + this.player.getPosLat().ToString());
		GUI.Label (new Rect(20,45,300,50), "Longitude : " + this.player.getPosLng().ToString());
	}


	private class Point {
		public float latitude;
		public float longitude;

		public Point(float latP, float lngP){
			latitude = latP;
			longitude =lngP;
		}
	}
}