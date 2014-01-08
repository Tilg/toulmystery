using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;
using System.IO;

public class VegetationMesh : MonoBehaviour {
	
	static UnityEngine.Object[] prefabTrees;
	static GameObject meshTrees;

	public static string[] OpenTextFile(string filename) {
		TextAsset o = (TextAsset) AssetDatabase.LoadAssetAtPath ("Assets/Terrain Items/"+filename+".txt", typeof(TextAsset));
		if (o == null)  Debug.Log( "Error: file \'"+ filename + "\' not found or not readable" );
		return o.text.Split("\n".ToCharArray());
	}

	// check if a line match an item. return null if no match. return the rest of the line if matche
	public static string MatchItem (string line, string item) {
		if (line.StartsWith(item)) return line.Remove(0,item.Length);
		return null;
	}

	public static Vector3 stringToVector3 (string str) {
		str = str.Replace (",",".");
		string[] values = str.Split(' '); 
		return new Vector3 (float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
	}

	public static ArrayList decodeArrayOfPoints (string str) {

		if (str =="]") return null;
		string[] values = str.Replace("]", "").Split(',');
		ArrayList res = new ArrayList();
		foreach (string val in values) res.Add(stringToVector3(val.Trim()));
		return res;
	}
	
	public static Vector3 WorldToTerrain (Terrain terrain, Vector3 p) {
		float startX = terrain.transform.position.x;
//		float startY = terrain.transform.position.y;
    	float startZ = terrain.transform.position.z;
		float maxX = terrain.terrainData.size.x;
		float maxY = terrain.terrainData.size.y;
		float maxZ = terrain.terrainData.size.z;
		float terrainX =(-p.x-startX)/maxX;
		float terrainZ =  (p.z-startZ)/maxZ;
		
		float worldY = terrain.terrainData.GetInterpolatedHeight(terrainX, terrainZ);
		//worldY+=1.1f;
		float terrainY = (worldY)/maxY;
		
		return new Vector3 (terrainX, terrainY, terrainZ);
	}

//	[MenuItem("Toul/Add Mesh Trees")]
	public static void DoVegetation() {

			meshTrees = GameObject.Find ("MeshTrees");
			if  (meshTrees!=null) UnityEngine.Object.DestroyImmediate(meshTrees,false);

			//meshTrees = GameObject.Find ("MeshTrees"); // root to store all trees
			meshTrees = new GameObject();
			meshTrees.name = "MeshTrees";
			
			
			// load all prefab
			prefabTrees=new UnityEngine.Object[19];
			prefabTrees[0] = Resources.Load("Arbres/0Pommier");
			prefabTrees[1] = Resources.Load("Arbres/1Pommier");
			prefabTrees[2] = Resources.Load("Arbres/2Pommier");
			prefabTrees[3] = Resources.Load("Arbres/3 Arbre long");
			prefabTrees[4] = Resources.Load("Arbres/4Arbre long");
			prefabTrees[5] = Resources.Load("Arbres/5Arbre long");
			prefabTrees[6] = Resources.Load("Arbres/6Sapin");
			prefabTrees[7] = Resources.Load("Arbres/7Sapin");
			prefabTrees[8] = Resources.Load("Arbres/8Sapin");
			prefabTrees[9] = Resources.Load("Arbres/9BuissonHaie");
			prefabTrees[10] = Resources.Load("Arbres/10BuissonHaie");
			prefabTrees[11] = Resources.Load("Arbres/11BuissonHaie");
			prefabTrees[12] = Resources.Load("Arbres/12PetitBuissonVert");
			prefabTrees[13] = Resources.Load("Arbres/13PetitBuissonBleu");
			prefabTrees[14] = Resources.Load("Arbres/14PetitBuissonRouge");
			prefabTrees[15] = Resources.Load("Arbres/15ArbreTroncCourt");
			prefabTrees[16] = Resources.Load("Arbres/16ArbreTroncCourt");
			prefabTrees[17] = Resources.Load("Arbres/17ArbreTroncCourt");
			prefabTrees[18] = Resources.Load("Arbres/18Cep");


		int n=0;
	
		n+=AddVegetation("zone1_vegCours");
		n+=AddVegetation("zone1_vegRamparts");
		n+=AddVegetation("zone2_veg");
		n+=AddVegetation("zone2_vigne");
		n+=AddVegetation("zone2_haies");
		
		EditorUtility.DisplayDialog("Add Mesh Trees", "Done !\n\n"+n.ToString()+" trees on terrain", "OK");

	}
	

//	[MenuItem("Toul/Remove Mesh Trees")]
	public static void DoRemoveVegetation() {
			GameObject meshTrees = GameObject.Find ("MeshTrees");
			if  (meshTrees!=null) UnityEngine.Object.DestroyImmediate(meshTrees,false);
	}


	public static int AddVegetation(string filename) 
	{
		string[] lines = OpenTextFile (filename);
		string trimmedLine;
		string result;
		
		Vector3 trans= Vector3.zero;
		ArrayList points= new ArrayList();
		int typeVegetation;
		
  		int n = 0;
//		int lineNumber = 0;
		foreach (string line in lines) {
//			Debug.Log (++lineNumber);
			trimmedLine = line.Trim();
			result = MatchItem(trimmedLine, "translation ");
			if ((null!=result) ) { trans = stringToVector3 (result); }
			result = MatchItem(trimmedLine, "points [");
			if (null!=result) { points = decodeArrayOfPoints(result); }
			result = MatchItem(trimmedLine, "pointsRemplis [");
			if (null!=result) { points = decodeArrayOfPoints(result); }
			result = MatchItem(trimmedLine, "typeVegetation ");
			if (null!=result) {
				typeVegetation=int.Parse(result);
				
				IEnumerator e = points.GetEnumerator();
				while (e.MoveNext()) {
					n++;
					GameObject tree = EditorUtility.InstantiatePrefab (prefabTrees[typeVegetation]) as GameObject;
					tree.transform.parent=meshTrees.transform;
					
					Vector3 v = (Vector3)e.Current+trans;
					v.x=-v.x;
					tree.transform.position=v;

				}
					
			}
		}
		return n;
	}
	
}
