using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Casa : MonoBehaviour
{
    [SerializeField] EntraESaiCartas entraESaiCartas;

    private Coroutine coroutineEntrada;

    IA_MapeamentoDeCases ia_MapeamentoDeCases;

    [SerializeField] private bool cartaSobreCasa;
    [SerializeField] private bool estaBloqueado = false;
    [SerializeField] private bool caseOcupadoPeloJogador = false;
    [SerializeField] private bool caseOcupadoPeloOponente = false;
    [SerializeField] private string nomeCartaDoJogador;
    [SerializeField] private string nomeCartaDoOponente;
    [SerializeField] private int idCartaOcupante = -1;
    [SerializeField] private int posicaoCasa = 0;
    [SerializeField] private int ultimoID;
    [SerializeField] private bool alfandega = false;


    [Obsolete]
    public void Start()
    {
        ia_MapeamentoDeCases = FindObjectOfType<IA_MapeamentoDeCases>();
        entraESaiCartas = FindObjectOfType<EntraESaiCartas>();
    }
    public void SetBloquearSaida(bool _alfandega)
    {
        alfandega = _alfandega;
    }
    public bool GetBloquearSaida()
    {
        return alfandega;
    }
    public void SetUltimoID(int _ID)
    {
        ultimoID = _ID;
    }
    public int GetUltimoID()
    {
        return ultimoID;
    }
    public void SetIDCartaOcupante(int _cartaID)
    {
        idCartaOcupante = _cartaID;
    }
    public int GetIDCartaOcupante()
    {
        return idCartaOcupante;
    }
    public void SetEstaBloqueado(bool _estaBloqueado)
    {
        estaBloqueado = _estaBloqueado;
    }
    public bool GetEstaBloqueado()
    {
        return estaBloqueado;
    }
    public void SetCasaPosicao(int _possicaoCasa)
    {
        posicaoCasa = _possicaoCasa;
    }
    public int GetPosicaoCasa()
    {
        return posicaoCasa;
    }
    public void SetCaseOcupadoOponente(bool _caseOcupado)
    {
        caseOcupadoPeloOponente = _caseOcupado;
    }
    public bool GetCaseOcupadoOponente()
    {
        return caseOcupadoPeloOponente;
    }
    public void SetCaseOcupadoJogador(bool _caseOcupado)
    {
        caseOcupadoPeloJogador = _caseOcupado;
    }
    public bool GetCaseOcupadoJogador()
    {
        return caseOcupadoPeloJogador;
    }

    [Obsolete]
    public void OnTriggerEnter2D(Collider2D carta)
    {
        if (GetEstaBloqueado() == true)
        {
            Debug.Log("Você não pode entrar em uma casa bloqueada !");
            return;
        }

        if (!carta.CompareTag("Carta Jogador") && !carta.CompareTag("Carta Oponente"))
        {
            return;
        }

        if (carta.CompareTag("Carta Jogador") || carta.CompareTag("Carta Oponente"))
        {
            CartaDaCena _carta = carta.GetComponent<CartaDaCena>();

            cartaSobreCasa = true;

            CartaEstaSobreACasa(_carta);
        }
    }
    public void OnTriggerExit2D(Collider2D carta)
    {
        if (carta.CompareTag("Carta Jogador") || carta.CompareTag("Carta Oponente"))
        {
            CartaDaCena _carta = carta.GetComponent<CartaDaCena>();

            //Debug.Log(
                //$"OnTriggerExit2D | Casa: {GetPosicaoCasa()} | " +
                //$"Carta: {_carta.dados.nome} | " +
                //$"ID: {_carta.dados.ID} | " +
                //$"Atacando: {_carta.estado.GetCartaEstaAtacando()} | " +
                //$"Sendo atacada: {_carta.estado.GetCartaEstaSendoAtacada()} | " +
                //$"Avançando: {_carta.estado.GetCartaEstaAvancando()}"
            //);

            if (_carta.estado.GetCartaEstaSendoAtacada() == true)
                return;

            if (_carta.estado.GetCartaEstaAtacando() == true)
                return;

            CartaSai(_carta);
        }
    }

    public void CartaEstaSobreACasa(CartaDaCena _cartaEntrando)
    {
        if (_cartaEntrando.CompareTag("Carta Oponente"))
        {
            if (GetIDCartaOcupante() != -1 && GetCaseOcupadoJogador() == true)//Quando uma carta do oponente entra na casa e já tem uma carta do jogador então:
            {
                _cartaEntrando.estado.SetCartaEstaAtacando(true);

                entraESaiCartas.RecebeCasasAtuais(this.GetIDCartaOcupante(), _cartaEntrando.dados.ID);
            }

            if (GetCaseOcupadoJogador() == false && GetCaseOcupadoOponente() == false)
            {
                CartaEntra(_cartaEntrando, this.transform);
            }

        }
        else if (_cartaEntrando.CompareTag("Carta Jogador"))
        {
            if (GetIDCartaOcupante() != -1 && GetCaseOcupadoOponente() == true)//Quando uma carta do jogador entra na casa e já tem uma carta do oponente então:
            {
                _cartaEntrando.estado.SetCartaEstaAtacando(true);

                entraESaiCartas.RecebeCasasAtuais(this.GetIDCartaOcupante(), _cartaEntrando.dados.ID);
            }

            if (GetCaseOcupadoJogador()) //Troca
            {
                //entraESaiCartas.ChamaTroca(this.GetIDCartaOcupante(), _cartaEntrando);
            }

            if (GetEstaBloqueado() == false && GetCaseOcupadoOponente() == false && GetCaseOcupadoJogador() == false)
            {
                if (coroutineEntrada == null)
                {
                    coroutineEntrada = StartCoroutine(CartaEspera(_cartaEntrando));
                }
            }
        }
    }
    public IEnumerator CartaEspera(CartaDaCena _cartaEntrando)
    {
        while (!entraESaiCartas.GetVerificaSeSoltouCarta())
        {
            if (!cartaSobreCasa)
            {
                coroutineEntrada = null;
                yield break;
            }

            yield return null;
        }

        if (_cartaEntrando == null)
        {
            coroutineEntrada = null;
            yield break;
        }

        CartaEntra(_cartaEntrando, this.transform);

        coroutineEntrada = null;
    }

    public void CartaEntra(CartaDaCena _cartaEntrando, Transform _casaTransfrom)
    {
        _cartaEntrando.estado.SetEstaAtivada(true); // CARTA ENTROU, ENTÃO ESTÁ ATIVA
        _cartaEntrando.estado.SetCartaEstaAvancando(false); //CARTA CHEGOU AO DESTINO, NÃO ESTÁ MAIS AVANÇANDO

        CasaDaCarta info = _cartaEntrando.casaDaCarta;

        info.SetCasaAntiga(info.GetCasaAtual());
        info.SetCasaAtual(this);

        if (_cartaEntrando.gameObject.CompareTag("Carta Jogador"))
        {
            entraESaiCartas.LiberaCasaQueEntrou(GetPosicaoCasa());

            SetCaseOcupadoJogador(true);
        }

        if (_cartaEntrando.gameObject.CompareTag("Carta Oponente"))
        {
            SetCaseOcupadoOponente(true);
        }

        //Debug.Log(
    //$"CartaEntra | Carta: {_cartaEntrando.dados.nome} | " +
            //$"ID: {_cartaEntrando.dados.ID} | " +
            //$"Casa destino: {GetPosicaoCasa()} | " +
            //$"Ocupante anterior: {GetIDCartaOcupante()}"
        //);

        SetIDCartaOcupante(_cartaEntrando.dados.ID);

        entraESaiCartas.MovimentaCartaParaCasa(_cartaEntrando, _casaTransfrom);
    }
    public void Lockdown()
    {
        SetBloquearSaida(true);

        StartCoroutine(bloqueioTemporario());
    }
    IEnumerator bloqueioTemporario()
    {
        yield return new WaitForSeconds(1f);

        SetBloquearSaida(false);
    }
    public void CartaSai(CartaDaCena _cartaSaindo)
    {
        if (GetBloquearSaida() == false)
        {
            if (_cartaSaindo == null)
                return;

            //Debug.Log(
            //$"CartaSai | Casa: {GetPosicaoCasa()} | " +
            // $"Carta: {_cartaSaindo.dados.nome} | " +
            // $"ID da carta: {_cartaSaindo.dados.ID} | " +
            // $"ID ocupante: {GetIDCartaOcupante()}"
            // );

            if (_cartaSaindo.estado.GetCartaEstaSendoAtacada() == true)
                return;

            if (GetIDCartaOcupante() != _cartaSaindo.dados.ID)
                return;

            DesocuparCasa(_cartaSaindo); //SE A CARTA SAI, ELA DEIXA DE SER ATIVA
        }
    }

    public void DesocuparCasa(CartaDaCena _cartaDestruida)
    {
        _cartaDestruida.estado.SetEstaAtivada(false);

        if (_cartaDestruida.gameObject.CompareTag("Carta Jogador"))
        {
            SetCaseOcupadoJogador(false);
        }

        if (_cartaDestruida.gameObject.CompareTag("Carta Oponente"))
        {
            SetCaseOcupadoOponente(false);
        }

        cartaSobreCasa = false;

        SetIDCartaOcupante(-1);

        Debug.Log("CARTA : " + _cartaDestruida.dados.nome);
        Debug.Log("ID DA CARTA : " + _cartaDestruida.dados.ID);
        Debug.Log("DESOCUPOU A CASA : " + GetPosicaoCasa());
    }
}
