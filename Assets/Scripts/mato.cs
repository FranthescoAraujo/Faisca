using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mato : MonoBehaviour
{
    public Rigidbody2D Rigidbody2DPersonagem;
    public float distancia = 5.0f;
    public bool lancou = false;
    public GameObject PorcoEspinho;
    private Animator Animacao;
    private gerenciadorJogo GJ;
    public GameObject personagem;

    void Start()
    {
        personagem = GameObject.FindGameObjectWithTag("Personagem");
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();
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
        if ((Mathf.Abs(transform.position.x - Rigidbody2DPersonagem.transform.position.x) <= distancia) && lancou == false)
        {
            Vector3 pontoPorcoEspinho = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject LancaPorcoEspinho = Instantiate(PorcoEspinho, pontoPorcoEspinho, Quaternion.identity);
            lancou = true;
            Animacao.SetBool("Lancou", true);
        }
    }

    void iniciarScriptsInimigo()
    {
        if (Vector2.Distance(transform.position, personagem.transform.position) <= 15f)
        {
            Lancar();
        }
    }
}
