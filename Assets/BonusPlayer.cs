using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BonusPlayer : MonoBehaviour {
	public GameObject Dracula;
	public PlayerState pState;
	Rigidbody rig;
	public float Jump_power = 300f;               //점프 파워
	public float speed = 0.35f;
	public Text txt_gold;                         //스코어텍스트
	
	//키보드
	float movrate_x;
	float movrate_z;
	int count;
	
	// Use this for initialization
	void Start () {
		rig = this.GetComponent<Rigidbody>();     //물리 속성연결
		pState = PlayerState.Running;
		Running();
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		rig.WakeUp();
		InputKeyCheck();
	}

	void InputKeyCheck()
	{
		float key = Input.GetAxis("Horizontal");
		float amtMove = speed * Time.smoothDeltaTime;

		if (key > 0)
		{
			//왼쪽이동
			Vector3 scale = transform.localScale;
			scale.x = -Mathf.Abs(scale.x);
			transform.localScale = scale;
			transform.Translate(Vector3.right * amtMove * key);
		}
		else if (key < 0)
		{
			//오른쪽 이동
			Vector3 scale = transform.localScale;
			scale.x = Mathf.Abs(scale.x);
			transform.localScale = scale;
			transform.Translate(Vector3.right * amtMove * key);
		}
		if (Application.isEditor)
		{
			movrate_x = Input.GetAxis("Horizontal");
			movrate_z = Input.GetAxis("Vertical");
		}
		this.transform.Translate(Vector3.right * movrate_x * speed);
		this.transform.Translate(Vector3.left * movrate_z * speed);

		if (Input.GetKeyDown(KeyCode.Space) == true) //|| Input.GetMouseButtonDown(0) == true)
		{
			if (pState == PlayerState.Running)
			{
				Jump();
			}
		}
	}

	public void Running()
	{
		pState = PlayerState.Running;
	}

	public void Jump()
	{
		pState = PlayerState.Jump;
		//SoundPlay(1);
		rig.AddForce(Vector3.up * Jump_power, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (pState != PlayerState.Running)
			Running();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "BCoin")
		{
			Destroy(other.gameObject);
			
			count = count += 1;
			GameData.m_blood += count;
			txt_gold.text = count.ToString();
		}

		if (other.gameObject.tag == "Moon")
		{
			//print(GameData.CurrentStage);
			SceneManager.LoadScene("Stage1");
		}
	}
}
