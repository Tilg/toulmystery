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

				string texture = "Assets/Resources/Textures/" + listeReponsesPossibles[i] + ".png";

				possibleImageResponse[i] = (Texture2D)Resources.LoadAssetAtPath(texture, typeof(Texture2D));
			}
		}else{
			if (listeReponsesPossibles.Length > 8) {
				numberOfElementInALine = (listeReponsesPossibles.Length/8)+1;
			}
		}
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
			                 constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator), title);


			/********************* question **************************/ 

			GUI.Label(new Rect (25 * DeviceHandler.multiplicator,
			                    30 * DeviceHandler.multiplicator,
			                    constants.LABEL_WIDTH * DeviceHandler.multiplicator,
			                    constants.LABEL_HEIGHT * DeviceHandler.multiplicator), question);

			/********************* answer list **************************/ 

			if (responseType.Equals (typeDeReponse.image)) {
				selectedElement = GUI.SelectionGrid(new Rect(25 * DeviceHandler.multiplicator,
				                                             90 * DeviceHandler.multiplicator,
				                                             constants.QCM_CHOICE_WIDTH * DeviceHandler.multiplicator,
				                                             constants.QCM_CHOICE_HEIGHT * DeviceHandler.multiplicator), selectedElement, possibleImageResponse, numberOfElementInALine);
			}else{
				selectedElement = GUI.SelectionGrid(new Rect(25 * DeviceHandler.multiplicator,
				                                             90 * DeviceHandler.multiplicator,
				                                             constants.QCM_CHOICE_WIDTH * DeviceHandler.multiplicator,
				                                             constants.QCM_CHOICE_HEIGHT * DeviceHandler.multiplicator), selectedElement, listeReponsesPossibles, numberOfElementInALine);
			}


			/********************* next button **************************/ 
			
			if (GUI.Button (new Rect (25 * DeviceHandler.multiplicator,
			                          260 * DeviceHandler.multiplicator,
			                          constants.BUTTON_WIDTH * DeviceHandler.multiplicator,
			                          constants.BUTTON_HEIGHT * DeviceHandler.multiplicator), "Valider")) {
				if ( selectedElement == numeroElementCorrect ){
					this.FinishModule();
				}
			}
			
			GUI.EndGroup ();
		}
	}
}
