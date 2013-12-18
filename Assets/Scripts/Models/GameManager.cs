using UnityEngine;
using System.Collections;
using NotificationCenter;

public class GameManager : MonoBehaviour {
	
	
	public string[] checkpointsIDList;
	
	private string moduleName = "GameManager";
	private NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
	private Hashtable additionnalDataTable;
	private ArrayList checkpointsList;
	
	/* Used to initiate the link between objetct with observer/observable pattern */
	void Awake() {
		
		checkpointsList = new ArrayList();

		//The GameManager listen the checkpoint
		nsNotifCenter.addObserverSelectorNameObject(this,this.RecordCheckpoint,"CheckpointModule",null);
	}
	
	void Start() {
		//the gameManager notify all checkpoint
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,null);
				
		// we lauch the first checkpoint
		this.LoadCheckpoint(checkpointsIDList[0]);
	}
	
	public void RecordCheckpoint(NSNotification aNotification){	
		
		Checkpoint newCheckpoint =  (Checkpoint)aNotification.obj ;
		
		Debug.Log("GAME MANAGER --> checkpoint enregistre : "  + newCheckpoint.id);	
		
		checkpointsList.Add(newCheckpoint);	
	}
	
	public void LoadNextCheckpoint(string  finishedCheckpointID){		
			
			int position = 0;
			
			for (int i=0; i< checkpointsIDList.Length; i++){// we look for the position in the checkpointlist of the finished checkpoint
				if ( checkpointsIDList[i].Equals(finishedCheckpointID)){
					position =i;
				}
			} 
			
			if (position != (checkpointsIDList.Length - 1)){// if the finished checkpoint was not the last checkpoint of this game, we lauch the next checkpoint
				this.LoadCheckpoint(checkpointsIDList[position+1]);
			}
			else
				Debug.Log("GAME MANAGER --> le jeu est termine !");	
	}
	
	/* fonction used to lauch a request to checkpoint with the id of the checkpoint that we want to lauch */
	public void LoadCheckpoint(string nextCheckpointID){
	
		Debug.Log("GAME MANAGER --> taille liste checkpoint : "+ checkpointsList.Count);
		
		foreach (Checkpoint checkpointX in checkpointsList){
			
			Debug.Log("GAME MANAGER --> id checkpoint : "+ checkpointX.id);
			Debug.Log("GAME MANAGER --> id checkpoint attendu : "+ nextCheckpointID);
			
			if ( checkpointX.id.Equals(nextCheckpointID)){
				Debug.Log("GAME MANAGER --> load checkpoint : "+ checkpointsIDList[0]);	
				checkpointX.Lauch(null);
			}
		}
	}
	
	/* destructor, remove the observer if destroyed */
	~GameManager(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
	
}
