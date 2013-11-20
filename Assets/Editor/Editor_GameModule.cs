using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameModule))]
public class Editor_GameModule : Editor {
	
	
	GameModule _target;
 
    void OnEnable()
    {
       _target = (GameModule)target;
    }
	
	
	public override void OnInspectorGUI()
    {
       GUILayout.BeginVertical();
             
        _target.title = EditorGUILayout.TextField("chekpoint ID", _target.title);
		_target.description = EditorGUILayout.TextField("Adresse", _target.description);
		_target.soundUrl = EditorGUILayout.TextField("Latitude", _target.soundUrl);
           
       GUILayout.EndVertical();
 
       //If we changed the GUI aply the new values to the script
       if(GUI.changed)
       {
         EditorUtility.SetDirty(_target);        
       }
    }
}
