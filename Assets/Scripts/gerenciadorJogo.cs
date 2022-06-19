using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class gerenciadorJogo : MonoBehaviour
{
    public bool gameLigado = false;
    public GameObject TelaGameOver;
    public GameObject TelaPause;
    public GameObject TelaFinal;
    public personagem personagem;
    private Camera Camera;
    private GameObject[] Fogs;
    private ColorGrading ColorGrading;
    private ChromaticAberration ChromaticAberration;
    private AutoExposure AutoExposure;
    public AudioSource Floresta;
    public AudioSource Fogo;
    private float passoCor = 0.0154f;
    private float passoSaturation = 1f;
    private float passoAzul = 2f;
    private float passoAberration = 0.006f;
    private float passoExposure = 0.02f;
    private float passoSom = 0.02f;
    private float passoFog = 0.01f;
    private bool isEscrevento;

    void Start()
    {
        gameLigado = false;
        Time.timeScale = 0;
        personagem = GameObject.FindGameObjectWithTag("Personagem").GetComponent<personagem>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Fogs = GameObject.FindGameObjectsWithTag("Fog");
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings<ColorGrading>(out ColorGrading);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings<ChromaticAberration>(out ChromaticAberration);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings<AutoExposure>(out AutoExposure);
    }

    private void Update()
    {
        if (gameLigado)
        {
            if (GameObject.FindGameObjectsWithTag("Boss").Length == 0)
            {
                if (!isEscrevento)
                {
                    gameLigado = false;
                    isEscrevento = true;
                    ColorGrading.saturation.value = -100f;
                    TelaFinal.SetActive(true);
                    personagem.Imobilizar();
                    InvokeRepeating("Escrever", 0f, 0.1f);
                }
            }
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
        ColorGrading.saturation.value = 0f;
        ColorGrading.mixerBlueOutBlueIn.value = 100f;
        ChromaticAberration.intensity.value = 0f;
        AutoExposure.keyValue.value = 1f;
        foreach (var Fog in Fogs)
        {
            var ColorFog = Fog.GetComponent<ParticleSystem>().main;
            ColorFog.startColor = new Color(1f, 1f, 1f, ColorFog.startColor.color.a + passoFog);
        }
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

    public void AtualizarSom()
    {
        if (Floresta.volume <= 0)
        {
            return;
        }
        Floresta.volume -= passoSom;
        Fogo.volume += passoSom;
    }

    public void AtualizarCor()
    {
        if (Camera.backgroundColor.g <= 0.68)
        {
            return;
        }
        if (Camera.backgroundColor.b <= 0.55)
        {
            Camera.backgroundColor = new Color(Camera.backgroundColor.r, Camera.backgroundColor.g - passoCor, Camera.backgroundColor.b);
            return;
        }
        Camera.backgroundColor = new Color(Camera.backgroundColor.r, Camera.backgroundColor.g, Camera.backgroundColor.b - passoCor);
    }

    public void AtualizarSaturation()
    {
        if (ColorGrading.saturation.value <= -50f)
        {
            return;
        }
        ColorGrading.saturation.value = ColorGrading.saturation.value - passoSaturation;
    }

    public void AtualizarAzul()
    {
        if (ColorGrading.mixerBlueOutBlueIn.value <= 0f)
        {
            return;
        }
        ColorGrading.mixerBlueOutBlueIn.value = ColorGrading.mixerBlueOutBlueIn.value - passoAzul;
    }

    public void AtualizarAberration()
    {
        if (ChromaticAberration.intensity.value >= 0.3f)
        {
            return;
        }
        ChromaticAberration.intensity.value = ChromaticAberration.intensity.value + passoAberration;
    }

    public void AtualizarExposure()
    {
        if (AutoExposure.keyValue.value >= 2)
        {
            return;
        }
        AutoExposure.keyValue.value = AutoExposure.keyValue.value + passoExposure;
    }

    public void AtualizarFog()
    {
        if (Fogs[0].GetComponent<ParticleSystem>().main.startColor.color.a >= 0.5f)
        {
            return;
        }
        foreach (var Fog in Fogs)
        {
            var ColorFog = Fog.GetComponent<ParticleSystem>().main;
            ColorFog.startColor = new Color(1f, 1f, 1f, ColorFog.startColor.color.a + passoFog);
        }
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

    private void Escrever()
    {
        GetComponent<final>().Escrever();
    }
}
