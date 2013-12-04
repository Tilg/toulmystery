using UnityEngine;
using System.Collections;
using NotificationCenter;

public class GestionnaireJeu : MonoBehaviour {
	
	
	public string[] checkpointList;
	private NSNotificationCenter nsNotifCenter;
	
	
	void Awake () {
		nsNotifCenter = NSNotificationCenter.defaultCenter;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
