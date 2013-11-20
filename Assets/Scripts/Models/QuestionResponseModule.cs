using UnityEngine;
using System.Collections;

public class QuestionResponseModule : MonoBehaviour {
	
	public int id;
	public ArrayList questionList;
	
	public QuestionResponseModule(int id){
		this.id = id;
		questionList = new ArrayList();
	}
	
	public void addElement( Response element ){
		questionList.Add(element);
	}
	
	public void removeElement(Response element){
		questionList.Remove(element);
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
	
	void OnGUI () {
		
	}
	
	void Start(){
		GameObject gameObjectQuestion = GameObject.Find("QuestionResponseModule");
	}
}
