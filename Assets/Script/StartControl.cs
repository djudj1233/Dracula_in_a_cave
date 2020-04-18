using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour {

	public Renderer Lamp;
	public Renderer Lamp2;
	private Color OriginColor;
	public AudioClip[] source;

	// Use this for initialization
	void Start()
	{
		OriginColor = Lamp.material.color;
		OriginColor = Lamp2.material.color;
	}

	// Update is called once per frame
	void Update()
	{
		float flicker = Mathf.Abs(Mathf.Sin(Time.time * 10));
		Lamp.material.color = OriginColor * flicker;
		Lamp2.material.color = OriginColor * flicker;
	}

	public void Btn_Start()
	{
		SceneManager.LoadScene("IntroScene");
	}
}
