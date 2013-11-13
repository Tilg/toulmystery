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
}
