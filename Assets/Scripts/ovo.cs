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
	public float distanciaMaxima = 5.0f;
	public float aux = 0;
	public bool quebrarOvo = false;
	public float time;
	public float timePerseguir;
	public bool noChao = false;
	private Animator Animacao;
	private float velocidadeMaxima = -6f;

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
        else
        {
			Rigidbody2DOvo.velocity = new Vector2(Rigidbody2DOvo.velocity.x, velocidadeMaxima);
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
		TempoPerseguir();
		if (Mathf.Abs(posicaoInicial.x - Rigidbody2DOvo.transform.position.x) >= distanciaMaxima || timePerseguir >= 3.0f)
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

	void TempoPerseguir()
    {
		timePerseguir += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Ground")
		{
			noChao = true;
			Animacao.SetBool("Girando", true);
		}
		if (trigger.gameObject.tag == "Agua")
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Ground")
		{
			Destroy(this.gameObject);
		}
	}
}
