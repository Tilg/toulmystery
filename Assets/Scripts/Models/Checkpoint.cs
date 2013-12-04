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
	bool affiche = false;
	
	public Checkpoint (){}
	
	void Awake () {
		//The checkpoint are know listening for the starting notification comming from GameManager
		nsNotifCenter.addObserverSelectorNameObject(this,this.startCheckpoint,"GameManager",null);
		
		//on abonne ici le checkpoint aux differents modules
		nsNotifCenter.addObserverSelectorNameObject(this,this.loadNextModule,"QuestionResponseModule",null);
	}
	
	void Start () {
	}
	
	
	public void startCheckpoint(NSNotification aNotification){
		Hashtable dictionnaireDonneesRecues = aNotification.userInfo;
		Debug.Log(dictionnaireDonneesRecues["ID"]);
		
		if (dictionnaireDonneesRecues["ID"].Equals(this.id)){
			Debug.Log("c'est moi :"+ this.id);
		}
		else
			Debug.Log("c'est pas moi"+ this.id);
	}
	
	/** fonction apellée lors d'une notification de changement par un objet observé */
	public void loadNextModule(NSNotification aNotification){		
		Debug.Log(aNotification.name);
		
		switch (aNotification.name){
			
			case ("QuestionResponseModule"):
				Hashtable dictionnaireDonneesRecues = aNotification.userInfo;
				Debug.Log(dictionnaireDonneesRecues["id"]);
			
				loadModule(((int)dictionnaireDonneesRecues["id"]));
				break;
				
			default :
				Debug.Log("AUCUN CAS CORRESPONDANT");
				break;
		}
	}
	
	
	/** fonction qui active le chargement du module suivant **/
	public void loadModule(int id){
		affiche = true;
	}
	
	
	/** fonction qui compose la GUI sur l'Ipad **/
	void OnGUI () {
		/** si le flag "chargment du prochain module" est activé, on notifie l'utilisateur **/ 
		if (affiche){
			GUI.Label(new Rect (25, 50, 100, 30), "transition vers prochain module");
		}
	}
	
	/** destructeur qui permet de supprimer l'observer de la liste des objet a notifier si il est detruit**/
	~Checkpoint(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
}
