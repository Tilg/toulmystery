using UnityEngine;
using UnityEditor; // Dont forget to add this as we are extending the Editor
using System.Collections;
 
[CustomEditor(typeof(GPSPoint))] //Set tour script to extend the GPSPoint.cs
public class EditorGPSPoint : Editor { // Our script inherits from Editor 

    GPSPoint _target;
 
    void OnEnable() {
       _target = (GPSPoint)target;
    }
 
    // Here is where the magic begins! You can use any GUI command here (As far as i know)
    public override void OnInspectorGUI() {
       GUILayout.BeginVertical();
           GUILayout.Label ("Reposionnement GPS", EditorStyles.boldLabel);
 
		float fltlat = 	EditorGUILayout.FloatField("Latitude",(float) _target.lat); // Common FLOAT field
		float fltlng = EditorGUILayout.FloatField("Longitude",(float) _target.lng); // Common FLOAT field	
        
		_target.lat =  fltlat; // Common float field
		_target.lng =  fltlng; // Common float field	
 
       GUILayout.EndVertical();
 
       //If we changed the GUI aply the new values to the script
       if(GUI.changed) {
         EditorUtility.SetDirty(_target);        
       }
    }
}