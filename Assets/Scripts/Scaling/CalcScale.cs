using UnityEngine;
using System.Collections;

public class CalcScale : MonoBehaviour {

	// Use this for initialization
	void Start () {		
		print(Scale.getInfo());
		
		// Petit test
		GameObject TestLocalisation = GameObject.Find("TestLocalisation");
		Scale.placeGameobjectAt(TestLocalisation,48.675557,5.893718);
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
