using UnityEngine;
using System.Collections;

public class CalcScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		GPSPoint gpsPoint1 = new GPSPoint(48.676845,5.889817);
		GPSPoint gpsPoint2 = new GPSPoint(48.677714,5.885418);
		GPSPoint gpsPoint3 = new GPSPoint(48.678869,5.8919912);
		
		
		Scale scale = new Scale(gpsPoint1,gpsPoint2,gpsPoint3);
		print(scale.getInfo());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
