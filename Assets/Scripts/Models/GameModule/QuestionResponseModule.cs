using UnityEngine;
using System.Collections;
using NotificationCenter;

public class QuestionResponseModule : GameModule {
	
	public string[] questions;
	public string[] responses;
	private string[] playerResponses;
	private int idCurrentQuestion=0;

	public QuestionResponseModule(){}

	void Awake () {
		//we need to manually initialyze this table to be able to get te response with he GUI
		playerResponses = new string[questions.Length];
		for ( int i=0 ; i<questions.Length; i++){
			playerResponses[i] = "";
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
			                 constants.FRAME_FOR_GAME_HEIGHT * DeviceHandler.multiplicator),this.title);
				
				/********************* Question/Reponce fields **************************/ 
				
				if (questions.Length > 0 ) { // si le tableau de question contient au moin une question
					
					//add the question label
				GUI.Label(new Rect (25 * DeviceHandler.multiplicator,
				                    50 * DeviceHandler.multiplicator,
				                    constants.LABEL_WIDTH * DeviceHandler.multiplicator,
				                    constants.LABEL_HEIGHT * DeviceHandler.multiplicator), questions[idCurrentQuestion]);
				
					//add the textfield to get the answer
					//Debug.Log(playerResponses[idCurrentQuestion]);
				playerResponses[idCurrentQuestion] = GUI.TextField (new Rect (25 * DeviceHandler.multiplicator,
				                                                              100 * DeviceHandler.multiplicator,
				                                                              constants.TEXT_FIELD_WIDTH * DeviceHandler.multiplicator,
				                                                              constants.TEXT_FIELD_HEIGHT * DeviceHandler.multiplicator), playerResponses[idCurrentQuestion]);
					
					/********************* Response validator button **************************/ 

					// add the button used to validate the answer
				if (GUI.Button (new Rect (25 * DeviceHandler.multiplicator,
				                          150 * DeviceHandler.multiplicator,
				                          constants.BUTTON_WIDTH * DeviceHandler.multiplicator,
				                          constants.BUTTON_HEIGHT * DeviceHandler.multiplicator), "valider")) {
						
						// This code is executed when the Button is clicked
						if ( playerResponses[idCurrentQuestion].Equals(responses[idCurrentQuestion])){
							
							if (idCurrentQuestion < questions.Length-1) { // if we have other questions to diplay
								idCurrentQuestion++;
							}else{ // if it was the last question of this gameModule

								this.FinishModule();
							}
						}	
					}
				}
				else{ // if they are no question in this module, he is already finished

					this.FinishModule();
				}
			
			GUI.EndGroup ();
		}
	}
}
