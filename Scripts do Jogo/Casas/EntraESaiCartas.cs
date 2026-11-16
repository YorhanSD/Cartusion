using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EntraESaiCartas : MonoBehaviour
{
    public TrocaLugar trocaLugar;
    public SistemaCombate sistemaCombate;
    public LiberaCasas liberaCasas;
    public Mapeamento_Jogador mapeamentoJogador;
    public BancoCards bancoCards;
    public CasaTemporaria casaTemporaria;
    public IA_MapeamentoDeCases ia_MapeamentoDeCases;

    private bool soltou;
    private bool encostou;

    private CartaDaCena cartaDefende;
    private CartaDaCena cartaAtaca;

    [System.Obsolete]
    private void Start()
    {
        ia_MapeamentoDeCases = GetComponent<IA_MapeamentoDeCases>();
        casaTemporaria = GetComponent<CasaTemporaria>();
        trocaLugar = GetComponent<TrocaLugar>();
        sistemaCombate = GetComponent<SistemaCombate>();
        liberaCasas = GetComponent<LiberaCasas>();
        mapeamentoJogador = GetComponent<Mapeamento_Jogador>();
        bancoCards = GetComponent<BancoCards>();
    }
    
    public void RecebeCasasAtuais(int _idCartaQueDefende, int _idCartaQueAtaca)
    {
        if (bancoCards == null) return;

        CartaDaCena cartaDefende = bancoCards.geralCartaCenaLista.Find(c => c.dados.ID == _idCartaQueDefende);

        casaTemporaria.CasaDoDefensor(cartaDefende.casaDaCarta.GetCasaAtual());

        CartaDaCena cartaAtaca = bancoCards.geralCartaCenaLista.Find(c => c.dados.ID == _idCartaQueAtaca);

        casaTemporaria.CasaDoAtacante(cartaAtaca.casaDaCarta.GetCasaAtual());

        if (cartaAtaca != null && cartaDefende != null)
        {
            ChamaCombate(cartaDefende, cartaAtaca);

            //Debug.Log($"Carta : {cartaAtaca.dados.nome} ataca {cartaDefende.dados.nome}");
        }
    }
    public void RetornaCartaParaCasa(CartaDaCena carta, Casa casaOrigem)
    {
        if (carta == null || casaOrigem == null)
            return;

        StartCoroutine(RetornaCartaGradualmente(carta, casaOrigem));
    }

    private IEnumerator RetornaCartaGradualmente(CartaDaCena carta, Casa casaOrigem)
    {
        Vector3 inicio = carta.transform.position;
        Vector3 destino = casaOrigem.transform.position;

        float duracao = 0.5f;
        float tempo = 0f;

        while (tempo < duracao)
        {
            if (carta == null)
                yield break;

            tempo += Time.deltaTime;

            carta.transform.position = Vector3.Lerp(
                inicio,
                destino,
                Mathf.Clamp01(tempo / duracao)
            );

            yield return null;
        }

        carta.transform.SetParent(casaOrigem.transform, false);
        carta.transform.localPosition = Vector3.zero;

        carta.estado.SetCartaEstaAtacando(false);
    }
    public void MovimentaCartaParaCasa(CartaDaCena _cartaEntrando, Transform _casaTransfrom)
    {
        StartCoroutine(MovimentaGradualmente(_cartaEntrando, _casaTransfrom));
    }

    IEnumerator MovimentaGradualmente(CartaDaCena carta, Transform casa)
    {
        if (carta == null || casa == null)
        {
            yield return null;
        }

        Vector3 posicaoInicial = carta.transform.position;
        Vector3 posicaoFinal = casa.position;

        float duracao = 0.5f;
        float tempo = 0f;

        while (tempo < duracao)
        {
            if(carta == null)
            {
                break;
            }

            tempo += Time.deltaTime;

            float progresso = tempo / duracao;

            carta.transform.position = Vector3.Lerp(
                posicaoInicial,
                posicaoFinal,
                progresso
            );

            yield return null;
        }

        if (carta != null)
        {
            carta.transform.SetParent(casa, false);
            carta.transform.localPosition = Vector3.zero;

            carta.estado.SetMoveuSe(true);
            carta.estado.SetPodeAtacar(false);
        }
    }
    public void ChamaTroca(int _ID, CartaDaCena _vanguarda)
    {
        foreach (CartaDaCena _carta in bancoCards.geralCartaCenaLista)
        {
            if (_carta.dados.ID == _ID)
            {
                //trocaLugar.VerificaPosicaoDasCartas(_carta, _vanguarda);
            }
        }
    }
    public void SetEncostouEmOutraCarta(bool _encostou)
    {
        encostou = _encostou;
    }
    public bool GetEncostouEmOutraCarta()
    {
        return encostou;
    }
    public void ChamaCombate(CartaDaCena _ocupante, CartaDaCena _entrando)
    {
        sistemaCombate.UmContraUm(_ocupante.dados.ID, _entrando.dados.ID);
    }
    public void ChamaMapeamentoJogador(CartaDaCena _cartaJogador)
    {
        mapeamentoJogador.VerificaPossicaoAtualDaCartaDoJogador(_cartaJogador.dados.ID);
    }
    public void LiberaCasaQueEntrou(int _posicaoCasa)
    {
        liberaCasas.Liberar(_posicaoCasa);
    }
    public void SetVerificaSeSoltouCarta(bool _soltou)
    {
        soltou = _soltou;
    }
    public bool GetVerificaSeSoltouCarta()
    {
        return soltou;
    }
}
