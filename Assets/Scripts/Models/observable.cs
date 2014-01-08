using UnityEngine;
using System.Collections;
using NotificationCenter;

public class observable : MonoBehaviour {
	
	public string name = "test";
	private Hashtable dictionnaire;
	
	
	// Use this for initialization
	void Start () {
	
		dictionnaire = new Hashtable();
		
		dictionnaire.Add("id", "1");
		
		NSNotificationCenter nsNotifCenter = NSNotificationCenter.defaultCenter;
		
		nsNotifCenter.postNotificationNameObjectUserInfo(name,this,dictionnaire);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
