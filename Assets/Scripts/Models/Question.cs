using UnityEngine;
using System.Collections;

public class Question : MonoBehaviour {

	public int id;
	public string label;
	public ArrayList responseList;
	
	
	public Question (int id, string label)
	{
		this.id = id;
		this.label = label;
		responseList = new ArrayList();
	}

	
	public void addResponse(Response response){
		responseList.Add(response);
	}
	
	
	public void removeResponse(Response response){
		responseList.Remove(response);
	}
}
