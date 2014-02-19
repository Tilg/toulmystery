using UnityEngine;
using System.Collections;

public class MultipleChoiceQuestionModule : GameModule {

	private int selectedElement = 0;

	public string question = "";

	public enum typeDeReponse {text, image};
	public typeDeReponse responseType;
	
	public string[] listeReponsesPossibles = new string[] {"", ""};

	public int numeroElementCorrect = 0; 

	private int numberOfElementInALine = 1;
	
	private Texture2D[] possibleImageResponse;


	void Awake(){


		if (responseType.Equals (typeDeReponse.image)) {

			if (listeReponsesPossibles.Length > 2) {
				numberOfElementInALine = (listeReponsesPossibles.Length/2);
			}

			possibleImageResponse = new Texture2D[listeReponsesPossibles.Length];

			for ( int i=0 ; i<listeReponsesPossibles.Length ; i++){
				possibleImageResponse[i] = Resources.Load(Constants.PATH_TO_MULITPLE_CHOICE_RESOURCES + listeReponsesPossibles[i]) as Texture2D;
			}
		}else{
			if (listeReponsesPossibles.Length > 8) {
				numberOfElementInALine = (listeReponsesPossibles.Length/8)+1;
			}
		}
	}
	
	// fonction used to construct the GUI
	void OnGUI () {

		//GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
		GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = GUI.skin.textField.fontSize = (int) (Constants.FONT_SIZE * DeviceHandler.multiplicator);
		
		if (display){
			
			GUI.BeginGroup (new Rect ((Screen.width / 2 - Constants.FRAME_FOR_GAME_WIDTH* DeviceHandler.multiplicator/2),
			                          (Screen.height / 2 - Constants.FRAME_FOR_GAME_HEIGHT* DeviceHandler.multiplicator/2),
			                          Constants.FRAME_FOR_GAME_WIDTH * DeviceHandler.multiplicator,
			                          Constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator));
			
			/********************* Title of the box **************************/ 
			
			// Make a background box
			GUI.Box(new Rect(0 * DeviceHandler.multiplicator,
			                 0 * DeviceHandler.multiplicator,
			                 Constants.FRAME_FOR_GAME_WIDTH * DeviceHandler.multiplicator,
			                 Constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator), title);


			/********************* question **************************/ 

			GUI.Label(new Rect (Constants.LEFT_GAP * DeviceHandler.multiplicator,
			                    Constants.GAP_BETWEEN_COMPONENT * DeviceHandler.multiplicator,
			                    Constants.LABEL_WIDTH * DeviceHandler.multiplicator,
			                    Constants.LABEL_HEIGHT * DeviceHandler.multiplicator), question);

			/********************* answer list **************************/ 

			if (responseType.Equals (typeDeReponse.image)) {
				selectedElement = GUI.SelectionGrid(new Rect(Constants.LEFT_GAP * DeviceHandler.multiplicator,
				                                             2*Constants.GAP_BETWEEN_COMPONENT * DeviceHandler.multiplicator + Constants.LABEL_HEIGHT * DeviceHandler.multiplicator,
				                                             Constants.QCM_CHOICE_WIDTH * DeviceHandler.multiplicator,
				                                             Constants.QCM_CHOICE_HEIGHT * DeviceHandler.multiplicator), selectedElement, possibleImageResponse, numberOfElementInALine);
			}else{
				selectedElement = GUI.SelectionGrid(new Rect(Constants.LEFT_GAP * DeviceHandler.multiplicator,
				                                             2*Constants.GAP_BETWEEN_COMPONENT  * DeviceHandler.multiplicator + Constants.LABEL_HEIGHT * DeviceHandler.multiplicator,
				                                             Constants.QCM_CHOICE_WIDTH * DeviceHandler.multiplicator,
				                                             Constants.QCM_CHOICE_HEIGHT * DeviceHandler.multiplicator), selectedElement, listeReponsesPossibles, numberOfElementInALine);
			}


			/********************* valider button **************************/ 
			
			if (GUI.Button (new Rect ((Constants.FRAME_FOR_GAME_WIDTH* DeviceHandler.multiplicator / 2 - Constants.BUTTON_WIDTH* DeviceHandler.multiplicator/2),
			                          Constants.Y_BOTTOM_BUTTON * DeviceHandler.multiplicator,
			                          Constants.BUTTON_WIDTH * DeviceHandler.multiplicator,
			                          Constants.BUTTON_HEIGHT * DeviceHandler.multiplicator), "Valider")) {
				if ( selectedElement == numeroElementCorrect ){
					this.FinishModule();
				}
			}
			
			GUI.EndGroup ();
		}
	}
}
