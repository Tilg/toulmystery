using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Schema;

public class InitTrajetXML : MonoBehaviour {
	
	private static XmlReader reader;
	private static string pathXmlFile;
	
	// Use this for initialization
	public static void LoadXML(GameManager gameManager){
		
		pathXmlFile = "Assets/Resources/Initialisation/" + gameManager.GetInitialisationXMLFile();

		ValidationEventHandler eventHandler = new ValidationEventHandler(InitTrajetXML.ValidationCallback);
		
		try
		{
			// Create the validating reader and specify DTD validation.
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.ValidationType = ValidationType.DTD;
			settings.ProhibitDtd = false;
			settings.ValidationEventHandler += eventHandler;
			
			reader = XmlReader.Create(pathXmlFile, settings);
			
			// Pass the validating reader to the XML document.
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);
			
			
			/** MAP POINTS initialisation
			XmlNode mappoints = doc.SelectSingleNode("/RACINE/MAPPOINTS");
			XmlNodeList mapPointsList = mappoints.ChildNodes;

			XmlNode mapPoint1= mapPointsList[0];
			XmlNode mapPoint2= mapPointsList[1];

			foreach( XmlNode mapPoint in mapPointsList){
				XmlAttributeCollection attributeList = mapPoint.Attributes;

				XmlNode latitude = attributeList.GetNamedItem("latitude");
				Debug.Log("latitude : "+latitude.Value);
				XmlNode longitude = attributeList.GetNamedItem("longitude");
				Debug.Log("longitude : "+longitude.Value);

			}
			*/
			
			// Trajet initialisation
			XmlNode trajet = doc.SelectSingleNode("/RACINE/TRAJET");
			
			XmlNodeList checkpointList = trajet.ChildNodes;
			
			
			// foreach Checkpoint we create a new instance 
			foreach ( XmlNode checkpoint in checkpointList ) {
				
				XmlAttributeCollection attributeList = checkpoint.Attributes;
				
				XmlNode id = attributeList.GetNamedItem("idCheckpoint");
				XmlNode latitude = attributeList.GetNamedItem("latitude");
				XmlNode longitude = attributeList.GetNamedItem("longitude");
				XmlNode range = attributeList.GetNamedItem("range");
				
				XmlNodeList moduleList = checkpoint.ChildNodes;
				
				// foreach module of the current checkpoint
				foreach ( XmlNode gameModule in moduleList ) {
					//we look for the gameModule type
					string moduleType = gameModule.FirstChild.Name;
					GameModule newGameModule = null;
					
					switch(moduleType){
						
					case "HelpAndExplainationModule" :
						newGameModule = InstanciateHelpAndExplainationModule(gameModule);	

						Debug.Log("passe ici");
						Debug.Log(newGameModule);

						break;
						
					case "MultipleChoiceQuestionModule" :
						newGameModule = InstanciateMultipleChoiceQuestionModule(gameModule);
						break;
						
					case "QuestionResponseModule" :
						newGameModule = InstanciateQuestionResponseModule(gameModule);
						break;
						
					default :
						Debug.Log("type de checkpoint inconnu");
						break;
					}
					
				}
			}
		}
		finally
		{
			if (reader != null)
				reader.Close();
		}
	}
	
	
	private static GameModule InstanciateHelpAndExplainationModule(XmlNode gameModule){

		//we get all the informations from the xml file that we need to create the new module
		XmlAttributeCollection attributeList = gameModule.Attributes;
		
		XmlNode idGameModule = attributeList.GetNamedItem("idGameModule");
		XmlNode title = attributeList.GetNamedItem("title");

		XmlAttributeCollection firstChildAttributeList = gameModule.FirstChild.Attributes;

		Debug.Log(firstChildAttributeList.Count);

		XmlNode sheetPath = firstChildAttributeList.GetNamedItem("sheetPath");

		//we instantiate a new GameObject with a default position and rotation value in 0
		//GameObject newModule = Instantiate(HelpAndExplainationModule, new Vector3(0,0,0), Quaternion.identity);
		
		//newModule.setId(idGameModule.Value);
		//newModule.setTitle(title.Value);
		//newModule.setMyGUITexture(sheetPath.Value);

		//return newModule;
		return null;
	}
	
	private static GameModule InstanciateMultipleChoiceQuestionModule(XmlNode gameModule){
		return null;
	}
	
	private static GameModule InstanciateQuestionResponseModule(XmlNode gameModule){
		return null;
	}
	
	//Display the validation error.
	private static void ValidationCallback(object sender, ValidationEventArgs args)
	{
		Debug.Log("passe dans ValidationCallback");
		Debug.Log("validator event message : " +args.Message);
	}
}
