using UnityEngine;
using NotificationCenter;
using System.Collections;

public class SwitchViewModule : GameModule {
	
	void OnGUI() {

		//Debug.Log(" passe bien dans le onGUI de switchViewModule "  );

		//Debug.Log(" etat de display :" + display);

		if (display){
			// Get the camera
			GameObject goCamera = GameObject.Find("playerView");
			ChangeView changeView = (ChangeView)goCamera.GetComponent ("ChangeView");

			if (GUI.Button(new Rect(315 * DeviceHandler.multiplicator, //x
			                        10 * DeviceHandler.multiplicator, //y
			                        65 * DeviceHandler.multiplicator, // width
			                        30 * DeviceHandler.multiplicator), "Camera")){ //height
			if (changeView.fpsView)
				changeView.change2SkyView();
			else
				changeView.change2FPSView();
			}
		}
	}	
}
