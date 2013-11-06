using UnityEngine;
using System.Collections;

public class Checkpoint {
	
	private int id;
	private string adress;
	private float latitude;
	private float longitude;
	private float range;
	private ArrayList gameModulesList;
	private GameModule curentGameModule; 
	
	public Checkpoint (int id, string adress, float latitude, float longitude, float range)
	{
		this.id = id;
		this.adress = adress;
		this.latitude = latitude;
		this.longitude = longitude;
		this.range = range;
		gameModulesList = new ArrayList();
	}
	
	public void addModule(GameModule newModule){
		gameModulesList.Add(newModule);
	}
	
	public void deleteModule(GameModule module){
		gameModulesList.Remove(module);
	}
}
