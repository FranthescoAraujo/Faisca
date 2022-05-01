using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gerenciadorJogo : MonoBehaviour
{
    public bool gameLigado = false;
    public GameObject TelaGameOver;
    public personagem personagem;
    private Camera Camera;
    Color background;
    public float passoCor = 0.0078f;

    void Start()
    {
        gameLigado = false;
        Time.timeScale = 0;
        personagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<personagem>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Camera.clearFlags = CameraClearFlags.SolidColor;
        background = Camera.backgroundColor;
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

    public void atualizarCorFundo()
    {
        if (background.b <= 0.55)
        {
            background = new Color(background.r, background.g - passoCor, background.b);
            return;
        }
        background = new Color(background.r, background.g, background.b - passoCor);
    }
}
