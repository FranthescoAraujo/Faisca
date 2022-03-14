using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coco : MonoBehaviour
{
	public Rigidbody2D Rigidbody2DCoco;
	public Rigidbody2D Rigidbody2DPersonagem;
	public float tempo;
	public float gravidade = -9.8f;
	Vector3 velocidadeCoco = new Vector3(0, 0, 0);
	public float variacao = 2f;

	// Start is called before the first frame update
	void Start()
	{
		Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
		tempo = Random.Range(0.5f, 2.0f);
		Velocidade();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Velocidade()
	{
		velocidadeCoco.x = ((Rigidbody2DPersonagem.position.x + Random.Range(-1.0f, 1.0f) * variacao) - Rigidbody2DCoco.position.x) / tempo;
		velocidadeCoco.y = ((Rigidbody2DPersonagem.position.y + Random.Range(-1.0f, 1.0f) * variacao) - Rigidbody2DCoco.position.y - (gravidade * Mathf.Pow(tempo, 2) / 2)) / tempo;

		Rigidbody2DCoco.velocity = velocidadeCoco;
	}
}
