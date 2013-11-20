using UnityEngine;
using System.Collections;

public class CalcScale : MonoBehaviour {

	// Use this for initialization
	void Start () {		
		print(Scale.getInfo());
		
		// Petit test
		GameObject TestLocalisation = GameObject.Find("TestLocalisation");
		Scale.placeGameobjectAt(TestLocalisation,48.676659,5.89013);
		// Scale.placeGameobjectAt(TestLocalisation,(48.67484+48.67779)/2,(5.889814+5.885423)/2);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
