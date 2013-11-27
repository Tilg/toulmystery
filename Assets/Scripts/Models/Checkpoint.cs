using UnityEngine;
using System.Collections;
using UnityEditor;
using NotificationCenter;

[CanEditMultipleObjects]
public class Checkpoint : MonoBehaviour{
	
	public int id;
	public string adress;
	public float latitude;
	public float longitude;
	public float range; 
	
	bool affiche = false;
	
	public Checkpoint (){}
	
	// fonction appelé une seule fois comme un constructeur, se lance avant le start
	void Awake () {
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		
		//on abonne ici le checkpoint aux differents modules
		nsNotifCenter.addObserverSelectorNameObject(this,this.loadNextModule,"QuestionResponseModule",null);	
	}
	
	void Start () {
	}
	
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
	
	~Checkpoint(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
	
	public void loadModule(int id){
		affiche = true;
	}
	
	// fonction qui compose la GUI sur l'Ipad
	void OnGUI () {
		
		if (affiche){
			GUI.Label(new Rect (25, 50, 100, 30), "transition vers prochain module");
		}
		
	}
	
}
