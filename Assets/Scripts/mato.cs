using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mato : MonoBehaviour
{
    //Função Lancar
    public Rigidbody2D Rigidbody2DPersonagem;
    public float distancia = 5.0f;
    public bool lancou = false;
    public GameObject PorcoEspinho;

    //Animator
    private Animator Animacao;

    private gerenciadorJogo GJ;


    // Start is called before the first frame update
    void Start()
    {
        GJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<gerenciadorJogo>();

        Animacao = GetComponent<Animator>();
        Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GJ.EstadoJogo() == true)
        {
            Lancar();
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
     

}
