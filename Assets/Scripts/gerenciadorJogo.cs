using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gerenciadorJogo : MonoBehaviour
{
    public bool gameLigado = false;
    public GameObject TelaGameOver;
    public GameObject TelaPause;
    public personagem personagem;
    private Camera Camera;
    public AudioSource Floresta;
    public AudioSource Fogo;
    public float passoCor = 0.0078f;
    public float passoSom = 0.005f;

    void Start()
    {
        gameLigado = false;
        Time.timeScale = 0;
        personagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<personagem>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (gameLigado)
        {
            if (!Floresta.isPlaying)
            {
                Floresta.Play();
            }
            if (!Fogo.isPlaying)
            {
                Fogo.Play();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
            return;
        }
        Floresta.Pause();
        Fogo.Pause();
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

    public void AtualizarCorFundo()
    {
        if (Camera.backgroundColor.b <= 0.55)
        {
            Camera.backgroundColor = new Color(Camera.backgroundColor.r, Camera.backgroundColor.g - passoCor, Camera.backgroundColor.b);
            return;
        }
        Camera.backgroundColor = new Color(Camera.backgroundColor.r, Camera.backgroundColor.g, Camera.backgroundColor.b - passoCor);
    }

    public void AtualizarSom()
    {
        if (Floresta.volume <= 0)
        {
            return;
        }
        Floresta.volume -= passoSom;
        Fogo.volume += passoSom;
    }

    public void Pause()
    {
        TelaPause.SetActive(true);
        gameLigado = false;
        Time.timeScale = 0;
    }

    public void Voltar()
    {
        TelaPause.SetActive(false);
        gameLigado = true;
        Time.timeScale = 1;
    }
}
