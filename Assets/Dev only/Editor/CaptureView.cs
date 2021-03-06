using UnityEngine;
using UnityEditor;
using System;
using System.IO;

using System.Collections;
using System.Collections.Generic;

public class CaptureView {


	[MenuItem("Toul/CaptureView %g")]
	public static void Do() {
		
		// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		// don't forget to set the window to a rect
		// and close scene tab
		
		
		Terrain terrain = Terrain.activeTerrain;
		float startX = terrain.transform.position.x;
 //   	float startZ = terrain.transform.position.z;
 		float maxX = startX+terrain.terrainData.size.x;
		float camSize = 2.0f*Camera.main.orthographicSize;
		float yPos = Camera.main.transform.position.y;
		
		float xPos = Camera.main.transform.position.x;
		float zPos = Camera.main.transform.position.z;
		int i=1;
				
				String aPath;
				//seems there's an error of 5 pixel in height
				Texture2D screen = new Texture2D( Screen.width, Screen.height+5, TextureFormat.RGB24, false );
				screen.ReadPixels( new Rect(0, 0, Screen.width, Screen.height+5), 0, 0 );
				screen.Apply();
				
//				Capture myScript =  (Capture) Camera.main.GetComponent(typeof(Capture));
//				Texture2D screen = myScript.CaptureScreen();
				
				
				var bytes = screen.EncodeToPNG();
				UnityEngine.Object.Destroy (screen);
				aPath = "/Volumes/Install/capture/";
				String baseName = "image";
				String fullPath=aPath+baseName+" "+i.ToString()+".png";
				while (File.Exists(fullPath)) {
					i++;
					fullPath=aPath+baseName+" "+i.ToString()+".png";
				}
				File.WriteAllBytes(fullPath, bytes);
		
	
		xPos+=camSize;
		if (xPos >= maxX) {
			xPos = startX;
			zPos+=camSize;
		}
		Camera.main.transform.position = new Vector3(xPos, yPos, zPos);
 		
		

	}
	
	
} // end of class
