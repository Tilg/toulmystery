using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;
using System.IO;


public class VegetationMonoTerrain {
	
	public static float maxH;

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
		float startY = terrain.transform.position.y;
    	float startZ = terrain.transform.position.z;
		float maxX = terrain.terrainData.size.x;
		float maxY = terrain.terrainData.size.y;
		float maxZ = terrain.terrainData.size.z;
		float terrainX =(-p.x-startX)/maxX;
		float terrainZ =  (p.z-startZ)/maxZ;
		
		if (p.y>maxH) maxH= p.y;
		
//		float worldY = terrain.terrainData.GetInterpolatedHeight(terrainX, terrainZ);
//		worldY+=0.1f;
//		float terrainY = (worldY)/maxY;

		float worldY = p.y;

		float terrainY = (worldY-startY)/maxY;
		
		return new Vector3 (terrainX, terrainY, terrainZ);
	}

	static Terrain getMainTerrain (string aName) {
 		UnityEngine.Object[] ts=UnityEngine.Object.FindObjectsOfType(typeof(Terrain));
 		Terrain mainTerrain=null;
 		foreach (Terrain t in ts) {
 			if (t.name==aName) {
 				mainTerrain = t;
 				break;
 			}
 		}
 		return mainTerrain;
	}

	[MenuItem("Toul/Add Trees")]
	public static void DoVegetation() {
//	 	Undo.RegisterUndo (Terrain.activeTerrain.terrainData, "Add Trees");
		TerrainData[] terrains= new TerrainData[2]; 
		terrains[0] = getMainTerrain("TerrainVignes").terrainData;
		terrains[1] = getMainTerrain("TerrainArbres").terrainData;
		Undo.RegisterUndo (terrains, "Add Trees");
		
		terrains[0].treeInstances=new TreeInstance[0];
		terrains[1].treeInstances=new TreeInstance[0];

		AddVegetationVignes("zone2_vigne");

		AddVegetation("zone1_vegCours");
		AddVegetation("zone1_vegRamparts");
		AddVegetation("zone2_vegetation");
		AddVegetation("zone2_haies");
		
		int n = getMainTerrain("TerrainArbres").terrainData.treeInstances.Length;
		n += getMainTerrain("TerrainVignes").terrainData.treeInstances.Length;
		EditorUtility.DisplayDialog("Add Trees", "Done !\n\n"+n.ToString()+" trees on terrain", "OK");

	}
	
	[MenuItem("Toul/Remove Trees")]
	public static void DoRemoveVegetation() {
		TerrainData[] terrains= new TerrainData[2]; 
		terrains[0] = getMainTerrain("TerrainVignes").terrainData;
		terrains[1] = getMainTerrain("TerrainArbres").terrainData;
		Undo.RegisterUndo (terrains, "Remove Trees");
		
		terrains[0].treeInstances=new TreeInstance[0];
		terrains[1].treeInstances=new TreeInstance[0];
	}


	public static void AddVegetation(string filename) 
	{
		string[] lines = OpenTextFile (filename);
		string trimmedLine;
		string result;
		
		Vector3 trans= Vector3.zero;
		ArrayList points= new ArrayList();
		int typeVegetation=0;
		
   		Terrain terrain = getMainTerrain("TerrainArbres");
		TerrainData	terraindata = terrain.terrainData;
		TreeInstance tree;
		List<TreeInstance> newTrees = new List<TreeInstance>(terraindata.treeInstances);

		Vector3 deltaY=Vector3.zero;
		deltaY.y+=0.0f;
				
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
				float rnd;
				while (e.MoveNext()) {
					
					// make more random
					if ((typeVegetation<=11) && (typeVegetation!=6)  && (typeVegetation!=7)  && (typeVegetation!=8)) {
						while ((rnd=Random.value)==1.0f);
						typeVegetation = (typeVegetation/3)*3+(int) (rnd*3);
					}
					
				
					tree = new TreeInstance(); 
					tree.position = WorldToTerrain(terrain,(Vector3)e.Current+trans+deltaY);
			
					tree.prototypeIndex = typeVegetation;
					
					if (typeVegetation==18) Debug.Log ("invalide vegetation");
					
					if (typeVegetation<=5) rnd =  0.3f*(Random.value-1.0f)-0.1f;
					else if (typeVegetation<=8) rnd =  0.2f*(Random.value-1.0f);
					else if (typeVegetation==9) rnd =  0.2f*(Random.value-1.0f);
					else rnd= 0.25f*(Random.value-1.0f);
//		rnd=0.0f;

					if ((typeVegetation>=6) && (typeVegetation<=8)) tree.widthScale = 1.0f; 
					else tree.widthScale = 1.0f+rnd; 
					tree.heightScale = 1.0f+rnd;
					
					Color c = Color.white;
					c[0] = 0.92f+Random.value*0.08f;
					c[1] = 0.92f+Random.value*0.08f;
					c[2] = 0.92f+Random.value*0.08f;
					tree.color = c;
					
					tree.lightmapColor = Color.white;
					
					
					// 1, 3, 4, 5
					
					//if (typeVegetation==5)
					newTrees.Add(tree);
				}
					
			}
		}
		terraindata.treeInstances= newTrees.ToArray();
	}
	
	public static void AddVegetationVignes(string filename) 
	{
		string[] lines = OpenTextFile (filename);
		string trimmedLine;
		string result;
		
		maxH = 0.0f;
		
		Vector3 trans= Vector3.zero;
		ArrayList points= new ArrayList();
		int typeVegetation=0;
		typeVegetation+=0; // avoid a warning
		
   		Terrain terrain = getMainTerrain("TerrainVignes");
		TerrainData	terraindata = terrain.terrainData;
		TreeInstance tree;
		List<TreeInstance> newTrees = new List<TreeInstance>(terraindata.treeInstances);
		
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
				float rnd;
				while (e.MoveNext()) {
				
					tree = new TreeInstance(); 
					tree.position = WorldToTerrain(terrain,(Vector3)e.Current+trans);
			
					tree.prototypeIndex = 1;
					rnd= 0.25f*(Random.value-1.0f);
//		rnd=0.5f;
					tree.widthScale = 1.0f+rnd; 
					tree.heightScale = 1.0f+rnd;
					
					Color c = Color.white;
					c[0] = 0.92f+Random.value*0.08f;
					c[1] = 0.92f+Random.value*0.08f;
					c[2] = 0.92f+Random.value*0.08f;
					tree.color = c;
					
					tree.lightmapColor = Color.white;
					
					newTrees.Add(tree);
				}
					
			}
		}
		terraindata.treeInstances= newTrees.ToArray();
		Debug.Log (maxH);
	}
	

	
} // end of class
