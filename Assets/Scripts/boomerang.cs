using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerang : MonoBehaviour
{
    //Função DistanciaMaxima
    public float distanciaMaxima = 5.0f;
    public float aux = 0;

    //Função Lancar
    public Rigidbody2D Rigidbody2DBoomerang;

    //Função PrimeiraPosicao
    public Vector3 posicao;

    //Função CapturarMouse
    public Vector3 mouse;

    //Função VelocidadeXY
    public float velocidade = 10.0f;
    public float velocidadeX = 0;
    public float velocidadeY = 0;

    //Função CalcularAngulos
    private Rigidbody2D Rigidbody2DPersonagem;
    public bool isVolta = false;
    public float catetoOposto = 0;
    public float catetoAdjacente = 0;
    public float hipotenusa = 0;

    //Função Trigger
    public float contador = 0;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2DPersonagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<Rigidbody2D>();
        Rigidbody2DBoomerang.gravityScale = 0f;
        CapturarMouse();
        PrimeiraPosicao();
        CalcularAngulos(isVolta);
        VelocidadeXY();
        Lancar();    
    }

    // Update is called once per frame
    void Update()
    {
        DistanciaMaxima();
        CalcularAngulos(isVolta);
        VelocidadeXY();
        Lancar();
    }

    void CapturarMouse()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
    }

    void PrimeiraPosicao()
    {
        posicao = Rigidbody2DBoomerang.position;
    }

    void DistanciaMaxima()
    {
        aux = Mathf.Sqrt(Mathf.Pow(Rigidbody2DBoomerang.transform.position.x - posicao.x, 2) + Mathf.Pow(Rigidbody2DBoomerang.transform.position.y - posicao.y, 2));
        if (aux >= distanciaMaxima)
        {
            isVolta = true;
        }
    }

    void CalcularAngulos(bool isVolta)
    {
        if (isVolta == false)
        {
            catetoOposto = mouse.y - posicao.y;
            catetoAdjacente = mouse.x - posicao.x;
            hipotenusa = Mathf.Sqrt(Mathf.Pow(catetoOposto, 2) + Mathf.Pow(catetoAdjacente, 2));
        }
        else
        {
            catetoOposto = Rigidbody2DPersonagem.transform.position.y - Rigidbody2DBoomerang.transform.position.y;
            catetoAdjacente = Rigidbody2DPersonagem.transform.position.x - Rigidbody2DBoomerang.transform.position.x;
            hipotenusa = Mathf.Sqrt(Mathf.Pow(catetoOposto, 2) + Mathf.Pow(catetoAdjacente, 2));
        }
    }

    void VelocidadeXY()
    {
        velocidadeY = velocidade * catetoOposto / hipotenusa;
        velocidadeX = velocidade * catetoAdjacente / hipotenusa;
    }

    void Lancar()
    {
        Rigidbody2DBoomerang.velocity = new Vector3(velocidadeX, velocidadeY, 0);
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Personagem")
        {
            contador += 1;
            if (contador >= 2)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
