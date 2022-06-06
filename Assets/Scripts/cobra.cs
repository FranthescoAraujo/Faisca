using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cobra : MonoBehaviour
{
	private Rigidbody2D Rigidbody2DPersonagem;
	private float distanciaMinima = 5.0f;
	private float distanciaMinimaY = 1.0f;
	private SpriteRenderer SpriteRendererCobra;
	private float velocidade = 1f;
	private float velocidadeProximo = 4f;
	private float distanciaInicial = -2.0f;
	private float distanciaFinal = 2.0f;
	private Vector3 PosicaoInicial;
	private gerenciadorJogo GJ;
	private int vidas = 3;
	private float meuTempoDano;
	private bool podeTomarDano = true;
	private Color alpha;
	private GameObject personagem;
	private AudioSource Hit;
	private AudioSource Cobra;

	void Start()
	{
		personagem = GameObject.FindGameObjectWithTag("Personagem");
		GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();
		Hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<AudioSource>();
		PosicaoInicial = transform.position;
		Rigidbody2DPersonagem = personagem.GetComponent<Rigidbody2D>();
		SpriteRendererCobra = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (GJ.EstadoJogo() == true)
		{
			iniciarScriptsInimigo();
		}
	}

	void Andar()
	{
		{
			transform.position = new Vector3(transform.position.x + velocidade * Time.deltaTime, transform.position.y, transform.position.z);
			if (transform.position.x > (PosicaoInicial.x + distanciaFinal))
			{
				velocidade = -Mathf.Abs(velocidade);
				SpriteRendererCobra.flipX = true;
			}
			else if (transform.position.x < (PosicaoInicial.x + distanciaInicial))
			{
				velocidade = Mathf.Abs(velocidade);
				SpriteRendererCobra.flipX = false;
			}
		}
	}

	void Perseguir()
	{
		transform.position = new Vector3(transform.position.x + velocidadeProximo * Time.deltaTime, transform.position.y, transform.position.z);
		if (transform.position.x > Rigidbody2DPersonagem.transform.position.x)
		{
			velocidadeProximo = -Mathf.Abs(velocidadeProximo);
			SpriteRendererCobra.flipX = true;
		}
		else if (transform.position.x < Rigidbody2DPersonagem.transform.position.x)
		{
			velocidadeProximo = Mathf.Abs(velocidadeProximo);
			SpriteRendererCobra.flipX = false;
		}
	}

	void Movimento()
	{
		if (Mathf.Abs(transform.position.x - Rigidbody2DPersonagem.transform.position.x) <= distanciaMinima 
			&& Mathf.Abs(transform.position.y - Rigidbody2DPersonagem.transform.position.y ) <= distanciaMinimaY)
		{
			Perseguir();
		}
		else
		{
			Andar();
		}
	}

	private void OnTriggerStay2D(Collider2D colisao)
	{
		if (colisao.gameObject.tag == "DestroyBoomerang")
		{
			if (podeTomarDano)
			{
				Hit.Play();
				podeTomarDano = false;
				Destroy(colisao.gameObject);
				alpha = GetComponent<SpriteRenderer>().material.color;
				alpha.a = 0.5f;
				GetComponent<SpriteRenderer>().material.color = alpha;
				vidas--;
				if (vidas <= 0)
				{
					Destroy(this.gameObject);
				}
			}
		}
	}

    void Dano()
	{
		if (!podeTomarDano)
		{
			TemporizadorDano();
		}
	}

	void TemporizadorDano()
	{
		meuTempoDano += Time.deltaTime;
		if (meuTempoDano > 0.5f)
		{
			podeTomarDano = true;
			meuTempoDano = 0;
			alpha.a = 1f;
			GetComponent<SpriteRenderer>().material.color = alpha;
		}
	}

	void iniciarScriptsInimigo()
	{
		if (Vector2.Distance(transform.position, personagem.transform.position) <= 15f)
		{
			if (!Cobra.isPlaying)
            {
				Cobra.Play();
			}
			Movimento();
			Dano();
		}
	}

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Agua")
		{
			Destroy(this.gameObject);
		}
	}
}
