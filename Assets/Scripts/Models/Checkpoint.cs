using UnityEngine;
using System.Collections;
using NotificationCenter;

public class Checkpoint : MonoBehaviour{
	
	public string id; // id du checkpoint
	public float latitude;
	public float longitude;
	public float range; // distance a partir de laquelle le checkpoint va être activé
	public string[] modulesList; //liste de module qui vont composer ce checkpoint
	
	private string moduleName = "CheckpointModule"; // nom du module, sert a identifier le module lors des notifications observer/observable
	
	
	/*boolean servant de flag, va etre a false si la question n'est pas validé, si il est mis a true on affiche une notification
	 "bien joué" ou "mauvaise réponse en fonction de la reponse du joueur
	 */
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
