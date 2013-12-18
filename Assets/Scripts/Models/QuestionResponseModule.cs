using UnityEngine;
using System.Collections;
using NotificationCenter;

public class QuestionResponseModule : MonoBehaviour {
	
	
	public string id;
	public string[] questions;
	public string[] responses;
	
	private string moduleName = "QuestionResponseModule";
	private string[] playerResponses;
	private int idCurrentQuestion=0;
	private bool display = false;
	
	private ArrayList checkpointsList;
	private Checkpoint currentCheckpoint;
	
	void Awake () {
		
		checkpointsList = new ArrayList();
		
		//we need to manually initialyze this table to be able to get te response with he GUI
		playerResponses = new string[questions.Length];
		for ( int i=0 ; i<questions.Length; i++){
			playerResponses[i] = "";
		}
		
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.addObserverSelectorNameObject(this,this.RecordCheckpoint,"CheckpointModule",null);//The Module are know listening for the starting notification comming from GameManager
	}
	
	void Start(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter; // used to nity the checkpoints
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
	
	
	// fonction used to construct the GUI
	void OnGUI () {

		if (display){
			
			GUI.BeginGroup (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 300));
	
				/********************* Title of the box **************************/ 
				
				// Make a background box
				GUI.Box(new Rect(0,0,200,300),"A vous de jouer ...");
				
				/********************* Question/Reponce fields **************************/ 
				
				if (questions.Length > 0 ) { // si le tableau de question contient au moin une question
					
					//add the question label
					GUI.Label(new Rect (25, 50, 100, 30), questions[idCurrentQuestion]);
				
					//add the textfield to get the answer
					Debug.Log(playerResponses[idCurrentQuestion]);
					playerResponses[idCurrentQuestion] = GUI.TextField (new Rect (25, 100, 100, 30), playerResponses[idCurrentQuestion]);
					
					/********************* Response validator button **************************/ 

					// add the button used to validate the answer
					if (GUI.Button (new Rect (25, 150, 100, 30), "valider")) {
						
						// This code is executed when the Button is clicked
						if ( playerResponses[idCurrentQuestion].Equals(responses[idCurrentQuestion])){
							
							Debug.Log("("+this.id+") GAME MODULE --> Reponse correcte");
							
							if (idCurrentQuestion < questions.Length-1) { // if we have other questions to diplay
								Debug.Log("("+this.id+") GAME MODULE --> Question terminee : "+ questions[idCurrentQuestion]);
								idCurrentQuestion++;
								Debug.Log("("+this.id+") GAME MODULE --> Nouvelle Question : "+ questions[idCurrentQuestion]);
							}else{ // if it was the last question of this gameModule
								display=false;
								this.FinishModule();
							}
							
						}else{
							Debug.Log("("+this.id+") GAME MODULE --> Reponse fausse !!!");
						}		
					}
				}
				else{ // if they are no question in this module, he is already finished
					this.FinishModule();
				}
			
			GUI.EndGroup ();
		}
	}
	
	//this methos is called when the module is finish (when all the questions are validated)
	public void FinishModule(){
		currentCheckpoint.FindNextModule(this.id);
	}
	
	/* destructor, remove the observer if destroyed */
	~QuestionResponseModule(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
}
