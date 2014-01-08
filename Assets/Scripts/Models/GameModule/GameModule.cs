using UnityEngine;
using System.Collections;
using NotificationCenter;

public abstract class GameModule : MonoBehaviour{
	
	public string id;
	public string title;

	private string moduleName = "GameModule";
	private ArrayList checkpointsList;
	private Checkpoint currentCheckpoint;

	protected bool display = false;
	
	public GameModule(){}	

	void Awake () {
		
		checkpointsList = new ArrayList();
		
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.addObserverSelectorNameObject(this,this.RecordCheckpoint,"CheckpointModule",null);//The Module are know listening for the starting notification comming from GameManager
	}


	void Start(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter; // used to notify the checkpoints
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,null);
	}
	
	/* Used to know the reference of the checkpoint using this gameModule */
	public void RecordCheckpoint(NSNotification aNotification){	
		
		Checkpoint newCheckpoint =  (Checkpoint)aNotification.obj ;
		
		Hashtable additionnalDataTable = aNotification.userInfo;
		
		string[] moduleIdList = (string[]) additionnalDataTable["GameModuleIdList"];
		
		foreach ( string moduleID in moduleIdList){
			
			if (moduleID.Equals(this.id)){ // if the gameModule find his id in the module id send by the checkpoint, the checkpoint who send the notification contains this module
				
				if (! checkpointsList.Contains(newCheckpoint)){
					checkpointsList.Add(newCheckpoint);
				} 	
			}
		}		
	}
	
	
	public void BeginModule(Checkpoint callingCheckpoint){
		currentCheckpoint = callingCheckpoint;
		display = true;
	}


	//this methos is called when the module is finish (when all the questions are validated)
	public void FinishModule(){
		display=false;
		currentCheckpoint.FindNextModule(this.id);
	}

	/* destructor, remove the observer if destroyed */
	~GameModule(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
	
}
