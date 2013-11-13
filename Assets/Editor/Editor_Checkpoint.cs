using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Checkpoint))]
public class Editor_Checkpoint : Editor {

	Checkpoint _target;
 
    void OnEnable()
    {
       _target = (Checkpoint)target;
    }
 
    public override void OnInspectorGUI()
    {
       GUILayout.BeginVertical();
             
        _target.id = EditorGUILayout.IntField("chekpoint ID", _target.id);
		_target.adress = EditorGUILayout.TextField("Adresse", _target.adress);
		_target.latitude = EditorGUILayout.FloatField("Latitude", _target.latitude);
		_target.longitude = EditorGUILayout.FloatField("Longitude", _target.longitude);
		_target.range = EditorGUILayout.FloatField("Distance d'activation (en metre)", _target.range);
           
       GUILayout.EndVertical();
 
       //If we changed the GUI aply the new values to the script
       if(GUI.changed)
       {
         EditorUtility.SetDirty(_target);        
       }
    }
}
