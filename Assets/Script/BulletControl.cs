using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
	public int dmage = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.x >= 2f) //|| this.gameObject.tag =="Land"
		{
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag =="Enemy")
		{
			GameData.m_enemyCheck += 1;
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
	}
}
