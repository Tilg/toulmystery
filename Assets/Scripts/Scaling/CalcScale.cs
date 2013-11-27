using UnityEngine;
using System.Collections;

public class CalcScale : MonoBehaviour {

	// Use this for initialization
	void Start () {		
		print(Scale.getInfo());
		
		// Petit test
		GameObject TestLocalisation = GameObject.Find("TestLocalisation");
		GameObject TestCamera = GameObject.Find ("TestCamera");
		
		// Scale.placeGameobjectAt(TestLocalisation,48.676659,5.89013,20);
		// Scale.placeGameobjectAt (TestCamera,48.676659,5.89013,40);
		Scale.placeGameObjectAt(TestLocalisation,48.678607,5.891568,25);
		Scale.placeGameObjectAt (TestCamera,48.678607,5.891568,40);
		
		// Scale.placeGameobjectAt(TestLocalisation,(48.67484+48.67779)/2,(5.889814+5.885423)/2);
		
	}
	
}
