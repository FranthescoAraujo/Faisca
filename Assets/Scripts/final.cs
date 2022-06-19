using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class final : MonoBehaviour
{
    private string texto = "VOCÊ VENCEU...\n" +
                           "Porém, o oxigênio acabou e a temperatura da Terra aumentou, " +
                           "todas as árvores e animais morreram. \n " + 
                           "Nosso planeta se tornou inabitável. \n" +
                           ".\n" +
                           ".\n" +
                           ".\n" +
                           "Proteja nossas florestas, a sobrevivência da raça humana depende disso.\n" +
                           "\n" +
                           "Diga não às queimadas. ";
    private string mostrarTexto;
    public TMP_Text tmpText;
    public AudioSource pressSpace;
    public bool tecla1;
    public AudioSource press1;
    public AudioSource press2;
    private int posicaoLetra;

    public void Escrever()
    {
        if (posicaoLetra == texto.Length)
        {
            return;
        }
        mostrarTexto = texto.Substring(0, posicaoLetra);
        tmpText.text = mostrarTexto;
        if (char.IsWhiteSpace(texto[posicaoLetra]))
        {
            pressSpace.Play();
        } else
        {
            if (tecla1)
            {
                press1.Play();
                tecla1 = false;
            }
            if (!tecla1)
            {
                press2.Play();
                tecla1 = true;
            }
        }
        posicaoLetra++;
    }
}
