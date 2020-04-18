using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Play, Pause, End}

public class GameData
{
	public static int m_enemyCheck =0; //제거할 적군의수 Count
	public static int m_blood =1300; //획득 Blood
	public static PlayerState pState; //플레이어의 상태
	public static float playTimeCurrent = 80f; // 45 현재 시간
	public static float playTimeMax = 60f; //맥스 시간
	public static int HpMax = 100; //주인공의 최대 HP
	public static int CurrentHp = 120;// 80 주인공의 현재 HP
	//public static string CurrentStage = "";
}
public class GameControl : MonoBehaviour {
	
	public GameObject[] NumberImage; // Blood 4자리에 Img
	public GameObject Pause_UI;
	public GameObject Event_UI;
	public GameObject Final_UI;
	public GameState GS; //게임상태
	public Sprite[] Number; // 0~9 까지의 img
	public Slider TimeCheck; //시간체크 슬라이더
	public Slider HpCHeck; //Hp체크 슬라이더
	public GameObject player;
	public int Hpflag = 1; // HP처음 상태 표시하기 위한 값
	public Button EventButton, BounusButton, StoreButton;
	public Text []bonus;
	public Text Enemy_txt;
	public Text ResultStage;
	public Text ResultBlood;
	public int replyFlag = 1;
	int event1 = 0, event2 = 0, event3 = 0, event4 = 0; //EventCheck하기위한 변수

	//public string CurrentStage { get; private set; }

	//public PlayerControl PC; //상점 이동후 다시 그 위치에서 시작...
	//public Vector3 playPosition;

	// Use this for initialization
	void Start () {
		EventButton.gameObject.SetActive(false);
		for(int i=0; i<bonus.Length; i++)
		{
			bonus[i].gameObject.SetActive(false);
		}
		GS = GameState.Play;
		Final_UI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//playPosition = PC.PlayerPosition.transform.position; //캐릭터위치 포지션 확인?
		if(GS == GameState.Play)
		{
			BloodCheck();
			TimeChecked();
			EnemyCheck();
			if (Hpflag == 1)
			{
				HpMinus();
			}
			if (GameData.m_enemyCheck == 5)
			{
				EventPlay();
			}
		}
	}

	void BloodCheck()
	{
		//골드 단위 계산 후 이미지로 표시..
		int temp = GameData.m_blood / 1000;
		NumberImage[0].GetComponent<Image>().sprite = Number[temp];

		int temp2 = GameData.m_blood % 1000;
		temp2 = temp2 / 100;
		NumberImage[1].GetComponent<Image>().sprite = Number[temp2];

		int temp3 = GameData.m_blood % 100;
		temp3 = temp3 / 10;
		NumberImage[2].GetComponent<Image>().sprite = Number[temp3];

		int temp4 = GameData.m_blood % 10;
		NumberImage[3].GetComponent<Image>().sprite = Number[temp4];
	}

	void EnemyCheck()
	{
		Enemy_txt.text = GameData.m_enemyCheck.ToString();
	}

	void TimeChecked()
	{
		if(GameData.pState != PlayerState.Death)
		{
			if(SceneManager.GetActiveScene().name == "Stage1")
			{
				GameData.playTimeCurrent -= 1 * Time.deltaTime;
			}
			else if(SceneManager.GetActiveScene().name == "Stage2")
			{
				GameData.playTimeCurrent -= 2 * Time.deltaTime;
			}
			
			TimeCheck.value = GameData.playTimeCurrent;
			TimeCheck.maxValue = GameData.playTimeMax;
			if(GameData.playTimeCurrent < 0)
			{
				Destroy(player);
			}
		}
	}

	public void GetCoin() //블러드 먹었을경우 +1
	{
		GameData.m_blood += 3;
	}

	public void GetTime() //타임아템획득시 +4
	{
		GameData.playTimeCurrent += 4f;
	}

	public void HpMinus() //체력 --
	{
		HpCHeck.value = GameData.CurrentHp;
		HpCHeck.maxValue = GameData.HpMax;
		GameData.CurrentHp -= 6;
		//Hp가 0 보다 작을 때 죽음.
		if(GameData.CurrentHp <= 0)
		{
			Destroy(player);
			GS = GameState.End;
			GameOver();
		}
		Hpflag = 2;
	}

	public void EventControl(int num) // 글자를 다먹었을경우
	{
		if (num == 1) { event1 = num; bonus[num - 1].gameObject.SetActive(true); }
		if (num == 2) { event2 = num; bonus[num - 1].gameObject.SetActive(true); }
		if (num == 3) { event3 = num; bonus[num - 1].gameObject.SetActive(true); }
		if (num == 4) { event4 = num; bonus[num - 1].gameObject.SetActive(true); }
		if(event1 == 1 && event2 == 2 && event3 == 3 && event4 == 4)
		{
			EventPlay();
		}
	}

	void EventPlay() // 이벤트 버튼 띄우기
	{
		EventButton.gameObject.SetActive(true);
	}

	public void ClickEvent()
	{
		Event_UI.SetActive(true);
		GS = GameState.Pause;
		if (event1 == 1 && event2 == 2 && event3 == 3 && event4 == 4) //보너스 글자 다모았을경우 버튼 활성화
		{
			BounusButton.interactable = true;
		}
		else//보너스 글자 다모았을경우 버튼 비활성화
		{
			BounusButton.interactable = false;
		}
		if(GameData.m_enemyCheck >= 5) //해치운 적군의 수가 5이상일때 상점버튼 활성화
		{
			StoreButton.interactable = true;
		}
		else //상점 버튼비활성화
		{
			StoreButton.interactable = false;
		}
	}

	public void OffEvent() //이벤트 버튼에서 계속하기
	{
		Event_UI.SetActive(false);
		GS = GameState.Play;
	}

	public void OnPause()//일시정지 버튼을 눌렀을경우
	{
		Pause_UI.SetActive(true);
		GS = GameState.Pause;
	}

	public void OffPause() //일시정지->계속하기
	{
		Pause_UI.SetActive(false);
		GS = GameState.Play;
	}

	public void GoTitle()
	{
		Final_UI.SetActive(false);
		SceneManager.LoadScene("StartScene");
	}

	public void Reply()
	{
		if (replyFlag == 1)
		{
			if (SceneManager.GetActiveScene().name == "Stage1")
			{
				SceneManager.LoadScene("Stage1");
			}
			else if (SceneManager.GetActiveScene().name == "Stage2")
			{
				SceneManager.LoadScene("Stage2");
			}
			replyFlag = 2;
			Final_UI.SetActive(false);
			//초기화
			GameData.m_blood = 1300;
			GameData.m_enemyCheck = 0;
			GameData.CurrentHp = 85;
			event1 = event2 = event3 = event4 = 0;
		}
	}

	public void GoStore() //상점
	{
		GameData.m_enemyCheck = 0; // 적군 초기화
		SceneManager.LoadScene("StoreScene");
	}

	public void GoBounus() //보너스
	{
		//CurrentStage = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene("BonusScene");
	}

	public void GameOver()
	{
		GS = GameState.End;
		Final_UI.SetActive(true);
		ResultBlood.text = GameData.m_blood.ToString();
		replyFlag = 1;
		if (SceneManager.GetActiveScene().name == "Stage1")
		{
			ResultStage.text = "Stage1";
		}
		else if (SceneManager.GetActiveScene().name == "Stage2")
		{
			ResultStage.text = "Stage2";
		}
	}
}
