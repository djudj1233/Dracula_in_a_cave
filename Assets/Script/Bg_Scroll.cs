using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg_Scroll : MonoBehaviour {
	public GameControl GC;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GC.GS != GameState.Pause)
		{
			this.transform.Translate(Vector3.left * 3 * Time.deltaTime);
			if (this.transform.position.x < -55f)
			{
				this.transform.position = new Vector3(120 + this.transform.position.x, 2, 10);
			}
		}
	}
}
