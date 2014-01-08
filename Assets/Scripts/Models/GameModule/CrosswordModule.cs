using UnityEngine;
using System.Collections;

[System.Serializable]
public class CrosswordModule : GameModule {

	public int nbCasesInWidth =0;
	public int nbCasesInHeight =0;

	[SerializeField]
	public Word wordsList;
	
}
