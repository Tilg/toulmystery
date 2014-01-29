using UnityEngine;
using System.Collections;
using NotificationCenter;

public class GameManager : MonoBehaviour {
	
	
	public string[] checkpointsIDList;
	
	private string moduleName = "GameManager";
	private NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
	private Hashtable additionnalDataTable;
	private ArrayList checkpointsList;

	private ArrayList availabeCheckpointsList;

	private bool playerIsPlaying = false;
	
	/* Used to initiate the link between objetct with observer/observable pattern */
	void Awake() {
		
		checkpointsList = new ArrayList();
		availabeCheckpointsList = new ArrayList();

		//The GameManager listen the checkpoint
		nsNotifCenter.addObserverSelectorNameObject(this,this.RecordCheckpoint,"CheckpointModule",null);

		// The gameMangaer listen the GPSManager
		nsNotifCenter.addObserverSelectorNameObject(this,this.UpdateGpsData,"GPSManager",null);

	}
	
	void Start() {
		//the gameManager notify all checkpoint
		nsNotifCenter.postNotificationNameObjectUserInfo(moduleName,this,null);
		
	}

	public void UpdateGpsData(NSNotification aNotification){


		Debug.Log("GAME MANAGER --> notification changement coordonnée gps recues");	
		Debug.Log("GAME MANAGER --> etat de playerIsPlaying : "+playerIsPlaying);
		if ( ! playerIsPlaying ){ //if the player is not playing, we check if he is near to an active checkpoint 

			Hashtable additionnalDataTable = aNotification.userInfo;
			
			float newLat = (float) additionnalDataTable["newLatitude"];
			float newLong = (float) additionnalDataTable["newLongitude"];

			CheckForAvailableCheckpoint( newLat, newLong );
		}
	}



	public void RecordCheckpoint(NSNotification aNotification){	
		
		Checkpoint newCheckpoint =  (Checkpoint)aNotification.obj ;

		checkpointsList.Add(newCheckpoint);	
		
		// if all the checkpoints are registered
		if (checkpointsList.Count == checkpointsIDList.Length){
			// we lauch the first checkpoint

			Debug.Log("GAME MANAGER --> le dernier checkpoint s'est enregistré, le jeu peut commencer.");

			foreach (Checkpoint checkpointX in checkpointsList){

				Debug.Log("GAME MANAGER --> id du checkpoint  : "+ checkpointX.id);
				Debug.Log("GAME MANAGER --> id du premier checkpoint de la liste  : "+ checkpointsIDList[0]);

				if ( checkpointX.id.Equals(checkpointsIDList[0])){	
					Debug.Log("GAME MANAGER --> le checkpoint suivant est placé dans la liste des checkpoints activés : " + checkpointX.id );
					availabeCheckpointsList.Add(checkpointX);
				}
			}
		}
	}

	public void LoadNextCheckpoint(string  finishedCheckpointID){		
			
		playerIsPlaying = false; //the function is called by a finised checkpoint so the player is available 

		for (int i=0; i< availabeCheckpointsList.Count; i++){// we look for the position in the checkpointlist of the finished checkpoint

			Checkpoint tmp = (Checkpoint) availabeCheckpointsList[i];

			if ( finishedCheckpointID.Equals(tmp.id)){
				availabeCheckpointsList.Remove(tmp);
			}
		}

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


	public void CheckForAvailableCheckpoint( float latitudeParam, float longitudeParam){

		Debug.Log("GAME MANAGER --> checkForAvailableCheckpoint at : ");
		Debug.Log("latitudeParam : "+ latitudeParam);
		Debug.Log("longitudeParam : "+ longitudeParam);

		ArrayList availableAndInRangeCheckpointsList = new ArrayList();

		Debug.Log("nb de checkpoint dans la liste de checkpoint disponible : "+ availabeCheckpointsList.Count);

		foreach (Checkpoint checkpointX in availabeCheckpointsList){

			//we are calculating the distance between the coordonates of the player and the coordonates of the checkpointX
			float distanceBetweenPlayerAndCheckpoint = (Transpose.getDistWorld(latitudeParam,longitudeParam,checkpointX.latitude,checkpointX.longitude)*1000f); // getDistworld return a result in kilometers, *1000 to have the result in meters
			Debug.Log("distance entre le checkpoint "+ checkpointX.id + " est les coordonnées du joueur reçues du GPSManager : " + distanceBetweenPlayerAndCheckpoint);
			Debug.Log("distance d'activation du checkpoint : "+checkpointX.rangeInMeters);

			//if the distance is in the activation area of the checkpoint
			if ( distanceBetweenPlayerAndCheckpoint <= checkpointX.rangeInMeters ){
				availableAndInRangeCheckpointsList.Add(checkpointX);
				Debug.Log("le checkpoint est ajouté a la liste des checkpoint pouvant être lancé");
			} 
		}

		Debug.Log("taille de la liste des checkpoint disponible et dont on est a distance d'activation : " + availableAndInRangeCheckpointsList.Count);
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

	/* destructor, remove the observer if destroyed */
	~GameManager(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
	
}
