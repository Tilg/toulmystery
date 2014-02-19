using UnityEngine;
using System.Collections;

public class LockAndValidModule : GameModule {

	private GameObject player;
	private GameObject map;
	private GameObject mapPoints;
	private GameObject vuforia;


	void Awake(){
		this.player = GameObject.Find("Player");
		this.map = GameObject.Find("map"); 
		this.mapPoints = GameObject.Find("MapPoints");
		this.vuforia = GameObject.Find("VuforiaAR"); 
	}

	void OnGUI () {

		if (display){

			// 1 - hide the root element (All the game)
			this.player.SetActive(false);
			this.map.SetActive(false);
			this.mapPoints.SetActive(false);

			// 2 - Enable all module's child
			foreach (Transform child in vuforia.transform) {
				child.gameObject.SetActive(true);
			}

			// 3 - Display  A text
			GUI.Box(new Rect(50,50,constants.FRAME_FOR_GAME_WIDTH,constants.FRAME_FOR_GAME_HEIGHT),this.title);




		}
	}

}
