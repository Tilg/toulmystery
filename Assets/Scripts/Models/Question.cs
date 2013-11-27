using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Question : MonoBehaviour {

	[SerializeField]
	public int id;
	
	[SerializeField]
	public string boxName; 
	
	[SerializeField]
	public string questionText; 
	
	[SerializeField]
	public string correctAnswer;
	
	[SerializeField]
	public string answer;
	
	[SerializeField]
	public string buttonText;
	
	public Question ()
	{
	}
	
	/*
	public void addResponse(Response response){
		responseList.Add(response);
	}
	
	
	public void removeResponse(Response response){
		responseList.Remove(response);
	}
	*/
	
	void OnGUI () {
		
		GUI.BeginGroup (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 300));
	
		// Make a background box
		GUI.Box(new Rect(0,0,200,300),boxName);
				
		//add the question label
		GUI.Label (new Rect (25, 50, 100, 30), questionText);
		
		//add the textfield to get the answer
		answer = GUI.TextField (new Rect (25, 100, 100, 30), "");
		
		// add the button used to validate the answer
		if (GUI.Button (new Rect (25, 150, 100, 30), buttonText)) {
			
			// This code is executed when the Button is clicked
			if ( answer.Equals(correctAnswer)){
				print ("bonne reponse");
			}else
				print ("mauvaise reponse");
		}
	
		GUI.EndGroup ();
		
	}
	
	
	void Start(){
		
		/*
		Object[] gameObjectQuestion = GameObject.FindGameObjectsWithTag ("Question");
		
		foreach (GameObject gameObject in gameObjectQuestion){
			Question questionScript = (Question) gameObject.GetComponent("Question");
			print (questionScript.label);
		}
		*/
		
		boxName = "A vous de jouez !!!";
		questionText = "2+2 = ?";
		correctAnswer = "4";
		answer ="";
		buttonText="valider";
	}
	
	public int Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}
	
	public string QuestionText {
		get {
			return this.questionText;
		}
		set {
			questionText = value;
		}
	}

	public string Answer {
		get {
			return this.answer;
		}
		set {
			answer = value;
		}
	}
}
