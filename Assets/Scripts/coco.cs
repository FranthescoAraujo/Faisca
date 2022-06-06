using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coco : MonoBehaviour
{
	private Rigidbody2D Rigidbody2DCoco;
	private Rigidbody2D Rigidbody2DPersonagem;
	private float tempo;
	private float gravidade = -9.8f;
	private Vector2 velocidadeCoco = new Vector3(0, 0);
	private float variacao = 2f;

	void Start()
	{
		Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
		Rigidbody2DCoco = GetComponent<Rigidbody2D>();
		tempo = Random.Range(0.5f, 2f);
		Velocidade();
	}

    private void Update()
    {
		VelocidadeQueda();
	}

    void Velocidade()
	{
		velocidadeCoco.x = ((Rigidbody2DPersonagem.position.x + Random.Range(-1.0f, 1.0f) * variacao) - Rigidbody2DCoco.position.x) / tempo;
		velocidadeCoco.y = ((Rigidbody2DPersonagem.position.y + Random.Range(-1.0f, 1.0f) * variacao) - Rigidbody2DCoco.position.y - (gravidade * Mathf.Pow(tempo, 2) / 2)) / tempo;
		Rigidbody2DCoco.velocity = velocidadeCoco;
	}

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Agua")
		{
			Destroy(this.gameObject);
		}
	}

	private void VelocidadeQueda()
	{
		if (Rigidbody2DCoco.velocity.y < -9f)
		{
			Rigidbody2DCoco.velocity = new Vector2(Rigidbody2DCoco.velocity.x, -9f);
		}
		if (Rigidbody2DCoco.velocity.x < -10f)
		{
			Rigidbody2DCoco.velocity = new Vector2(-10f, Rigidbody2DCoco.velocity.y);
		}
		if (Rigidbody2DCoco.velocity.x > 10f)
		{
			Rigidbody2DCoco.velocity = new Vector2(10f, Rigidbody2DCoco.velocity.y);
		}
	}
}
