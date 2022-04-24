using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gerenciadorJogo : MonoBehaviour
{
    public bool gameLigado = false;
    public GameObject TelaGameOver;

    void Start()
    {
        gameLigado = false;
        Time.timeScale = 0;
    }

    public bool EstadoJogo()
    {
        return gameLigado;
    }

    public void LigarJogo()
    {
        gameLigado = true;
        Time.timeScale = 1;
    }

    public void PersonagemMorreu()
    {
        destroirOvos();
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

    void destroirOvos()
    {
        GameObject[] ovos = GameObject.FindGameObjectsWithTag("Ovo");
        foreach (GameObject ovo in ovos)
        {
            Destroy(ovo);
        }
    }
}
