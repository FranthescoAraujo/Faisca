using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porcoEspinho : MonoBehaviour
{
    public Rigidbody2D Rigidbody2DPorcoEspinho;
    public SpriteRenderer SpriteRendererPorcoEspinho;
    public float velocidade = 0.01f;
    public bool isChao = false;
    public Vector3 PosicaoInicial;
    public float distanciaFinal = 5.0f;
    public float distanciaInicial = -5.0f;
    public float velocidadeX = 5.0f;
    private Animator Animacao;
    public int vidas = 4;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        SpriteRendererPorcoEspinho = GetComponent<SpriteRenderer>();
        Rigidbody2DPorcoEspinho = GetComponent<Rigidbody2D>();
        Animacao = GetComponent<Animator>();
        Lancar();
    }

    void Update()
    {
        Andar();
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Ground")
        {
            Rigidbody2DPorcoEspinho.velocity = new Vector2(0, 0);
            PosicaoChao();
        }
    }

    void Andar()
    {
        if (isChao == true)
        {
            transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);

            if (transform.position.x > (PosicaoInicial.x + distanciaFinal))
            {
                velocidade = -Mathf.Abs(velocidade);
                SpriteRendererPorcoEspinho.flipX = true;
            }
            else if (transform.position.x < (PosicaoInicial.x + distanciaInicial))
            {
                velocidade = Mathf.Abs(velocidade);
                SpriteRendererPorcoEspinho.flipX = false;
            }
        }
    }

    void Lancar()
    {
        velocidadeX = Random.Range(-velocidadeX, velocidadeX);
        Rigidbody2DPorcoEspinho.velocity = new Vector2(velocidadeX,0);
        Rigidbody2DPorcoEspinho.AddForce(transform.up * 300f);
    }

    void PosicaoChao()  
    {
        if (isChao == false)
        {
            Animacao.SetBool("Chao", true);
            PosicaoInicial = transform.position;
            isChao = true;
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
