using UnityEngine;
using System.Collections;
using NotificationCenter;

public class GameManager : MonoBehaviour {
	
	
	public string[] checkpointList;
	
	private string moduleName = "GameManager";
	private NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
	private Hashtable additionnalDataTable;
	
	
	void Awake () {
		
		//The GameManager listen the checkpoint for the notification lauch by checpoint when it finish all it's game modules
		nsNotifCenter.addObserverSelectorNameObject(this,this.loadNextCheckpoint,"CheckpointModule",null);
		
		
	}
	
	// Use this for initialization
	void Start () {
		//the gameManager notify all checkpoint that he want to lauch the first checkpoint
		
		if (checkpointList.Length > 0){
			additionnalDataTable = new Hashtable();
			additionnalDataTable.Add("ID", checkpointList[0]); // we add to the data table the id of the firt checkpoint to start
			nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,additionnalDataTable);
		}
		
		
	}
	
	/** fonction lauch by receiving a notification from a checkpoint */
	public void loadNextCheckpoint(NSNotification aNotification){		

	}
	
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
