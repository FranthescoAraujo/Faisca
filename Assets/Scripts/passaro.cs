using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passaro : MonoBehaviour
{
	//Função Andar
	private SpriteRenderer SpriteRendererPassaro;
	private Vector3 PosicaoInicial;
	public float velocidade = 0.01f;
	public float distanciaInicial = -10.0f;
	public float distanciaFinal = 10.0f;

	//Animator
	private Animator Animacao;

	//Função Ovo
	public float tempo = 0;
	public GameObject Ovo;

	public int vidas = 2;

	private gerenciadorJogo GJ;

	//Start is called before the first frame update

	void Start()
	{
		GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();

		Animacao = GetComponent<Animator>();
		SpriteRendererPassaro = GetComponent<SpriteRenderer>();
		PosicaoInicial = transform.position;
	}

	//Update is called once per frame

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
		transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);

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
