using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndigControl : MonoBehaviour {
	public GameObject backGround;
	public Sprite[] backImg;
	public Text EndingText;
	public float TimeCount;
	int i = 0;
	// Use this for initialization
	void Start () {
		EndingText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		NextScene();
	}

	void NextScene()
	{
		TimeCount += 1 * Time.deltaTime;
		int Count = (int)TimeCount;
		if (Count == 1)
		{
			if ( i< 5 )
			{
				backGround.GetComponent<Image>().sprite = backImg[i];
				TimeCount = 0;
				i++;
			}
			else
			{
				EndingText.gameObject.SetActive(true);
			}
		}
	}
}
