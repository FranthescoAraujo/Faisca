using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
	public GameObject GameObjectPersonagem;
	public float VELOCIDADE_CAMERA = 0.5f;

	void Start()
	{
		transform.position = new Vector3(GameObjectPersonagem.transform.position.x, GameObjectPersonagem.transform.position.y + 0.5f, transform.position.z);
	}

	void Update()
	{
		Seguir();
		PersonagemMorreu();
	}

	void Seguir()
	{
		Vector3 seguirPersonagem = new Vector3(GameObjectPersonagem.transform.position.x, GameObjectPersonagem.transform.position.y + 0.5f, transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, seguirPersonagem, VELOCIDADE_CAMERA);
	}

	void PersonagemMorreu()
    {
		if (GameObjectPersonagem.GetComponent<personagem>().morreu)
        {
			transform.position = new Vector3(GameObjectPersonagem.transform.position.x, GameObjectPersonagem.transform.position.y + 0.5f, transform.position.z);
		}
    }
}
