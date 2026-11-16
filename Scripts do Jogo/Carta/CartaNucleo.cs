using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartaNucleo : MonoBehaviour
{
    public bool nucleoOponente;

    public string nome;

    public Slider barraCouraca;

    public Slider barraVida;

    public int reacao;

    public int vidaAtual;

    public int vidaMAX;

    public int couracaAtual;

    public int couracaMAX;

    public TextMeshProUGUI nomeTMPRO;

    public TextMeshProUGUI vidaMaximaTMPRO;

    public TextMeshProUGUI vidaAtualTMPRO;

    public TextMeshProUGUI couracaAtualTMPRO;

    public TextMeshProUGUI reacaoTMPRO;

    [SerializeField] private RectTransform reactTransform;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private EntraESaiCartas entraESaiCartas;
    [SerializeField] private Regras_Jogo regrasJogo;
    [SerializeField] private ControleDeDano controleDano;
    [SerializeField] private CasaTemporaria casaTemporaria;
    [SerializeField] private BancoCards bancoCartas;
    [SerializeField] private VerificaDanoPorReacao verificaDanoPorReacao;

    private Canvas canvas;

    [SerializeField] private Transform uiParent;

    [System.Obsolete]
    void Awake()
    {
        verificaDanoPorReacao = FindObjectOfType<VerificaDanoPorReacao>();
        bancoCartas = FindObjectOfType<BancoCards>();
        casaTemporaria = FindObjectOfType<CasaTemporaria>();
        controleDano = FindObjectOfType<ControleDeDano>();
        regrasJogo = FindObjectOfType<Regras_Jogo>();
        entraESaiCartas = FindObjectOfType<EntraESaiCartas>();
        canvas = GetComponentInParent<Canvas>();
        reactTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvas == null)
        {
            Debug.LogError($"Canvas não encontrado para a carta {gameObject.name}");
        }
    }

    private void Start()
    {
        AtualizarUI();
    }

    private void OnTriggerEnter2D(Collider2D _carta)
    {
        if (nucleoOponente == false)
        {
            if (_carta.gameObject.CompareTag("Carta Oponente"))
            {
                CartaDaCena cartaOponente = _carta.GetComponent<CartaDaCena>();

                if (couracaAtual > 0)
                {
                    couracaAtual -= 10;
                }
                else
                {
                    vidaAtual -= cartaOponente.dados.ataqueAtual;

                    cartaOponente.dados.vidaAtual -= reacao;

                    CartaDaCena pseudoCartaNucleo = new CartaDaCena();

                    pseudoCartaNucleo.dados.reacao = reacao;

                    DanoPorReacaoDoNucleo(cartaOponente, pseudoCartaNucleo);

                    Debug.Log("Carta Oponente : " + cartaOponente.dados.nome + " está atacando o núcleo do Jogador");

                    if(vidaAtual <= 0)
                    {
                        regrasJogo.AtivaTela_Derrota();
                    }
                }

                cartaOponente.estado.SetMoveuSe(true);

                AtualizarUI();

                MandaACartaDeVoltaParaACasaDela(cartaOponente);
            }
        }
        else
        {
            if (_carta.gameObject.CompareTag("Carta Jogador"))
            {
                CartaDaCena cartaJogador = _carta.GetComponent<CartaDaCena>();

                if (couracaAtual > 0)
                {
                    couracaAtual -= 10;
                }
                else
                {
                    vidaAtual -= cartaJogador.dados.ataqueAtual;

                    cartaJogador.dados.vidaAtual -= reacao;

                    CartaDaCena pseudoCartaNucleo = new CartaDaCena();

                    pseudoCartaNucleo.dados.reacao = reacao;

                    DanoPorReacaoDoNucleo(cartaJogador, pseudoCartaNucleo);

                    Debug.Log("Carta Jogador : " + cartaJogador.dados.nome + " está atacando o núcleo do Oponente");

                    if (vidaAtual <= 0)
                    {
                        regrasJogo.AtivaTela_Vitoria();
                    }
                }

                cartaJogador.estado.SetMoveuSe(true);

                AtualizarUI();

                MandaACartaDeVoltaParaACasaDela(cartaJogador);
            }
        }
    }
    public bool AtaqueDaCartaPermiteDanoPorReacao(CartaDaCena _cartaAtacante)
    {
        foreach (Ataque ataque in _cartaAtacante.listaAtaque)
        {
            if (ataque.GetAtaqueAtivado() == true && _cartaAtacante.aplicaEfeitoDoAtaque.GetImuneReacao() == true)
            {
                Debug.Log($"A carta : {_cartaAtacante.dados.nome} com seu Ataque: {ataque.nome} possuí proteção contra a reação da carta adversária");
                return false;
            }
        }

        return false;
    }
    public void DanoPorReacaoDoNucleo(CartaDaCena _ataca, CartaDaCena _defende)
    {
        verificaDanoPorReacao.DanoPorReacao(_ataca, _defende);
    }
    public void MandaACartaDeVoltaParaACasaDela(CartaDaCena cartaAtacante)
    {
        Casa casaOrigem = cartaAtacante.casaDaCarta.GetCasaAtual();

        if (casaOrigem == null)
        {
            Debug.LogError("Casa de origem não encontrada!");
            return;
        }

        entraESaiCartas.RetornaCartaParaCasa(cartaAtacante, casaOrigem);
    }

    public void AtualizarUI()
    {
        barraCouraca.value = couracaAtual;
        barraCouraca.maxValue = couracaMAX;

        barraVida.maxValue = vidaMAX;
        barraVida.value = vidaAtual;

        if (vidaAtualTMPRO != null && vidaMaximaTMPRO != null && couracaAtualTMPRO != null)
        {
            vidaAtualTMPRO.text = vidaAtual.ToString();
            vidaMaximaTMPRO.text = vidaMAX.ToString();

            couracaAtualTMPRO.text = couracaAtual.ToString();
        }
        else
        {
            Debug.Log(this.gameObject.name + " tem componentes nulos");
        }

        if (nomeTMPRO != null && couracaAtualTMPRO != null && reacaoTMPRO != null)
        {
            nomeTMPRO.text = nome;
            couracaAtualTMPRO.text = $"{couracaAtual.ToString()}";
            reacaoTMPRO.text = $"RE {reacao.ToString()}";
        }
        else
        {
            Debug.Log(this.gameObject.name + " tem componentes nulos");
        }
    }
}
