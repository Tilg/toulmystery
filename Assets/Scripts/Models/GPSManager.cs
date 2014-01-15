using UnityEngine;

public class GPSManager : MonoBehaviour {

	public bool gpsInit = false;
	public float currentLat;
	public float currentLng;
	
	void Start () {

		#if PC
			Debug.Log("Sur PC, pas de GPS");
		#elif !PC

		// Start of GPS Location Service  
		Input.location.Start(0.5f); // Accuracy of 0.5 m
		int wait = 3000; // ms 

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

				// Every 3 sec the device position (and player position btw) is updated
				InvokeRepeating("updateDevicePosition", 0, 3);
			}
		}
		else {
			Debug.Log("Echec lors de l'activation du GPS.");
		}
		#endif  
	}

	void RetrieveGPSData() {

		// Get the last known position
		LocationInfo currentGPSPosition = Input.location.lastData;
		this.currentLat = currentGPSPosition.latitude;
		this.currentLng = currentGPSPosition.longitude;

		// Display (debug)
		Debug.Log("Derniere position connue{lat:" +  this.currentLat + ", lng:" + this.currentLng + "}");

	}

	void updateDevicePosition(){

		GameObject goPlayer = GameObject.Find ("Player");
		Player player = (Player)goPlayer.GetComponent("Player");

		// update player position data
		player.setPosLat(this.currentLat);
		player.setPosLat(this.currentLng);

		// Player position in reality is transposed on the map in unity 
		Transpose.placeGameObjectAt(goPlayer, this.currentLat, this.currentLng);
	}


	void OnGUI(){
		// Make a background box 
		GUI.Box(new Rect(10,10,150,60), "Position actuelle"); 
		GUI.Label (new Rect(20,30,100,20), "Latitude : " + this.currentLat.ToString());
		GUI.Label (new Rect(20,45,100,20), "Longitude : " + this.currentLng.ToString());
	}

}