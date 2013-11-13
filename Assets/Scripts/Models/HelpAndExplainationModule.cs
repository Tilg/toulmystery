using UnityEngine;
using System.Collections;

public class HelpAndExplainationModule : GameModule {

	public ArrayList sheetList;
	
	public HelpAndExplainationModule(string _title, string _description, string _soundUrl){
	
		this.title = _title;
		this.description = _description;
		this.soundUrl = _soundUrl;
		
		sheetList = new ArrayList();
	}
	
	/*
	public void addElement( InformationSheet element){
		sheetList.AddRange(element);
	}
	
	public void removeElement( InformationSheet element){
		sheetList.Remove(element);
	}
	*/
}
