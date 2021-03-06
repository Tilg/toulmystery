using UnityEngine;
using System.Collections;

public class RollOver : MonoBehaviour {
	
	public TextAsset displayedStrings;
	static public Renderer itemHit;
	static Hashtable displayedStringsDict;

	static public Material[] itemHitMaterials;
	static public Material[] materialsHover;

	static void updateInfos (string aName){
		Menu.infos=(string) displayedStringsDict[aName];
	}
	
	
	void readStringDisplayed () {
		displayedStringsDict= new Hashtable();
		if (displayedStrings != null) {
			string[] lines=displayedStrings.text.Split("\n".ToCharArray());
			string[] values;
			for (int i = 0; i<lines.Length; i++) {
				if (!lines[i].StartsWith("//")) {
					values=lines[i].Split(';');
					displayedStringsDict[values[0]]=values[1];
				}
			}
		}
	}
	
	void Start () {
		itemHit=null;
		readStringDisplayed ();

		materialsHover = new Material[10];
		Material selected = (Material) Resources.Load("selectedMat", typeof(Material));
		
		for (int i = 0;  i<materialsHover.Length; i++) 
			materialsHover[i] = selected;
	}
	
	
	void UpdateOLD  () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000f)) {
    		//Debug.Log (hit.transform.tag);
    		if (hit.transform.tag=="Ilot") {
    			if (itemHit!=hit.transform.renderer) {
     				if (itemHit!=null) itemHit.material.color=Color.white;
    				itemHit=hit.transform.renderer;
    				itemHit.material.color=Color.red;
    				updateInfos (itemHit.name);
    			}
    		}
    		else {
   				if (itemHit!=null) {
   					itemHit.material.color=Color.white;
   					itemHit=null;
   					Menu.infos = null;
   				}
    		}
		} 
		else {
   				if (itemHit!=null) {
   					itemHit.material.color=Color.white;
   					itemHit=null;
  					Menu.infos = null;
   				}
		}
	}


	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000f)) {
    		//Debug.Log (hit.transform.tag);
    		if (hit.transform.tag=="Ilot") {
    			if (itemHit!=hit.transform.renderer) {
 					if (itemHit!=null) itemHit.materials = itemHitMaterials;
					
					itemHit=hit.transform.renderer;
					itemHitMaterials= hit.transform.renderer.materials;
					hit.transform.renderer.materials=materialsHover;

	   				updateInfos (itemHit.name);
    			}
    		}
    		else {
   				if (itemHit!=null) {
   					itemHit.materials= itemHitMaterials;
   					itemHit=null;
   					Menu.infos = null;
   				}
    		}
		} 
		else {
   				if (itemHit!=null) {
   					itemHit.materials= itemHitMaterials;
   					itemHit=null;
  					Menu.infos = null;
   				}
		}
	}

}


