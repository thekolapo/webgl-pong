using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorLerpingScript : MonoBehaviour {
    public Color color1;
	// [HideInInspector]
	public Color color2;
    public Color [] colors;
	public float duration;
	private bool canChangeColor = true;

	//float t = 0;

	bool increment = true;
	bool canTransition = false;

	// Use this for initialization
	void Start () {
		color2 = colors[Random.Range(0, colors.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		float t = Mathf.PingPong(Time.time, duration) / duration;
	
		if(t >= 0.994f || t <= 0.0994f){
			if(canTransition){
				return;
			}

			canTransition = true;
			Color randomizedColor = colors[Random.Range(0, colors.Length)];
			Color whichColor = (t >= 0.994f)? color1 = randomizedColor : color2 = randomizedColor;

			StartCoroutine(WaitFor(1.5f));
		}
	
		// if(!canChangeColor){
		// 	return;
		// }

        Camera.main.backgroundColor = Color.Lerp(color1, color2, t);
	
	}

	private IEnumerator WaitFor(float t){
		yield return new WaitForSeconds(t);
		canChangeColor = true;
		canTransition = false;
	}


}
