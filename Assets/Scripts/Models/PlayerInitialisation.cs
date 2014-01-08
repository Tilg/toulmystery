using UnityEngine;
using System.Collections;

public class PlayerInitialisation : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject goPlayer = GameObject.Find("Player");
		Transpose.placeGameObjectAt(goPlayer, 48.675313, 5.888597);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
