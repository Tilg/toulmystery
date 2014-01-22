using UnityEngine;
using System.Collections;
using NotificationCenter;

public class GameManager : MonoBehaviour {
	
	
	public string[] checkpointsIDList;
	
	private string moduleName = "GameManager";
	private NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
	private Hashtable additionnalDataTable;
	private ArrayList checkpointsList= new ArrayList();

	private ArrayList availabeCheckpointsList= new ArrayList();
	private Player player;

	private bool playerIsPlaying = false;
	public string initialisationXMLFile ="";
	
	/* Used to initiate the link between objetct with observer/observable pattern */
	void Awake() {
		
		/** if the user chose to initialize the Trajet with an xml file **/
		if (initialisationXMLFile != ""){
			LoadFromXML();
		}else{
			//The GameManager listen the checkpoint
			nsNotifCenter.addObserverSelectorNameObject(this,this.RecordCheckpoint,"CheckpointModule",null);
		}

		// The GameManager listen the player
		nsNotifCenter.addObserverSelectorNameObject(this,this.RecordPlayer,"player",null);
		

	}
	
	void Start() {
		//the gameManager notify all checkpoint
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,null);
		
	}
	
	public void RecordCheckpoint(NSNotification aNotification){	
		
		Checkpoint newCheckpoint =  (Checkpoint)aNotification.obj ;

		checkpointsList.Add(newCheckpoint);	
		
		// if all the checkpoints are registered
		if (checkpointsList.Count == checkpointsIDList.Length){
			// we lauch the first checkpoint

			foreach (Checkpoint checkpointX in checkpointsList){
				if ( checkpointX.id.Equals(checkpointsIDList[0])){	
					availabeCheckpointsList.Add(checkpointX);
				}
			}
		}
	}

	public void RecordPlayer(NSNotification aNotification){	
		player = (Player)aNotification.obj; // we used the notification to registered the player
	}
	
	public void LoadNextCheckpoint(string  finishedCheckpointID){		
			
		playerIsPlaying = false; //the function is called by a finised checkpoint so the plaer is available 
			
			int position = 0;
			
			for (int i=0; i< checkpointsIDList.Length; i++){// we look for the position in the checkpointlist of the finished checkpoint
				if ( checkpointsIDList[i].Equals(finishedCheckpointID)){
					position =i;
				}
			} 
			
			if (position != (checkpointsIDList.Length - 1)){// if the finished checkpoint was not the last checkpoint of this game, we lauch the next checkpoint
				this.ActiveCheckpoint(checkpointsIDList[position+1]);
			}
			else
				Debug.Log("GAME MANAGER --> le jeu est termine !");	
	}
	
	/* fonction used to lauch a request to checkpoint with the id of the checkpoint that we want to lauch */
	public void ActiveCheckpoint(string nextCheckpointID){
	
		foreach (Checkpoint checkpointX in checkpointsList){
			if ( checkpointX.id.Equals(nextCheckpointID)){	
				availabeCheckpointsList.Add(checkpointX);
			}
		}
	}

	/* fonction used to lauch a request to checkpoint with the id of the checkpoint that we want to lauch */
	public void LoadCheckpoint(Checkpoint nextCheckpoint){
		playerIsPlaying = true;
		nextCheckpoint.Lauch(null);
	}


	public void checkForAvailableCheckpoint( float latitudeParam, float longitudeParam){

		ArrayList availableAndInRangeCheckpointsList = new ArrayList();

		foreach (Checkpoint checkpointX in availabeCheckpointsList){

			//we are calculating the distance between the coordonates of the player and the coordonates of the checkpointX
			float distanceBetweenPlayerAndCheckpoint = Transpose.getDistWorld(latitudeParam,longitudeParam,checkpointX.latitude,checkpointX.longitude); 

			//if the distance is in the activation area of the checkpoint
			if ( distanceBetweenPlayerAndCheckpoint <= checkpointX.rangeInMeters ){
				availableAndInRangeCheckpointsList.Add(checkpointX);
			} 

			if (availableAndInRangeCheckpointsList.Count > 0){ // if a checkpoint can be lauch

				if (availableAndInRangeCheckpointsList.Count == 1){ // if we have just one checkpoint, we lauch it
					LoadCheckpoint((Checkpoint) availableAndInRangeCheckpointsList[0]);
				}else{
					// if we are more than one checkpoint who can be launch in the availableAndInRangeCheckpointsList list, we lauch the checkpoint with the smallest
					// id in checkpointsIDList

					int idOfTheFirstCheckpoint = 100;
					Checkpoint selectedCheckpoint = null;

					foreach (Checkpoint checkpointTMP in availableAndInRangeCheckpointsList){

						for (int i=0; i< checkpointsIDList.Length; i++){// we look for the position in the checkpointlist of the finished checkpoint
							if ( checkpointsIDList[i].Equals(checkpointTMP.id) && i<idOfTheFirstCheckpoint){
								idOfTheFirstCheckpoint =i;
								selectedCheckpoint = checkpointTMP;
							}
						}
					}
					LoadCheckpoint(selectedCheckpoint);
				}
			}
		}
	}

	void Update(){
		if ( ! playerIsPlaying ){ //if the player is not playing, we check if he is near to an active checkpoint 
			checkForAvailableCheckpoint( player.getPosLat(), player.getPosLng() );
		}
	}

	public void LoadFromXML(){
		InitTrajetXML.LoadXML(this);	
	}

	public string GetInitialisationXMLFile(){
		return initialisationXMLFile;
	}
	
	public ArrayList GetCheckpointsList(){
		return checkpointsList;
	}
	
	public void SetCheckpointsList(ArrayList listeCheckpoint){
		checkpointsList = listeCheckpoint;
	}


	/* destructor, remove the observer if destroyed */
	~GameManager(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
	
}
