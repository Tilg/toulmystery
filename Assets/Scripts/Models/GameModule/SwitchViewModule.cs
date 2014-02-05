using UnityEngine;
using NotificationCenter;
using System.Collections;

public class SwitchViewModule : GameModule {
	
	void OnGUI() {

		Debug.Log(" passe bien dans le onGUI de switchViewModule "  );

		Debug.Log(" etat de display :" + display);

		if (display){
			// Get the camera
			GameObject goCamera = GameObject.Find("playerView");
			ChangeView changeView = (ChangeView)goCamera.GetComponent ("ChangeView");

			if (GUI.Button(new Rect(315, 10, 65, 30), "Camera"))
			if (changeView.fpsView)
				changeView.change2SkyView();
			else
				changeView.change2FPSView();
		}
	}	
}
