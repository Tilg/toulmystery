using UnityEngine;
using UnityEditor;
using System.Collections;

using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour {

	static Vector3 adjustPosition (Vector3 initialPos) {
		initialPos.y+=100.0f;
    	RaycastHit hit;
        Physics.Raycast (initialPos, -Vector3.up, out hit) ;	 
		return hit.point;
	}


	public static void makeaLineOfBarriere(GameObject bxx) {
		
		GameObject deb = null;
		GameObject fin = null;
		int nbElt=0; 
		List<GameObject> segments = new List<GameObject>();
		
		Component[] components = bxx.GetComponentsInChildren <Transform>(true);
		foreach (Component c in components) {
			if (c.transform.parent.gameObject==bxx) {
				if (c.gameObject.name=="Sphere1")  deb = c.gameObject;
				if (c.gameObject.name=="Sphere2")  fin = c.gameObject;
				if (c.gameObject.name.StartsWith ("Seg"))  {
					segments.Add(c.gameObject);
					nbElt++;
				}
			}
		}	
		
		Vector3 debPos = deb.transform.position;
		Vector3 finPos = fin.transform.position;
		debPos=adjustPosition (debPos);
		finPos=adjustPosition (finPos);
		Vector3 vLength = (finPos-debPos)/nbElt;

		int i;
		for (i=0;i <nbElt;i++) {
			GameObject barriere=segments[i] as GameObject;
			
			Vector3 pA = debPos+i*vLength;
			Vector3 pB = pA+vLength;
			pA=adjustPosition (pA);
			pB=adjustPosition (pB);

			MeshFilter mf  = barriere.GetComponentInChildren<MeshFilter>();
			float barriereLength = mf.sharedMesh.bounds.extents.x*2.0f;
		
			barriere.transform.position = pA; 
			Vector3 oldScale = barriere.transform.localScale;
			barriere.transform.localScale= new Vector3 (vLength.magnitude/barriereLength,oldScale.y,oldScale.z);
			
			Quaternion r=Quaternion.FromToRotation (new Vector3(-1,0,0), pB-pA);
			Vector3 eAngles = r.eulerAngles;
			eAngles.x=-eAngles.x;
			eAngles.y=0;
			eAngles.z=0;
			r=r*Quaternion.Euler(eAngles);
			barriere.transform.rotation = r;
			
			
		}

	}

	[MenuItem("Toul/Place Fences")]
	public static void Do() {
		
		GameObject barrieres = GameObject.Find ("Barrières");
		Component[] bArray = barrieres.GetComponentsInChildren <Component>();
		
		//loop through each BXX gameobject : represent one line of "barri√®re"
		foreach (Component b in bArray) {
			if (b.transform.parent!=null && b.transform.parent.name=="Barrières") {
				Debug.Log (b.name);
				makeaLineOfBarriere(b.gameObject);
			}
		}
	}
}
