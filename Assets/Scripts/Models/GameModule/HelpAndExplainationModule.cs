using UnityEngine;
using System.Collections;

// all texture used in HelpAndExplainaitionModule must me place in the "Assets/Resources/Textures/" repository

public class HelpAndExplainationModule : GameModule{

	public string nomFeuilleInfo;
	private Texture2D myGUITexture;

	void Awake () {

		string texture = "Assets/Resources/Textures/" + nomFeuilleInfo + ".png";
		myGUITexture = (Texture2D)Resources.LoadAssetAtPath(texture, typeof(Texture2D));
	}

	// fonction used to construct the GUI
	void OnGUI () {

		if (display){
			
			GUI.BeginGroup (new Rect ((Screen.width / 2 - constants.FRAME_FOR_GAME_WIDTH/2) * DeviceHandler.multiplicator,
			                          (Screen.height / 2 - constants.FRAME_FOR_GAME_HEIGHT/2) * DeviceHandler.multiplicator,
			                          constants.FRAME_FOR_GAME_WIDTH * DeviceHandler.multiplicator,
			                          constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator));
			
			/********************* Title of the box **************************/ 
			
			// Make a background box
			GUI.Box(new Rect(0 * DeviceHandler.multiplicator,
			                 0 * DeviceHandler.multiplicator,
			                 constants.FRAME_FOR_GAME_WIDTH * DeviceHandler.multiplicator,
			                 constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator), "");
			
			/********************* sheet **************************/ 

			GUI.DrawTexture(new Rect(0 * DeviceHandler.multiplicator,
			                         0 * DeviceHandler.multiplicator,
			                         constants.HELP_AND_EXPLAINATION_SHEET_WIDTH * DeviceHandler.multiplicator,
			                         constants.HELP_AND_EXPLAINATION_SHEET_HEIGHT * DeviceHandler.multiplicator), myGUITexture, ScaleMode.StretchToFill, true, 10.0f);

			/********************* next button **************************/ 
				
			if (GUI.Button (new Rect (25 * DeviceHandler.multiplicator,
			                          260 * DeviceHandler.multiplicator,
			                          constants.BUTTON_WIDTH * DeviceHandler.multiplicator,
			                          constants.BUTTON_HEIGHT * DeviceHandler.multiplicator), "Fermer")) {
				this.FinishModule();
			}
			
			GUI.EndGroup ();
		}
	}

}
