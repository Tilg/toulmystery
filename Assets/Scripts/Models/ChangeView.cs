using UnityEngine;
using System.Collections;

public class ChangeView : MonoBehaviour {

	private bool fpsView = true;
	
	public void change2FPSView(){

		// Set camera projection to perspective
		camera.orthographic = false;

		// Put the camera near the city floor
		// TODO: Change the coordinates on the Y axis (problem: referential)
		transform.position = new Vector3(transform.position.x,35.34215f,transform.position.z);

		// 0 Degrees rotation 
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.x = 0f;
		transform.eulerAngles = eulerAngles;

		this.fpsView = true;

		Debug.Log("SWITCH TO FPSVIEW - DONE");
	}
	
	public void change2SkyView(){

		// Set camera projection to ortographic
		camera.orthographic = true;

		// Put the camera on top
		// TODO: Change the coordinates on the Y axis (problem: referential)
		transform.position = new Vector3(transform.position.x,48.34215f,transform.position.z);

		// 90 degrees rotation
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.x = 90f;
		transform.eulerAngles = eulerAngles;

		this.fpsView = false;
		Debug.Log("FPS VIEW [" + fpsView + "]");
		Debug.Log("SWITCH TO SKYVIEW - DONE");
	}
	
	void OnGUI() {
		if (GUI.Button(new Rect(315, 10, 65, 30), "Camera"))
			if (fpsView)
				change2SkyView();
			else
				change2FPSView();
	}	
}
