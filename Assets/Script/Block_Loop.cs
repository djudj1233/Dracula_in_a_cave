using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Loop : MonoBehaviour {
	public float Speed = 5;
	public Transform Azone;
	public Transform Bzone;
	public Transform[] Block;
	public int BlockCount = 0;
	public GameControl GC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GC.GS != GameState.Pause)
		{
			Move();
			if (Bzone.transform.position.x <= 0f)
			{
				Destroy(Azone.gameObject);
				Azone = Bzone;
				MakeBlock();
			}
		}
	}

	void Move()
	{
		Azone.transform.Translate(Vector3.left * Speed * Time.deltaTime);
		Bzone.transform.Translate(Vector3.left * Speed * Time.deltaTime);
	}

	void MakeBlock()
	{
		
		if(BlockCount < 3)
		{
			int index = Random.Range(0, Block.Length - 3);
			Bzone = Instantiate(Block[index], new Vector3(Azone.transform.position.x + 30, -2, 0), this.transform.rotation);
			BlockCount += 1;
		}
		else
		{
			if (BlockCount == 3)
			{
				Bzone = Instantiate(Block[Block.Length - 2], new Vector3(Azone.transform.position.x + 30, -2, 0), this.transform.rotation);
				BlockCount += 1;
			}
			else
			{
				Bzone = Instantiate(Block[Block.Length - 1], new Vector3(Azone.transform.position.x + 30, -2, 0), this.transform.rotation);
			}
		}
		
	}
}
