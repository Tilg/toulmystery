using UnityEngine;
using System.Collections;

public class Route {

	private ArrayList checkpointList;
	private Checkpoint currentCheckpoint;
	
	public Route(){
		checkpointList = new ArrayList();	
	} 
	
	public void addCheckpoint(Checkpoint newCheckpoint){
		checkpointList.Add(newCheckpoint);
	}
	
	public void deleteCheckpoint(Checkpoint checkpoint){
		checkpointList.Remove(checkpoint);
	}
	
	public void lauchNextModule(){
		//TODO : on apelle le controleur en lui passant le current module afin qui l'ance le module suivant
	}
}
