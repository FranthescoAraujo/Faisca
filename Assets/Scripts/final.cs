using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class final : MonoBehaviour
{
    private string texto = "O que � o Lorem Ipsum? \n " + 
                           "O Lorem Ipsum � um texto modelo da ind�stria tipogr�fica e de impress�o. " + 
                           "O Lorem Ipsum tem vindo a ser o texto padr�o usado por estas ind�strias desde o ano de 1500, " + 
                           "quando uma misturou os caracteres de um texto para criar um esp�cime de livro.Este texto n�o s� sobreviveu 5 s�culos, " + 
                           "mas tamb�m o salto para a tipografia electr�nica, mantendo-se essencialmente inalterada.";
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
