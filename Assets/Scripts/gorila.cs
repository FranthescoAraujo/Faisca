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

    void Start()
    {
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();
        Animacao = GetComponent<Animator>();
        Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GJ.EstadoJogo() == true)
        {
            TempoLancar();
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
