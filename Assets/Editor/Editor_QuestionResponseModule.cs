using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(QuestionResponseModule))]
public class Editor_QuestionResponseModule : Editor {
	
	QuestionResponseModule _target;
 
    void OnEnable()
    {
		questionList = new List<Question>();
       _target = (QuestionResponseModule)target;
    }
	
	
	public override void OnInspectorGUI()
    {
       
	}
}
