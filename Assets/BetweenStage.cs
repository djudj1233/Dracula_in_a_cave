using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetweenStage : MonoBehaviour {
	public float TimeCheck;
	public int TimeCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TimeCheck += 1 * Time.deltaTime;
		TimeCount = (int)TimeCheck;
		if(TimeCount == 1)
		{
			SceneManager.LoadScene("Stage2");
		}
	}
}
