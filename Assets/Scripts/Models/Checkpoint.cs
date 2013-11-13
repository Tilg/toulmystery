using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour{
	
	public int id;
	public string adress;
	public float latitude;
	public float longitude;
	public float range;
	public ArrayList gameModulesList;
	public GameModule curentGameModule; 
	
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
