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

	void Start()
	{
		GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();
		Animacao = GetComponent<Animator>();
		SpriteRendererPeixe = GetComponent<SpriteRenderer>();
		PosicaoInicial = transform.position;
	}

	void Update()
	{
		if (GJ.EstadoJogo() == true)
		{
			AndarOuPular();
			TempoPular();
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

	private void OnCollisionEnter2D(Collision2D colisao)
	{
		if (colisao.gameObject.tag == "DestroyBoomerang")
		{
			Destroy(colisao.gameObject);
			vidas--;
			if (vidas <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
