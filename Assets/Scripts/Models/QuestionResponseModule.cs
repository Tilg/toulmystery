using UnityEngine;
using System.Collections;
using NotificationCenter;

public class QuestionResponseModule : MonoBehaviour {
	
	private string moduleName = "QuestionResponseModule";
	public int id = 1;
	public string[] questions;
	public string[] responses;
	private string[] playerResponses;
	private Hashtable dictionnaireDonneeSup;
	
	public int Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}
	public string[] Questions {
		get {
			return this.questions;
		}
		set {
			questions = value;
		}
	}
	public string[] Responses {
		get {
			return this.responses;
		}
		set {
			responses = value;
		}
	}
	
	void Start(){
		dictionnaireDonneeSup = new Hashtable();
		dictionnaireDonneeSup.Add("id", id);		
	}	
	
	// fonction qui compose la GUI sur l'Ipad
	void OnGUI () {
		
		GUI.BeginGroup (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 300));
	
		// Make a background box
		GUI.Box(new Rect(0,0,200,300),"A vous de jouer ...");
				
		//add the question label
		GUI.Label(new Rect (25, 50, 100, 30), questions[0]);
		
		//add the textfield to get the answer
		playerResponses[0] = GUI.TextField (new Rect (25, 100, 100, 30), "");
		
		// add the button used to validate the answer
		if (GUI.Button (new Rect (25, 150, 100, 30), "valider")) {
			
			// This code is executed when the Button is clicked
			if ( playerResponses[0].Equals(responses[0])){
				//on affiche un petit label de felicitation
				GUI.Label(new Rect (25, 50, 100, 30),"bonne reponse");
				// on envoi un message au checkpoint pour lui préciser que le module est terminé
				NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
				nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,dictionnaireDonneeSup);
			}else
				GUI.Label(new Rect (25, 50, 100, 30), "mauvaise reponse");
		}
	
		GUI.EndGroup ();
		
	}
}
