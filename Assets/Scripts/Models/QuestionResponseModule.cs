using UnityEngine;
using System.Collections;
using NotificationCenter;

public class QuestionResponseModule : MonoBehaviour {
	
	
	public string id;
	public string[] questions;
	public string[] responses;
	
	private string moduleName = "QuestionResponseModule";
	private string[] playerResponses;
	private Hashtable dictionnaireDonneeSup;
	private int idCurrentQuestion;
	
	private NSNotificationCenter nsNotifCenter;
	
	void Awake () {
		idCurrentQuestion = 0;
		
	}
	
	void Start(){
		// On initialise la hashtable contenant les informations supplémentaires passé lors des notifications
		dictionnaireDonneeSup = new Hashtable();
		dictionnaireDonneeSup.Add("id", id);
		
		// on notifie au checkpoint
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,dictionnaireDonneeSup);
		
		playerResponses = new string[questions.Length];
		for ( int i=0 ; i<questions.Length; i++){
			playerResponses[i] = "";
		}
	}	
	
	// fonction qui compose la GUI sur l'Ipad
	void OnGUI () {
		
		GUI.BeginGroup (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 300));
	
		/********************* Titre de la box contenant les questions **************************/ 
		
		// Make a background box
		GUI.Box(new Rect(0,0,200,300),"A vous de jouer ...");
		
		/********************* Composition de la Question/Reponce **************************/ 
		
		if (questions.Length > 0 ) { // si le tableau de question contient au moin une question
			
			//add the question label
			GUI.Label(new Rect (25, 50, 100, 30), questions[idCurrentQuestion]);
		
			//add the textfield to get the answer
			Debug.Log(playerResponses[idCurrentQuestion]);
			playerResponses[idCurrentQuestion] = GUI.TextField (new Rect (25, 100, 100, 30), playerResponses[idCurrentQuestion]);
			
			/********************* Bouton de validation de la réponse **************************/ 
			
			// add the button used to validate the answer
			if (GUI.Button (new Rect (25, 150, 100, 30), "valider")) {
				
				// This code is executed when the Button is clicked
				if ( playerResponses[idCurrentQuestion].Equals(responses[idCurrentQuestion])){
					
					//on affiche un petit label de felicitation
					GUI.Label(new Rect (25, 175, 100, 30),"bonne reponse");
					
					// on regarde si la question corante était la derniere de la liste de questions du module
					if (idCurrentQuestion < questions.Length) { // si c'est pas la derniere on affiche la question suivante
						idCurrentQuestion++;
					}else{
						// si c'était la dernière question du module
					NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter; // on notifie au listener que le module est terminé
					nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,dictionnaireDonneeSup);
					}
					
				}else{
					GUI.Label(new Rect (25, 175, 100, 30), "mauvaise reponse");
				}
					
			}
			
		}
		else{ // si le module ne contenait aucune question, on le valide par defaut et on notifie l'observer pour appeler le module suivant
			
			// on envoi un message au checkpoint pour lui préciser que le module est terminé
			nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,dictionnaireDonneeSup);
		}
		
		
	
		GUI.EndGroup ();
		
	}
}
