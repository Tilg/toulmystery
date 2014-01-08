using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Word))]
public class EditorWord : Editor {

	Word _target;

	void OnEnable() {
		_target = (Word)target;
	}

	public override void OnInspectorGUI() {

		GUILayout.BeginVertical();

		GUILayout.Label ("Edition de mot", EditorStyles.boldLabel);
			
		int intNumber = EditorGUILayout.IntField("numéro du mot",(int) _target.number);

		string stringWord = EditorGUILayout.TextField("mot",(string) _target.word);
		string stringDefinition = EditorGUILayout.TextField("définition du mot",(string) _target.definition);

		int intXBeginCell = EditorGUILayout.IntField("X case départ",(int) _target.xBeginCell);
		int intYBeginCell = EditorGUILayout.IntField("Y case départ",(int) _target.yBeginCell);

		int	intXEndCell = EditorGUILayout.IntField("X case fin",(int) _target.xEndCell);
		int	intYEndCell = EditorGUILayout.IntField("Y case fin",(int) _target.yEndCell);

		_target.number =  intNumber; // Common float field
		_target.word =  stringWord; // Common float field
		_target.definition =  stringDefinition; // Common float field

		_target.xBeginCell =  intXBeginCell; // Common float field
		_target.yBeginCell =  intYBeginCell; // Common float field

		_target.xEndCell =  intXEndCell; // Common float field
		_target.yEndCell =  intYEndCell; // Common float field
			
			
		GUILayout.EndVertical();
		
		//If we changed the GUI aply the new values to the script
		if(GUI.changed) {
			EditorUtility.SetDirty(_target);        
		}
	}
}