using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialougeManager : MonoBehaviour {

	public static DialougeManager instance;

	#region Singleton
	private void Awake()
	{
		if(instance == null)
		{
			DontDestroyOnLoad(this.gameObject);
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	[SerializeField]
	public Dialouge dialouge;

	public Text text;
	private List<string> listSentense;
	private int count; //대화 진행상황 카운트
	public bool talking = false;

	// Use this for initialization
	void Start () {
		count = 0;
		text.text = "";
		listSentense = new List<string>();
		
		ShowDialouge(dialouge);
	}
	
	public void ShowDialouge(Dialouge dialouge)
	{
		talking = true;
		for(int i =0; i < dialouge.sentences.Length; i++)
		{
			listSentense.Add(dialouge.sentences[i]);
		}
		StartCoroutine(StartDialougeCoroutine());
	}

	public void ExitDialouge()
	{
		text.text = "";
		count = 0;
		listSentense.Clear();
		talking = false;
		if(SceneManager.GetActiveScene().name == "IntroScene")
		{
			SceneManager.LoadScene("Stage1");
		}else if(SceneManager.GetActiveScene().name == "EndingScene")
		{
			SceneManager.LoadScene("EndingScene 1");
		}
		
	}

	IEnumerator StartDialougeCoroutine()
	{
		for(int i=0; i<listSentense[count].Length; i++)
		{
			text.text += listSentense[count][i]; //1번째문장, 가나다라마바사(1글자씩출력)
			yield return new WaitForSeconds(0.02f);
		}

	}

	// Update is called once per frame
	void Update () {
		if(talking == true)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				count++;
				text.text = "";
				if (count == listSentense.Count) //대화창이 마지막일때!!
				{
					StopAllCoroutines();
					ExitDialouge();
				}
				else
				{
					StopAllCoroutines();
					StartCoroutine(StartDialougeCoroutine());
				}
			}
		}
		
	}
}
