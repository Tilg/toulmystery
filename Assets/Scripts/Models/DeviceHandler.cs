using UnityEngine;
using System.Collections;

public class DeviceHandler : MonoBehaviour {

	// this multiplicator allows us to modify the interface in the different view regarding to the kind of device who run the game 
	public static float multiplicator;
	private bool defaultMode; // the default mode set the current dpi, screen width and screen height with the data set in the Constants script // IPAD mini retina by default
	private DeviceOrientation lastOrientation;

	private int currentWidth;
	private int currentHeight;
	private float currentDPI;
	
	void Awake(){

		if (Screen.dpi == 0){ // if we cannot know the dpi of the device running the app 
			defaultMode = true;
			currentDPI = Constants.DEFAULT_DPI;
			currentWidth = Constants.DEFAULT_SCREEN_WIDTH;
			currentHeight = Constants.DEFAULT_SCREEN_HEIGTH;
		}
		else{
			currentDPI = Screen.dpi;
			currentWidth = Screen.width;
			currentHeight = Screen.height;
		}


		// we calculate the multiplicator
		if (defaultMode || currentDPI == Constants.DEFAULT_DPI || currentWidth == Constants.DEFAULT_SCREEN_WIDTH || currentHeight == Constants.DEFAULT_SCREEN_HEIGTH ){ // if we are in the default mode or if the device is an ipadMini retina

			multiplicator = 1;
		}
		else{

			float widthRatio = Constants.DEFAULT_SCREEN_WIDTH / currentWidth;
			float dpiRatio = Constants.DEFAULT_DPI / currentDPI;

			multiplicator = 1 / (widthRatio * dpiRatio );
		}

	}

}
