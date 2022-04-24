using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovo : MonoBehaviour
{
	public Rigidbody2D Rigidbody2DOvo;
	public Rigidbody2D Rigidbody2DPersonagem;
	public SpriteRenderer SpriteRendererOvo;
	Vector3 posicaoInicial;
	Vector3 posicaoInicialPersonagem;
	public float velocidade = 0.02f;
	public float distanciaMaxima = 10.0f;
	public float aux = 0;
	public bool quebrarOvo = false;
	public float time = 1.0f;
	public bool noChao = false;
	private Animator Animacao;

	void Start()
	{
		Animacao = GetComponent<Animator>();
		Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
		Rigidbody2DOvo = GetComponent<Rigidbody2D>();
		SpriteRendererOvo = GetComponent<SpriteRenderer>();
		distanciaMaxima *= Random.value;
		posicaoInicial = Rigidbody2DOvo.transform.position;
		posicaoInicialPersonagem = Rigidbody2DPersonagem.transform.position;
	}

	void Update()
	{
		if (noChao == true)
		{
			Stop();
		}
	}

	void Perseguir()
	{
		if (posicaoInicial.x < posicaoInicialPersonagem.x)
		{
			Rigidbody2DOvo.position = new Vector3(Rigidbody2DOvo.transform.position.x + velocidade, Rigidbody2DOvo.transform.position.y, 0);
			SpriteRendererOvo.flipX = true;
		}
		else
		{
			Rigidbody2DOvo.position = new Vector3(Rigidbody2DOvo.transform.position.x - velocidade, Rigidbody2DOvo.transform.position.y, 0);
			SpriteRendererOvo.flipX = false;
		}
	}

	void Stop()
	{
		aux = Mathf.Abs(posicaoInicial.x - Rigidbody2DOvo.transform.position.x);
		if (Mathf.Abs(posicaoInicial.x - Rigidbody2DOvo.transform.position.x) >= distanciaMaxima)
		{
			Tempo();
			Animacao.SetBool("Quebrado", true);
			Rigidbody2DOvo.velocity = new Vector3(0, 0, 0);
			if (time >= 3.0f)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			Perseguir();
		}
	}

	void Tempo()
	{
		time += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Ground")
		{
			noChao = true;
			Animacao.SetBool("Girando", true);
		}
	}
}
