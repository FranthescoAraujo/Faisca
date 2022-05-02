using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peixe : MonoBehaviour
{
	private SpriteRenderer SpriteRendererPeixe;
	private Vector3 PosicaoInicial;
	public float velocidade = 0.01f;
	public float distanciaInicial = -3.0f;
	public float distanciaFinal = 3.0f;
	public float distanciaPulo = 4f;
	public float tempo = 0;
	public float velocidadePulo = 0.02f;
	public bool pulando = false;
	public int vidas = 2;
	private Animator Animacao;
	private gerenciadorJogo GJ;
	float meuTempoDano;
	bool podeTomarDano = true;
	Color alpha;
	public GameObject personagem;
	public AudioSource Hit;

	void Start()
	{
		personagem = GameObject.FindGameObjectWithTag("Personagem");
		GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();
		Hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<AudioSource>();
		Animacao = GetComponent<Animator>();
		SpriteRendererPeixe = GetComponent<SpriteRenderer>();
		PosicaoInicial = transform.position;
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
		Animacao.SetBool("Pulando", false);
		transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);
		if (transform.position.x > (PosicaoInicial.x + distanciaFinal))
		{
			velocidade = -Mathf.Abs(velocidade);
			SpriteRendererPeixe.flipX = true;
		}
		else if (transform.position.x < (PosicaoInicial.x + distanciaInicial))
		{
			velocidade = Mathf.Abs(velocidade);
			SpriteRendererPeixe.flipX = false;
		}
	}

	void Pular()
	{
		Animacao.SetBool("Pulando", true);
		transform.position = new Vector3(transform.position.x, transform.position.y + velocidadePulo, transform.position.z);
		if (transform.position.y > (PosicaoInicial.y + distanciaPulo))
		{
			velocidadePulo = -velocidadePulo;
			SpriteRendererPeixe.flipY = true;
		}
	}

	void AndarOuPular()
	{
		if (tempo >= 3.0f)
		{
			Pular();
		}
		else
		{
			Andar();
		}
	}

	void TempoPular()
	{
		tempo += Time.deltaTime;
		if ((transform.position.y <= PosicaoInicial.y) && (tempo >= 4.0f))
		{
			tempo = 0;
			velocidadePulo = -velocidadePulo;
			SpriteRendererPeixe.flipY = false;
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
			AndarOuPular();
			TempoPular();
			Dano();
		}
	}

	private void OnCollisionEnter2D(Collision2D colisao)
	{
		if (colisao.gameObject.tag == "Personagem")
		{
			Vector3 moveDirection = new Vector3(colisao.transform.position.x - transform.position.x, 0, 0);
			colisao.gameObject.GetComponent<Rigidbody2D>().AddForce(moveDirection * 3000f);
		}
	}
}
