using UnityEngine;
using System.Collections;
using NotificationCenter;

public class Checkpoint : MonoBehaviour{
	
	public string id; // checkpointID
	public float latitude;
	public float longitude;
	public float rangeInMeters; // this range is used to lauch the checkpoint if the user is closed enought of his latitude and longitude
	public string[] modulesIDList; //list of all the ID of the modules who composed this checkpoint
	
	private GameManager gameManager = null;
	private ArrayList gameModuleList;
	
	private string moduleName = "CheckpointModule";
	
	public Checkpoint (){}
	
	/* Used to initiate the link between objetct with observer/observable pattern */
	void Awake () {
		
		gameModuleList = new ArrayList();
		
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		
		//The checkpoint are know listening for the starting notification comming from GameManager
		nsNotifCenter.addObserverSelectorNameObject(this,this.RecordGameManager,"GameManager",null);
		nsNotifCenter.addObserverSelectorNameObject(this,this.RecordModule,"GameModule",null);
	}
	
	void Start(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		
		Hashtable additionnalDataTable = new Hashtable();
		additionnalDataTable.Add("GameModuleIdList", modulesIDList); // we add to the table the id off the module that we want to start
		
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,additionnalDataTable);

		// CheckPoint placement
	
		/* Fisrt: get all chekpoints (array) */
		GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

		/* After, for each GameObjecy Chekpoint... */
		foreach (GameObject goCheckpoint in checkpoints) {

			/* ... the instance of Checkpoint Component (script) */
			Checkpoint compoCheckpoint = (Checkpoint) goCheckpoint.GetComponent ("Checkpoint");

			if(compoCheckpoint.id == this.id){
				Transpose.placeGameObjectAt(goCheckpoint,compoCheckpoint.latitude ,compoCheckpoint.longitude);
				break;
			}

		}

	}
	
	/* Used to know the reference of the gameManager */
	public void RecordGameManager(NSNotification aNotification) {	
		gameManager = (GameManager) aNotification.obj;
	}


	/* Used to know the reference of the gameModule of this checkpoint */
	public void RecordModule(NSNotification aNotification){	
		GameModule newGameModule =  (GameModule) aNotification.obj ;
		
		foreach ( string id in modulesIDList){
			
			if (id.Equals(newGameModule.id)){ // if the gameModule who send the notification is a game module of this checkpoint
				
				gameModuleList.Add(newGameModule);
			}
		}		
	}


	/* this method is called to start a checkpoint */
	public void Lauch(string targetedModuleID){
		 

		if (targetedModuleID == null){
			targetedModuleID = modulesIDList[0];
		}

		foreach (GameModule moduleX in gameModuleList){
			if ( moduleX.id.Equals(targetedModuleID)){
				moduleX.BeginModule(this);
			}
		}
	}
	
	
	
	public void FindNextModule(string finishedModuleID){
		
		int position = 0;
			
		// we look for the position in the module list of the finished module
		for (int i=0; i< modulesIDList.Length; i++){
			if ( modulesIDList[i].Equals(finishedModuleID)){
				position =i;
			}
		} 
		
		if (position != (modulesIDList.Length - 1)){// if the finished module was not the last module of this checkpoint, we lauch the next module
			this.Lauch(modulesIDList[position+1]);
		}else{ // if the module who call this functio was the last of this checkpoint, we ask to the gameManager to lauch the next checkpoint
			gameManager.LoadNextCheckpoint(this.id);
		}
		
	}
	
	/* destructor, remove the observer if destroyed */
	~Checkpoint(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
}
