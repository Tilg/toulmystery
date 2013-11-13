using UnityEngine;
using System.Collections;

public class QuestionResponseModule : MonoBehaviour {

	public ArrayList responseList;
	
	public QuestionResponseModule(){
		
	}
	
	public void addElement( Response element ){
		responseList.Add(element);
	}
	
	public void removeElement(Response element){
		responseList.Remove(element);
	}
	
	
	//used to check all the response of all the ElementQuestionResponse who composed this module
	public bool checkAnswers(){
		/*
		foreach (Response element in responseList)
        {
            if (! element.checkAnswer) // if one of the answer is not correct we return false
				return false;
        }
		
		return true; //if all the response are correct, we pass the foreach and return true
		*/
		return false;
	}
}
