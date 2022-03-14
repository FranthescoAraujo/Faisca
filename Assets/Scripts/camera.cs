using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
	public GameObject GameObjectPersonagem;

	//Start is called before the first frame update

	void Start()
	{
		transform.position = new Vector3(GameObjectPersonagem.transform.position.x, GameObjectPersonagem.transform.position.y + 0.5f, transform.position.z);
	}

	//Update is called once per frame

	void Update()
	{
		Seguir();
	}

	void Seguir()
	{
		Vector3 seguirPersonagem = new Vector3(GameObjectPersonagem.transform.position.x, GameObjectPersonagem.transform.position.y + 0.5f, transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, seguirPersonagem, 0.1f);
	}
}
