using UnityEngine;
using System.Collections;

public class SwitchViewModule : GameModule {
	
	void OnGUI() {

		//GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
		GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = GUI.skin.textField.fontSize = (int) (Constants.FONT_SIZE * DeviceHandler.multiplicator);

		//Debug.Log(" passe bien dans le onGUI de switchViewModule "  );

		//Debug.Log(" etat de display :" + display);

		if (display){
			// Get the camera
			GameObject goCamera = GameObject.Find("playerView");
			ChangeView changeView = (ChangeView)goCamera.GetComponent ("ChangeView");

			if (GUI.Button(new Rect((Screen.width / 2 - Constants.BUTTON_WIDTH* DeviceHandler.multiplicator/2),
			                        Constants.Y_BOTTOM_BUTTON * DeviceHandler.multiplicator - Constants.BUTTON_HEIGHT * DeviceHandler.multiplicator - Constants.GAP_BETWEEN_COMPONENT * DeviceHandler.multiplicator,
			                        Constants.BUTTON_WIDTH  * DeviceHandler.multiplicator, // width
			                        Constants.BUTTON_HEIGHT * DeviceHandler.multiplicator), "Camera")){ //height
			if (changeView.fpsView)
				changeView.change2SkyView();
			else
				changeView.change2FPSView();
			}


			if (GUI.Button (new Rect ((Screen.width / 2 - Constants.BUTTON_WIDTH* DeviceHandler.multiplicator/2),
			                          Constants.Y_BOTTOM_BUTTON * DeviceHandler.multiplicator,
			                          Constants.BUTTON_WIDTH * DeviceHandler.multiplicator,
			                          Constants.BUTTON_HEIGHT * DeviceHandler.multiplicator), "Fermer")) {
				this.FinishModule();
			}
		}
	}	
}
