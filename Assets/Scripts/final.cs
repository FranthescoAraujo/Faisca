using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class final : MonoBehaviour
{
    private string texto = "VOC� VENCEU...\n" +
                           "Por�m, o oxig�nio acabou e a temperatura da Terra aumentou, " +
                           "todas as �rvores e animais morreram. \n " + 
                           "Nosso planeta se tornou inabit�vel. \n" +
                           ".\n" +
                           ".\n" +
                           ".\n" +
                           "Proteja nossas florestas, a sobreviv�ncia da ra�a humana depende disso.\n" +
                           "\n" +
                           "Diga n�o �s queimadas. ";
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
