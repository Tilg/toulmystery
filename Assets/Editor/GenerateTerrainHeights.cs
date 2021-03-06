using UnityEngine;
using UnityEditor;


public class GenerateTerrainHeights {
	
	public static void Mesh2Terrain (Terrain t, GameObject obj) {
	    if (obj.GetComponent<MeshFilter>() == null) {
			EditorUtility.DisplayDialog("Error while generating terrain", obj.name + " has no mesh.", "Cancel");
			return;
    	}
    	
 
  	  	TerrainData terraindata = t.terrainData;
   
    	// If there's no mesh collider, add one (and then remove it later when done)
    	bool addedCollider = false;
    	bool addedMesh = false;
    	MeshCollider objCollider = obj.collider as MeshCollider;
    	if (objCollider == null) {
        	objCollider = obj.AddComponent<MeshCollider>();
       		addedCollider = true;
   		 }
    	else if (objCollider.sharedMesh == null) {
        	objCollider.sharedMesh = (obj.GetComponent<MeshFilter>() as MeshFilter).sharedMesh;
        	addedMesh = true;
    	}

    	int resolutionX = terraindata.heightmapWidth;
    	int resolutionZ = terraindata.heightmapHeight;
    	float[,] heights = terraindata.GetHeights(0, 0, resolutionX, resolutionZ);
    
    	RaycastHit hit;
    	Ray ray = new Ray(Vector3.zero, -Vector3.up);
    
    	// le 0,0 du terrain est en position.x position.y
	    // les dimensions du terrain correspondent ‡ sa rÈsolution

    	float startX = Terrain.activeTerrain.transform.position.x;
    	float startY = Terrain.activeTerrain.transform.position.y;
    	float startZ = Terrain.activeTerrain.transform.position.z;
    	float stepX = terraindata.size.x / (resolutionX-1);
    	float maxY = terraindata.size.y;
   		float stepZ = terraindata.size.z / (resolutionZ-1);

    	// Do raycasting samples over the object to see what terrain heights should be
   		float z = startZ;
    	for (int zCount = 0; zCount < resolutionZ; zCount++) {
        	float x = startX;
        	for (int xCount = 0; xCount < resolutionX; xCount++) {
           		 ray.origin = new Vector3(x, 1000.0f, z);
            	// Debug.DrawLine (ray.origin, ray.origin+(20*ray.direction));
           		if (objCollider.Raycast(ray, out hit, 1000.0f)) heights[zCount, xCount]=(hit.point.y-startY)/maxY;
				//else heights[zCount, xCount] = 0.0;
           	 	x += stepX;
			}
			z += stepZ;
    	}
   
    	terraindata.SetHeights(0, 0, heights);

    	if (addedMesh) objCollider.sharedMesh = null;
    	if (addedCollider) Object.DestroyImmediate(objCollider);
	}


	[MenuItem("Toul/Generate Terrain Heights")]
	public static void Do() {
		Terrain ter = Terrain.activeTerrain;
 	   	if (Terrain.activeTerrain == null) {
       		 EditorUtility.DisplayDialog("Error while generating terrain", "Please make sure a terrain exists.", "Cancel");
        	 return;
   		 }   
 		GameObject td = GameObject.Find("Terrain Data");
	   	if (td == null) {
       		 EditorUtility.DisplayDialog("Error while generating terrain", "Terrain Data not found.", "Cancel");
        	 return;
   		 }   
		Component[] sn = td.GetComponentsInChildren<MeshFilter>();
		foreach (Component e in sn) Mesh2Terrain (ter, e.gameObject);
		
		EditorUtility.DisplayDialog("Generate Terrain Heights", "Done !", "OK");
	}

	
} // end of class
