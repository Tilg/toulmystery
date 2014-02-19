using UnityEngine;
using System.Collections;

// all texture used in HelpAndExplainaitionModule must me place in the "Assets/Resources" repository

public class HelpAndExplainationModule : GameModule{

	public string nomFeuilleInfo;
	private Texture2D myGUITexture;

	void Awake () {

		myGUITexture = Resources.Load(Constants.PATH_TO_HELP_AND_EXPLAINATION_SHEET + nomFeuilleInfo) as Texture2D;
	}

	// fonction used to construct the GUI
	void OnGUI () {


		//GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
		GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = GUI.skin.textField.fontSize = (int) (Constants.FONT_SIZE * DeviceHandler.multiplicator);

		if (display){
			
			GUI.BeginGroup (new Rect ((Screen.width / 2 - Constants.FRAME_FOR_GAME_WIDTH* DeviceHandler.multiplicator/2) ,
			                          (Screen.height / 2 - Constants.FRAME_FOR_GAME_HEIGHT* DeviceHandler.multiplicator/2),
			                          Constants.FRAME_FOR_GAME_WIDTH * DeviceHandler.multiplicator,
			                          Constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator));
			
			/********************* Title of the box **************************/ 

			
			// Make a background box
			GUI.Box(new Rect(0 * DeviceHandler.multiplicator,
			                 0 * DeviceHandler.multiplicator,
			                 Constants.FRAME_FOR_GAME_WIDTH * DeviceHandler.multiplicator,
			                 Constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator), "");
			
			/********************* sheet **************************/ 

			GUI.DrawTexture(new Rect(Constants.LEFT_GAP  * DeviceHandler.multiplicator,
			                         Constants.GAP_BETWEEN_COMPONENT* DeviceHandler.multiplicator,
			                         Constants.HELP_AND_EXPLAINATION_SHEET_WIDTH * DeviceHandler.multiplicator,
			                         Constants.HELP_AND_EXPLAINATION_SHEET_HEIGHT * DeviceHandler.multiplicator), myGUITexture, ScaleMode.StretchToFill, true, 10.0f);

			/********************* next button **************************/ 
				
			if (GUI.Button (new Rect ((Constants.FRAME_FOR_GAME_WIDTH* DeviceHandler.multiplicator / 2 - Constants.BUTTON_WIDTH* DeviceHandler.multiplicator/2),
			                          Constants.Y_BOTTOM_BUTTON * DeviceHandler.multiplicator,
			                          Constants.BUTTON_WIDTH * DeviceHandler.multiplicator,
			                          Constants.BUTTON_HEIGHT * DeviceHandler.multiplicator), "Fermer")) {
				this.FinishModule();
			}
			
			GUI.EndGroup ();
		}
	}

}
