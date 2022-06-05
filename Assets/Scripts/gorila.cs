using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gorila : MonoBehaviour
{
    public Rigidbody2D Rigidbody2DPersonagem;
    public float distancia = 15.0f;
    public bool lancou = false;
    public GameObject Coco;
    private Animator Animacao;
    public int vidas = 10;
    public float tempoCoco = 0;
    private gerenciadorJogo GJ;
    float meuTempoDano;
    bool podeTomarDano = true;
    Color alpha;
    public GameObject personagem;
    public AudioSource Hit;
    public AudioSource Gorila;

    void Start()
    {
        personagem = GameObject.FindGameObjectWithTag("Personagem");
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();
        Hit = GameObject.FindGameObjectWithTag("Hit").GetComponent<AudioSource>();
        Animacao = GetComponent<Animator>();
        Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GJ.EstadoJogo() == true)
        {
            iniciarScriptsInimigo();
        }
    }

    void Lancar()
    {
        if (Rigidbody2DPersonagem.transform.position.x < 28.0f && Rigidbody2DPersonagem.transform.position.y < -10.0f)
        {
            if ((Mathf.Abs(transform.position.x - Rigidbody2DPersonagem.transform.position.x) <= distancia) && lancou == false)
            {
                Vector3 posicaoCoco = new Vector3(transform.position.x + 0.8f, transform.position.y + 0.8f, transform.position.z);
                GameObject LancarCoco = Instantiate(Coco, posicaoCoco, Quaternion.identity);
            }
        }
    }

    void TempoLancar()
    {
        tempoCoco += Time.deltaTime;
        if (tempoCoco >= (0.5f * vidas))
        {
            Animacao.SetBool("JogaCoco", true);
            Lancar();
            Lancar();
            tempoCoco = 0;
        }
        else if (tempoCoco >= (0.4f * vidas))
        {
            Animacao.SetBool("JogaCoco", false);
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
        if (Vector2.Distance(transform.position, personagem.transform.position) <= 35f)
        {
            if (!Gorila.isPlaying)
            {
                Gorila.Play();
            }
            TempoLancar();
            Dano();
        }
    }
}
