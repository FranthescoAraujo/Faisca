using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passaro : MonoBehaviour
{
	private SpriteRenderer SpriteRendererPassaro;
	private Vector3 PosicaoInicial;
	public float velocidade = 1f;
	public float distanciaInicial = -10.0f;
	public float distanciaFinal = 10.0f;
	private Animator Animacao;
	public float tempo = 0;
	public GameObject Ovo;
	public int vidas = 2;
	private gerenciadorJogo GJ;

	void Start()
	{
		GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();
		Animacao = GetComponent<Animator>();
		SpriteRendererPassaro = GetComponent<SpriteRenderer>();
		PosicaoInicial = transform.position;
	}

	void Update()
	{
		if (GJ.EstadoJogo() == true)
		{
			Andar();
			TempoOvo();
		}
	}

	void Andar()
	{
		transform.position = new Vector3(transform.position.x + velocidade * Time.deltaTime, transform.position.y, transform.position.z);
		if (transform.position.x > (PosicaoInicial.x + distanciaFinal))
		{
			velocidade = -Mathf.Abs(velocidade);
			SpriteRendererPassaro.flipX = true;
		}
		else if (transform.position.x < (PosicaoInicial.x + distanciaInicial))
		{
			velocidade = Mathf.Abs(velocidade);
			SpriteRendererPassaro.flipX = false;
		}
	}

	void TempoOvo()
	{
		tempo += Time.deltaTime;
		if (tempo >= 3.0f)
		{
			AtaqueOvo();
			tempo = 0;
		}
	}

	void AtaqueOvo()
	{
		Vector3 pontoOvo = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
		GameObject OvoDisparo = Instantiate(Ovo, pontoOvo, Quaternion.identity);
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
