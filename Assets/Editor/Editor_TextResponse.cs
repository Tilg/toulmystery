using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TextResponse))]
public class Editor_TextResponse : Editor {

	TextResponse _target;
 
    void OnEnable()
    {
       _target = (TextResponse)target;
    }
 
    public override void OnInspectorGUI()
    {
       GUILayout.BeginVertical();
		
        _target.id = EditorGUILayout.IntField("id de reponse", _target.id);     
		_target.correctAnswer = EditorGUILayout.TextField("reponse attendue", _target.correctAnswer);
           
       GUILayout.EndVertical();
 
       //If we changed the GUI aply the new values to the script
       if(GUI.changed)
       {
         EditorUtility.SetDirty(_target);        
       }
    }
}
