using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState { Running, Jump, D_Jump, Death }

public class PlayerControl : MonoBehaviour
{
	Rigidbody rig;
	public PlayerState pState;
	public GameState gState;
	public float Jump_power = 650f;
	public Transform firePoint; //발사점
	public Transform bullet; //총알
	public GameControl GC;
	public Transform PlayerPosition;

	// Use this for initialization
	void Start()
	{
		rig = this.GetComponent<Rigidbody>();
		Running();
	}

	// Update is called once per frame
	void Update()
	{
		if(GC.GS != GameState.Pause)
		{
			rig.WakeUp();
			MoveCheck();
			if (Input.GetKeyDown(KeyCode.Space) == true)//|| Input.GetMouseButtonDown(0) ==
			{
				if (pState == PlayerState.Running)
				{
					Jump();
				}
				else if (pState == PlayerState.Jump)
				{
					D_Jump();
				}
			}
			if (Input.GetKeyDown(KeyCode.Z) == true)
			{
				Btn_Fire();
			}
			if(this.transform.position.y >= 7.25f)
			{
				Vector3 vector = transform.position;
				vector.y = 7.25f;
				this.transform.position= vector;
			}
		}
	}

	void Running()
	{
		pState = PlayerState.Running;
	}

	void Jump()
	{
		pState = PlayerState.Jump;
		rig.AddForce(Vector3.up * Jump_power);
	}

	void D_Jump()
	{
		pState = PlayerState.D_Jump;
		rig.AddForce(Vector3.up * Jump_power);
	}

	void GetCoin()
	{
		GC.GetCoin();
	}

	void GetTime()
	{
		GC.GetTime();
	}
	
	void MoveCheck() //캐릭터가 일정 위치 보이지 않을경우 포지션 변경
	{
		if(this.gameObject.transform.position.x <= -10.7f)
		{
			Vector3 vector = transform.position;
			vector.x = -7.5f;
			transform.position = vector;
		}
	}

	public void Btn_Fire()
	{
		Transform temp = Instantiate(bullet, firePoint.position, firePoint.rotation);
		temp.GetComponent<Rigidbody>().AddForce(temp.forward * 500f);
	}

	void GameOver()
	{
		pState = PlayerState.Death;
		GC.GameOver();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (pState != PlayerState.Running && pState != PlayerState.Death)
		{
			if(collision.gameObject.tag == "Block" || collision.gameObject.tag == "Enemy")
			{
				GC.HpMinus();
			}
			Running();
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		rig.WakeUp();
		if(other.gameObject.tag == "DeathZone" && pState!=PlayerState.Death)
		{
			GameOver();
		}
		if (other.gameObject.tag == "Blood")
		{
			GetCoin();
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag == "Time")
		{
			GetTime();
			Destroy(other.gameObject);
		}
		//2조게임 해당 글자를 먹었을때
		if (other.gameObject.tag == "event1")
		{
			Destroy(other.gameObject);
			GC.EventControl(1);
		}
		if (other.gameObject.tag == "event2")
		{
			Destroy(other.gameObject);
			GC.EventControl(2);
		}
		if (other.gameObject.tag == "event3")
		{
			Destroy(other.gameObject);
			GC.EventControl(3);
		}
		if (other.gameObject.tag == "event4")
		{
			Destroy(other.gameObject);
			GC.EventControl(4);
		}
		if (other.gameObject.tag == "NextStage")
		{
			SceneManager.LoadScene("BetweenStage");
		}
		if (other.gameObject.tag == "Ending")
		{
			SceneManager.LoadScene("EndingScene");
		}
	}
}

