using UnityEngine;
using System.Collections;
using NotificationCenter;

public class Checkpoint : MonoBehaviour{
	
	public string id; // checkpointID
	public float latitude;
	public float longitude;
	public float range; // this range is used to lauch the checkpoint if the user is closed enought of his latitude and longitude
	public string[] modulesList; //list of all the ID of the modules who composed this checkpoint
	
	private NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
	private string moduleName = "CheckpointModule";
	
	public Checkpoint (){}
	
	/* Used to initiate the link between objetct with observer/observable pattern */
	void Awake () {
		//The checkpoint are know listening for the starting notification comming from GameManager
		nsNotifCenter.addObserverSelectorNameObject(this,this.StartCheckpoint,"GameManager",null);
	}
	
	
	/* called when a notification is receive from the GameManager */
	public void StartCheckpoint(NSNotification aNotification){
		
		
		GameManager gm = (GameManager) aNotification.obj;
		gm.bidon();
		
		Hashtable incommingData = aNotification.userInfo; // the gameManager send in the table the ID of the requested checkpoint
		
		// All the ckeckpoint are receiving the notification from Game Manager, 
		if (incommingData["ID"].Equals(this.id)){ //if the checkpoint ID match with the reuested ID send by the game Manager
			
			Debug.Log("("+this.id+") CHECKPOINT --> c'est moi : "+ this.id);
			
			//The checkpoint are now listening the modules
			nsNotifCenter.addObserverSelectorNameObject(this,this.LoadNextModule,"QuestionResponseModule",null);
			
			if (modulesList.Length > 0){ // if the checkpoint have at least 1 modue to lauch , we lauch the first gameModule
				LoadModule(modulesList[0]);
			}
			else{ // if the checkpoint doesn't have a module, is he already finish
				this.CheckpointFinish();
			}	
		}
		else
			Debug.Log("("+this.id+") CHECKPOINT --> c'est pas moi : "+ this.id);
	}
	
	
	/* fonction called when receiving a notification from a game module notifying that he is finished */
	public void LoadNextModule(NSNotification aNotification){		
		
		Hashtable incommingData = aNotification.userInfo;
		Debug.Log("("+this.id+") CHECKPOINT --> gameModule signifie qu'il est termine : "+incommingData["finishedGameModuleID"]);	
		
		
		int position = 0;
			
		// we look for the position in the module list of the finished module who send the notification
		for (int i=0; i< modulesList.Length; i++){
			if ( modulesList[i].Equals(incommingData["finishedGameModuleID"])){
				position =i;
			}
		} 
		
		if (position == (modulesList.Length - 1)){// if the finished module was the last module of this checkpoint
			Debug.Log("("+this.id+") CHECKPOINT --> fin du checkpoint...");	
			this.CheckpointFinish();
		}
		else{
			Debug.Log("("+this.id+") CHECKPOINT --> Chargement prochain module...");	
			this.LoadModule(modulesList[position+1]);
		}
	}
	
	
	/* function used to lauch the next module */
	public void LoadModule(string moduleId){
		
		Hashtable additionnalDataTable = new Hashtable();
		additionnalDataTable.Add("lauchedGameModuleID", moduleId); // we add to the table the id off the module that we want to start
		
		Debug.Log("("+this.id+") CHECKPOINT --> chargement du module :" + moduleId);
		
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,additionnalDataTable);
	}
	
	public void CheckpointFinish(){
		
		// the ckeckpoint is no longer listening the gameModules
		//nsNotifCenter.removeObserverNameObject(this, "QuestionResponseModule",null);
		
		Hashtable additionnalDataTable = new Hashtable();
		additionnalDataTable.Add("finishedCheckpointID", id); // the finished checkpoint add  his ID to the dataTable
		
		Debug.Log("("+this.id+") CHECKPOINT --> notification de fin de checkpoint :" + id);
		
		Debug.Log("test 0");
		
		// the checkpoint send a ending notification to the game manager
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,additionnalDataTable);
		Debug.Log("test 1");
	} 
	
	/* destructor, remove the observer if destroyed */
	~Checkpoint(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
}
