using UnityEngine;
using System.Collections;

public class CalcScale : MonoBehaviour
{

	// Use this for initialization
	void Awake()
	{		
		// Petit test
		GameObject TestLocalisation = GameObject.Find ("TestLocalisation");
		GameObject TestCamera = GameObject.Find ("TestCamera");
		
		Scale.rotateMap();

		Scale.placeGameObjectAt (TestLocalisation, 48.677658,5.890704, 25);
		Scale.placeGameObjectAt (TestCamera, 48.677658,5.890704, 40);
		
		
	}
	
}
