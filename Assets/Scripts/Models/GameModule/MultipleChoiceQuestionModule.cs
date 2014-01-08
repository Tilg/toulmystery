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
			
			GUI.BeginGroup (new Rect (Screen.width / 2 - constants.FRAME_FOR_GAME_WIDTH/2,
			                          Screen.height / 2 - constants.FRAME_FOR_GAME_HEIGHT/2,
			                          constants.FRAME_FOR_GAME_WIDTH,
			                          constants.FRAME_FOR_GAME_HEIGHT));
			
			/********************* Title of the box **************************/ 
			
			// Make a background box
			GUI.Box(new Rect(0,0,constants.FRAME_FOR_GAME_WIDTH,constants.FRAME_FOR_GAME_HEIGHT), title);


			/********************* question **************************/ 

			GUI.Label(new Rect (25, 30,  constants.LABEL_WIDTH, constants.LABEL_HEIGHT), question);

			/********************* answer list **************************/ 

			if (responseType.Equals (typeDeReponse.image)) {
				selectedElement = GUI.SelectionGrid(new Rect(25, 90, constants.QCM_CHOICE_WIDTH, constants.QCM_CHOICE_HEIGHT), selectedElement, possibleImageResponse, numberOfElementInALine);
			}else{
				selectedElement = GUI.SelectionGrid(new Rect(25, 90, constants.QCM_CHOICE_WIDTH, constants.QCM_CHOICE_HEIGHT), selectedElement, listeReponsesPossibles, numberOfElementInALine);
			}


			/********************* next button **************************/ 
			
			if (GUI.Button (new Rect (25, 260, constants.BUTTON_WIDTH, constants.BUTTON_HEIGHT), "Valider")) {
				if ( selectedElement == numeroElementCorrect ){
					this.FinishModule();
				}
			}
			
			GUI.EndGroup ();
		}
	}
}
