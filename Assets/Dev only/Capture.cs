using UnityEngine;
using System.Collections;
using System;
using System.IO;


public class Capture : MonoBehaviour {

Terrain terrain;
float startX;
float startZ;
float yPos;
float xPos;
float zPos;

	// Use this for initialization
	void Start () {
		terrain = Terrain.activeTerrain;
		startX = terrain.transform.position.x;
    	startZ = terrain.transform.position.z;
		yPos = Camera.main.transform.position.y;
	
		float xPos = startX;
		float zPos = startZ;
		Camera.main.transform.position = new Vector3(xPos, yPos, zPos);
		Debug.Log (Screen.width+" "+Screen.height);
		
	}
	
	public Texture2D CaptureScreen () {
		Texture2D screen = new Texture2D( Screen.width, Screen.height+5, TextureFormat.RGB24, false );
		screen.ReadPixels( new Rect(0, 0, Screen.width, Screen.height+5), 0, 0 );
		screen.Apply();
		return screen;
	}
	
	// Update is called once per frame
//	void Update () {
////		if (zPos < maxZ) {
////			if (xPos < maxX) {
////	
////				String aPath;
////				var screen = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
////				screen.ReadPixels( new Rect(0, 0, Screen.width, Screen.height), 0, 0 );
////				screen.Apply();
////				var bytes = screen.EncodeToPNG();
////				UnityEngine.Object.Destroy (screen);
////				aPath = "/Volumes/Install/capture/";
////				String baseName = "image";
////				String fullPath=aPath+baseName+" "+i.ToString()+".png";
////				i++;
//////				File.WriteAllBytes(fullPath, bytes);
////		
////	
////	
////				Camera.main.transform.position = new Vector3(xPos, yPos, zPos);
////				xPos+=camSize;
////			}
////			xPos = startX;
////			zPos+=camSize;
////			Camera.main.transform.position = new Vector3(xPos, yPos, zPos);
////		}
//
//
//	}
}
