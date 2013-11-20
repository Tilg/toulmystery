using UnityEngine;
using System.Collections;

[System.Serializable]
public class TextResponse : Response {

	public string correctAnswer;
	
	public TextResponse (int id, string correctAnswer)
	{
		this.id = id;
		this.correctAnswer = correctAnswer;
	}
	
	public bool checkAnswer(){
		// a completer
		return false;
	}
}
