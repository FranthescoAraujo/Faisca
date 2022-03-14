using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cobra : MonoBehaviour
{
	//Função Perseguir
	private Rigidbody2D Rigidbody2DPersonagem;
	public float distanciaMinima = 5.0f;

	//Função Andar
	private SpriteRenderer SpriteRendererCobra;
	public float velocidade = 0.005f;
	public float velocidadeProximo = 0.008f;
	public float distanciaInicial = -2.0f;
	public float distanciaFinal = 2.0f;
	public Vector3 PosicaoInicial;

	public float teste = 0;
	public float teste2 = 0;

	private gerenciadorJogo GJ;

	//Start is called before the first frame update

	//Vidas
	public int vidas = 3;

	void Start()
	{
		GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();

		PosicaoInicial = transform.position;
		Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
		SpriteRendererCobra = GetComponent<SpriteRenderer>();
	}

	//Update is called once per frame

	void Update()
	{
		if (GJ.EstadoJogo() == true)
		{
			Movimento();
		}
	}

	void Andar()
	{
		{
			transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);

			teste = PosicaoInicial.x + distanciaFinal;
			teste2 = PosicaoInicial.x + distanciaInicial;

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
		transform.position = new Vector3(transform.position.x + velocidadeProximo, transform.position.y, transform.position.z);

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

		if (Mathf.Abs(transform.position.x - Rigidbody2DPersonagem.transform.position.x) <= distanciaMinima)
		{
			Perseguir();
		}
		else
		{
			Andar();
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
