using UnityEngine;
using System.Collections;

public class DeviceHandler : MonoBehaviour {

	// this multiplicator allows us to modify the size of the components in the different view regarding to the kind of device who run the game 
	public static int multiplicator;


	void Awake(){

		// the default multiplicator
		multiplicator = 1;



		// TODO calculer le coefficient multiplicator pour chaque device
	}
}
