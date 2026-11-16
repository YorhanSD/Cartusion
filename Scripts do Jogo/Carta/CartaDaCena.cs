using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CartaDaCena : MonoBehaviour
{
    public List<Ataque> listaAtaque = new List<Ataque>();

    public int printID;
    public string printEspecie;
    public string printNome;
    public string printNomeAtaqueSelecionado;
    public int printVidaMaxima;
    public int printVida;
    public int printAtaque;
    public int printCouraca;
    public int printReacao;
    public int printAtaqueSelecionadoID;

    [SerializeField] private RectTransform reactTransform;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private ControlaTurnos turnos;

    private Canvas canvas;

    public CartaRuntime dados;

    public CartaOriginal cartaBase;

    public CasaDaCarta casaDaCarta;

    public EstadoDasCartas estado;

    public AplicaEfeitoDoAtaque aplicaEfeitoDoAtaque;

    public UICard uiCarta;

    [SerializeField] private Transform uiParent;

    [System.Obsolete]
    void Awake()
    {
        aplicaEfeitoDoAtaque = GetComponent<AplicaEfeitoDoAtaque>();
        canvas = GetComponentInParent<Canvas>();
        reactTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvas == null)
        {
            Debug.LogError($"Canvas não encontrado para a carta {gameObject.name}");
        }
    }

    void Start()
    {
        casaDaCarta = GetComponent<CasaDaCarta>();
    }

    public void GravaUI(CartaRuntime _cartaRuntime)
    {
        uiCarta.AtualizarUI(_cartaRuntime);
    }
    public void PrintaDados(CartaRuntime _cartaRuntime)
    {
        printID = _cartaRuntime.ID;
        printEspecie = _cartaRuntime.especieSelecionada;
        printNome = _cartaRuntime.nome;
        printVidaMaxima = _cartaRuntime.vidaMaxima;
        printVida = _cartaRuntime.vidaAtual;
        printAtaque = _cartaRuntime.ataqueAtual;
        printCouraca = _cartaRuntime.couracaAtual;
        printReacao = _cartaRuntime.reacao;
        printAtaqueSelecionadoID = _cartaRuntime.ataqueSelecionado_ID;
        printNomeAtaqueSelecionado = _cartaRuntime.nomeAtaqueSelecionado;
    }
}
