using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gerenciadorJogo : MonoBehaviour
{
    public bool gameLigado = false;
    public GameObject TelaGameOver;
    public personagem personagem;

    void Start()
    {
        gameLigado = false;
        Time.timeScale = 0;
        personagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<personagem>();
    }

    public bool EstadoJogo()
    {
        return gameLigado;
    }

    public void LigarJogo()
    {
        gameLigado = true;
        Time.timeScale = 1;
        TelaGameOver.SetActive(false);
    }

    public void PersonagemMorreu()
    {
        personagem.destruirOvos();
        TelaGameOver.SetActive(true);
        gameLigado = false;
        Time.timeScale = 0;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }

    public void Fechar()
    {
        Application.Quit();
    }
}
