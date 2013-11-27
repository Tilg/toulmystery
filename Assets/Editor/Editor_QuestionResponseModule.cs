using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(QuestionResponseModule))]
public class Editor_QuestionResponseModule : Editor {
	 /*
	QuestionResponseModule _target;
	
	public int idProp;
	public Question questionProp;
 
    public void OnEnable()
    {
		//questionList = new List<Question>();
       _target = (QuestionResponseModule)target;
		
		// Setup the SerializedProperties
		idProp = _target.id;
		questionProp = _target.questionObj;
    }
	
	
	public override void OnInspectorGUI() {
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();
		
		idProp = EditorGUILayout.IntField("id de module", _target.id); 
		
		questionProp.questionText = EditorGUILayout.TextField("question", questionProp.questionText);
		
		questionProp.answer = EditorGUILayout.TextField("reponse attendue", questionProp.answer);
		
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();
	} */
	
}
