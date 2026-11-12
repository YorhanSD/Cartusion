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

    //private Casa casaPassadaDefensor;
    private Casa casaPassadaAtacante;
    private Casa casaAtualDefensor;
    private Casa casaAtualAtacante;

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
    public void RecebeCasaPassadaAtacante(int _casaPosicao)
    {
        /*
        foreach (Casa casa in ia_MapeamentoDeCases.listaCase)
        {
            if (casa.GetPosicaoCasa() == _casaPosicao)
            {
                casaPassadaAtacante = casa;

                Debug.Log($"Casa passada do atacante : {casaPassadaAtacante.GetPosicaoCasa()}");
            }
        }
        */
    }
    /*
    public Casa GetCasaPassadaAtacante()
    {
        //return casaPassadaAtacante;
    }
    */
    public void RecebeCasasAtuais(int _idCartaQueDefende, int _idCartaQueAtaca)
    {
        CartaDaCena cartaDefende = bancoCards.geralCartaCenaLista
     .Find(c => c.dados.ID == _idCartaQueDefende);

        CartaDaCena cartaAtaca = bancoCards.geralCartaCenaLista
     .Find(c => c.dados.ID == _idCartaQueAtaca);

        casaTemporaria.CasaDoDefensor(cartaDefende.casaDaCarta.GetCasaAtual());
        casaTemporaria.CasaDoAtacante(cartaAtaca.casaDaCarta.GetCasaAtual());

        /*
        foreach (Casa casa in ia_MapeamentoDeCases.listaCase)
        {
            if (casa.GetIDCartaOcupante() == _idCartaQueDefende)
            {
                //casaAtualDefensor = casa;
                casaTemporaria.CasaDoDefensor(cartaDefende.casaDaCarta.GetCasaAtual());

                Debug.Log($"Casa onde está o defensor : {casa.GetPosicaoCasa()}");
            }

            if (casa.GetIDCartaOcupante() == _idCartaQueAtaca)
            {
                //casaAtualAtacante = casa;
                casaTemporaria.CasaDoAtacante(cartaAtaca.casaDaCarta.GetCasaAtual());
                Debug.Log($"Casa onde está o atacante : {casa.GetPosicaoCasa()}");
            }
        }
        */

        ChamaCombate(cartaDefende, cartaAtaca);

        Debug.Log($"Carta : {cartaAtaca.dados.nome} ataca {cartaDefende.dados.nome}");
    }
    public void RecebeCasaAtacante(int _idCartaAtacante, CartaDaCena _cartaEntrando)
    {
        /*
        CartaDaCena carta = bancoCards.geralCartaCenaLista
     .Find(c => c.dados.ID == _idCartaAtacante);

        Debug.Log($"Procurando atacante {_idCartaAtacante}");

        foreach (Casa casa in ia_MapeamentoDeCases.listaCase)
        {
            Debug.Log($"Casa {casa.GetPosicaoCasa()} possui ID {casa.GetIDCartaOcupante()}");

            if (casa.GetIDCartaOcupante() == _idCartaAtacante)
            {
                Debug.Log($"Casa onde está o atacante : {casa.GetPosicaoCasa()}");

                casaTemporaria.CasaDoAtacante(casa);
                ChamaCombate(carta, _cartaEntrando);
            }
        }
        */
    }
    /*
    public void MovimentaCartaParaCasa(CartaDaCena _cartaEntrando, Transform _casaTransfrom)
    {
        _cartaEntrando.transform.SetParent(_casaTransfrom, false);
        _cartaEntrando.transform.localPosition = Vector3.zero;
        _cartaEntrando.SetMoveuSe(true);
        _cartaEntrando.SetPodeAtacar(false);
    }
    */
    public void MovimentaCartaParaCasa(CartaDaCena _cartaEntrando, Transform _casaTransfrom)
    {
        StartCoroutine(MovimentaGradualmente(_cartaEntrando, _casaTransfrom));
    }

    IEnumerator MovimentaGradualmente(CartaDaCena carta, Transform casa)
    {
        Vector3 posicaoInicial = carta.transform.position;
        Vector3 posicaoFinal = casa.position;

        float duracao = 0.5f;
        float tempo = 0f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;

            float progresso = tempo / duracao;

            carta.transform.position = Vector3.Lerp(
                posicaoInicial,
                posicaoFinal,
                progresso
            );

            yield return null;
        }

        carta.transform.SetParent(casa, false);
        carta.transform.localPosition = Vector3.zero;

        carta.SetMoveuSe(true);
        carta.SetPodeAtacar(false);
    }
    public void ProcuraCartaDentroDaCasa(int _ID, CartaDaCena _cartaEntrando)
    {
        /*
        CartaDaCena carta = bancoCards.geralCartaCenaLista
     .Find(c => c.dados.ID == _ID);

        if (carta != null)
        {
            //ChamaCombate(carta, _cartaEntrando);
        }
        */
    }
    public void ChamaTroca(int _ID, CartaDaCena _vanguarda)
    {
        foreach (CartaDaCena _carta in bancoCards.geralCartaCenaLista)
        {
            if (_carta.dados.ID == _ID)
            {
                trocaLugar.VerificaPosicaoDasCartas(_carta, _vanguarda);
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
