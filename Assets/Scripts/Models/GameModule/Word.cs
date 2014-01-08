using UnityEngine;
using System.Collections;

[System.Serializable]
public class Word : MonoBehaviour {

	[SerializeField]
	public int number;
	[SerializeField]
	public string word;
	[SerializeField]
	public string definition;
	[SerializeField]
	public int xBeginCell;
	[SerializeField]
	public int yBeginCell;
	[SerializeField]
	public int xEndCell;
	[SerializeField]
	public int yEndCell;

}
