using UnityEngine;
using System.Collections;


// must be attached to a one time GameObject
public class LinksController : MonoBehaviour {

	private static Hashtable links;
	private static LinksController singleton;
	private static bool isRunningInWebPlayer;

	IEnumerator LoadLinks () {
		string s =  Application.absoluteURL;
		isRunningInWebPlayer = (s!="");
		if (s=="")	s ="file://"+Application.dataPath+"/Resources/testlinks.unity3d"; // only when developing
		s= s.Substring (0,s.Length-7)+"txt";
		WWW www = new WWW (s);
		yield return www;
		
		
		links = new Hashtable();
		string[] lines = www.text.Split("\n".ToCharArray());
		string[] keyValue;
		foreach (string line in lines){
			keyValue = line.Split(';'); 
			if (keyValue.Length==2) {
				if ((keyValue[0].Length>0) && (keyValue[1].Length>0)) 
				links[keyValue[0]]=keyValue[1];
			}
		}
	}

	// Use this for initialization
	void Start () {
		singleton = this;
		//LoadLinks();
		StartCoroutine(LoadLinks());
	}
	
	public void MouseClickOnItemNamed (string aName) {
			
		string anUrl=links[aName] as string;
		if (null==anUrl) anUrl=links["DEFAUT"] as string;
		if (null!=anUrl) 
			Menu.infos=anUrl;
			Debug.Log (anUrl);
			Debug.Log (isRunningInWebPlayer);

		if (isRunningInWebPlayer) Application.ExternalCall ("Popup",anUrl);
		else Application.OpenURL(anUrl);
	}
	
	public static void MouseClickOn (string aName) {
		if (singleton.enabled) singleton.MouseClickOnItemNamed(aName);
	}
	

}
