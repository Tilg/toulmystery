using UnityEngine;
using System.Collections;

/// <summary>
/// Determines the size of the page buffer.
/// </summary>
/// <param name="initialPageBufferSize">Initial size of the page buffer.</param>
/// <param name="host">The host.</param>
/// <param name="port">The port.</param>
/// <param name="timeout">The timeout.</param>
/// <param name="smartTrace">if set to <c>true</c> [smart trace].</param>
/// <returns></returns>
public abstract class GameModule : MonoBehaviour{
	
	public string id;
	public string title;
	public string description;
	public string soundUrl;
	
	public GameModule(){}	
	
	public bool isFinished(){
		//TODO: la fonction va prevenir le conrôleur qu'elle se termine
		return false;
	}
	
	public void playSound(){
		//TODO: la fonction va lancer le son spécifié dans sound url si il y en a un de spécifié
	}
	
}
