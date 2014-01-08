using UnityEngine;
using System.Collections;

public class FPSMoveHead : MonoBehaviour {

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			float rotationX = transform.localEulerAngles.x-Input.GetAxis("Mouse Y") * sensitivityX;
			float rotationY = transform.localEulerAngles.y+Input.GetAxis("Mouse X") * sensitivityY;
			if (rotationX < -180f) rotationX+=360f;
			if (rotationX > +180) rotationX-=360f;
			rotationX = Mathf.Clamp (rotationX, minimumY, maximumY);
			
			transform.localRotation=Quaternion.Euler (rotationX, rotationY, 0f);
		}
	}
}