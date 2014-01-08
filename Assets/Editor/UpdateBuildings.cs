using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class UpdateBuildingsMaterials {
	
	public static Hashtable materialsDict;
	
	// we assume all materials are located in "Assets/Materiaux"
	// material are loaded only once
	public static Material GetMaterialWithName (string aName) {
		Material o = materialsDict[aName] as Material;
		if (null != o) return o;
		o = AssetDatabase.LoadAssetAtPath ("Assets/Materiaux/"+aName+".mat", typeof(Material)) as Material;
		if (null != o) materialsDict[aName] = o;
		else EditorUtility.DisplayDialog("Error", "Material \""+aName+"\" not found.", "Cancel");
		return o;
	}
	
	public static string GetIlotName (Component c) {
		Transform t;
		//if (c.GetType()== typeof(Transform)) t = c as Transform;
		//else 
		t = c.gameObject.transform;
		while (t.parent.name != "Ilots") {
			t=t.parent;
		}
		return t.name;
	}
	
	public static GameObject GetIlotGameObject (Component c) {
		Transform t;
		t = c.gameObject.transform;
		while (t.parent.parent.name != "Ilots") {
			t=t.parent;
		}
		return t.gameObject;
	}
	
	public static bool CheckMaterialName (string matName, string textureStart) {
		if (matName.StartsWith(textureStart)) return true;
		Debug.Log (textureStart);
		EditorUtility.DisplayDialog("Error", "Invalid texture name \""+matName+"\".", "Cancel");
		return false;
	}
	
	public static bool IsMaterialValid (Object o) {
		return AssetDatabase.GetAssetPath(o).StartsWith("Assets/Materiaux/");
	}
	
	public static void AddCollider (Renderer mf) { // add a collider if it does not have one
		GameObject go = GetIlotGameObject (mf);
   		MeshCollider objCollider = go.collider as MeshCollider;
   	 	if (objCollider == null) go.AddComponent<MeshCollider>();

		// add the mesh used for collider : needed for FPS Walk
		if (go.name.StartsWith ("MUR")) {
			objCollider = go.collider as MeshCollider;
 			if (objCollider.sharedMesh==null) {
				MeshFilter aMeshFilter = go.GetComponentInChildren<MeshFilter>();
				objCollider.sharedMesh = aMeshFilter.sharedMesh;
			}
		}
	}
	
	public static void AddScript (Renderer mf, string scriptName) {
		GameObject go = GetIlotGameObject (mf);		
		if (go.name.StartsWith ("_")) {
				MonoBehaviour aScript = go.GetComponent<MonoBehaviour>();
				if (null == aScript) go.AddComponent(scriptName);
		}
	}


	public static void SetTag (Renderer mf, string tagName) { 
		GameObject go = GetIlotGameObject (mf);		
		if (go.name.StartsWith ("_")) {
				go.tag = tagName;
		}
	}

	public static void UpdateMaterial (Renderer mf) {
		
		string textureNameStartWith = GetIlotName(mf)+"-"; // begining of all texture name for the current ilot
		textureNameStartWith=textureNameStartWith.ToLower();
		string newMaterialName;
		Material[] newMaterials;
		newMaterials = new Material[mf.sharedMaterials.Length];
		int i=0;
		bool hasChanged=false;
		foreach (Material mat in mf.sharedMaterials) {
			newMaterials[i]=mat;		
			if (! IsMaterialValid(mat)) {
				if (CheckMaterialName (mat.name, textureNameStartWith)) {
					newMaterialName = mat.name.Remove(0,textureNameStartWith.Length);
					
					if (newMaterialName=="rougeouverture") newMaterialName="cheminee";
					
					newMaterials[i] = GetMaterialWithName(newMaterialName);
					hasChanged = true;
				}
				else Debug.Log(mf.name);
			}
			i++;		
		}
		if (hasChanged) mf.materials=newMaterials;
	}

	[MenuItem("Toul/Update Buildings %u")]
	public static void Do() {
 		GameObject ilots = GameObject.Find("Ilots");
	   	if (ilots == null) {
       		 EditorUtility.DisplayDialog("Error", "Ilots not found.", "Cancel");
        	 return;
   		 }   
   		 
		materialsDict = new Hashtable();
		Renderer[] sn = ilots.GetComponentsInChildren<Renderer>();

		// prepare for undo
		List<Object> itemsForUndo = new List<Object>();
		foreach (Renderer mf in sn) {
			GameObject go = GetIlotGameObject (mf);
			itemsForUndo.Add (go);
			itemsForUndo.Add(mf);
		}
		Undo.RecordObjects (itemsForUndo.ToArray(), "Update Buildings");
	
	 	
		foreach (Renderer mf in sn) {
			AddCollider (mf);
			UpdateMaterial (mf);
			AddScript(mf, "MouseClic");
			SetTag(mf, "Ilot");
		}
		
		 EditorUtility.DisplayDialog("Done", "", "OK");
	}

	
} // end of class
