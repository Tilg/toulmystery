using UnityEngine;
using System.Collections;
using NotificationCenter;

public class GameManager : MonoBehaviour {
	
	
	public string[] checkpointList;
	
	private string moduleName = "GameManager";
	private NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
	private Hashtable additionnalDataTable;
	
	/* Used to initiate the link between objetct with observer/observable pattern */
	void Awake() {
		
		//The GameManager listen the checkpoint for the notification lauch by checpoint when it finish all it's game modules
		nsNotifCenter.addObserverSelectorNameObject(this,this.loadNextCheckpoint,"CheckpointModule",null);
	}
	
	/* fonction called at the beggining of the game */
	void Start() {
		
		//the gameManager notify all checkpoint that he want to lauch the first checkpoint
		if (checkpointList.Length > 0){
			additionnalDataTable = new Hashtable();
			additionnalDataTable.Add("ID", checkpointList[0]); // we add to the data table the id of the checkpoint to start
			
			Debug.Log("GAME MANAGER --> je demande un lancement du checkpoint  : "+checkpointList[0]);
			nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,additionnalDataTable);
		}	
	}
	
	/* fonction lauch when receiving a notification from a checkpoint telling that he is finished */
	public void loadNextCheckpoint(NSNotification aNotification){		
		
		Hashtable incommingData = aNotification.userInfo;
		
		if ( incommingData["finishedCheckpointID"] != null ){ // if the notification received from the checkpoint is a finish notification 
			
			Debug.Log("GAME MANAGER --> checkpoint signifie qu'il est termine : "+incommingData["finishedCheckpointID"]);
			
			int position = 0;
			
			// we look for the position in the checkpointlist of the finished checkpoint who send the notification
			for (int i=0; i< checkpointList.Length; i++){
				if ( checkpointList[i].Equals(incommingData["finishedCheckpointID"])){
					position =i;
				}
			} 
			
			if (position != (checkpointList.Length - 1)){// if the finished checkpoint was not the last checkpoint of this game, we lauch the next checkpoint
				loadCheckpoint(checkpointList[position+1]);
			}
			else
				Debug.Log("GAME MANAGER --> le jeu est termine !");
		
		}
	}
	
	
	/* fonction used to lauch a request to checkpoint with the id of the checkpoint that we want to lauch */
	public void loadCheckpoint(string id){
	
		additionnalDataTable = new Hashtable();
		additionnalDataTable.Add("ID", id); // we add to the data table the id of the checkpoint to start
		
		Debug.Log("GAME MANAGER --> je demande un lancement du checkpoint  : "+id);
		
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,additionnalDataTable);
	}
	
	
	/* destructor, remove the observer if destroyed */
	~GameManager(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
	
	public void bidon(){
		Debug.Log("test");
	}
	
}
