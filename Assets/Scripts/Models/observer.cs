using UnityEngine;
using System.Collections;
using NotificationCenter;

public class observer : MonoBehaviour {
	
	public int[] tab;
	
	// Use this for initialization
	void Start () {
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		
		//enregistrement
		nsNotifCenter.addObserverSelectorNameObject(this,this.updateIfNotif,"test",null);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void updateIfNotif(NSNotification aNotification){
		Debug.Log(aNotification.name);
		
		Hashtable dicoRecu = aNotification.userInfo;
		Debug.Log(dicoRecu["id"]);
	}
	
	
	~observer(){
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		nsNotifCenter.removeObserver(this);
	}
	
}
