using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreControl : MonoBehaviour {
	public GameControl GC;
	public Text blood_val; //소유 코인
	public Text Time_val;  //소유 시간
	public Text HP_val;    //현재 체력

	public Button Btn_HP200;
	public Button Btn_HP100;
	public Button Btn_Time50;
	public Button Btn_Time25;

	// Use this for initialization
	void Start () {
		ShowInfo();
	}
	
	// Update is called once per frame
	void Update () {
		ShowInfo();
	}

	void ShowInfo()
	{
		blood_val.text = GameData.m_blood.ToString();
		Time_val.text = GameData.playTimeCurrent.ToString("0");
		HP_val.text = GameData.CurrentHp.ToString();
	}

	public void HP_200_Btn()                //200체력 버튼
	{
		if (GameData.m_blood >= 20)
		{
			GameData.m_blood -= 200;
			GameData.CurrentHp += 200;
			if (GameData.CurrentHp > 5000)      //최대 체력 5000
			{
				GameData.CurrentHp = 5000;
				Btn_HP200.interactable = false;
				Btn_HP100.interactable = false;
			}
		}
	}

	public void HP_100_Btn()                //100체력 버튼
	{
		if (GameData.m_blood >= 10)
		{
			GameData.m_blood -= 100;
			GameData.CurrentHp += 100;
			if (GameData.CurrentHp > 5000)
			{
				GameData.CurrentHp = 5000;
				Btn_HP100.interactable = false;
				Btn_HP200.interactable = false;
			}
		}
	}

	public void Time_50_Btn()
	{
		if (GameData.m_blood >= 30)
		{
			GameData.m_blood -= 100;
			GameData.playTimeCurrent += 10;
			if (GameData.playTimeCurrent > 1000)  //최대 시간
			{
				GameData.playTimeCurrent = 1000;
				Btn_Time50.interactable = false;
				Btn_Time25.interactable = false;
			}
		}
	}

	public void Time_25_Btn()
	{
		if (GameData.m_blood >= 15)
		{
			GameData.m_blood -= 200;
			GameData.playTimeCurrent += 5;
			if (GameData.playTimeCurrent > 1000)
			{
				GameData.playTimeCurrent = 1000;
				Btn_Time50.interactable = false;
				Btn_Time25.interactable = false;
			}
		}
	}

	public void Go_Back_Btn()
	{
		SceneManager.LoadScene("Stage1");
	}
}
