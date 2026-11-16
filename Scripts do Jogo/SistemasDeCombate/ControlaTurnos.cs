using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlaTurnos : MonoBehaviour
{
    public bool turnoOponente;
    public int tempoNocaute;

    IA_Oponente iaOponente;
    SistemaCombate sistemaDeCombate;
    Cronometro cronometro;

    Baralho baralhoJogador;
    Baralho_Oponente baralhoOponente;

    IA_MapeamentoDeCases ia_MapeamentoDeCases;
    AplicaEfeitosDoTurnoSeguinte aplicaEfeitosDoTurnoSeguinte;

    public BancoCards bancoCartas;

    public GameObject telaTurnoPlayer;
    public GameObject telaturnoOponente;
    public GameObject botaoPassaTurno;

    public int numeroTurno;
    public TextMeshProUGUI textoTurno;

    public void Start()
    {
        numeroTurno = 1;

        //Para usar o GetComponent o Script deve estar no mesmo Objeto

        bancoCartas = GetComponent<BancoCards>();
        aplicaEfeitosDoTurnoSeguinte = GetComponent<AplicaEfeitosDoTurnoSeguinte>();
        sistemaDeCombate = GetComponent<SistemaCombate>();
        iaOponente = GetComponent<IA_Oponente>();
        cronometro = GetComponent<Cronometro>();
        baralhoJogador = GetComponent<Baralho>();
        baralhoOponente = GetComponent<Baralho_Oponente>();
        ia_MapeamentoDeCases = GetComponent<IA_MapeamentoDeCases>();

        StartCoroutine(GeraCartasDoJogador());
    }

    public void Update()
    {
        textoTurno.text = numeroTurno.ToString();
    }
    public void BotaoPassaOTurno()
    {
        telaturnoOponente.SetActive(true);

        botaoPassaTurno.SetActive(false);

        StartCoroutine(Esperas());

        sistemaDeCombate.travarJogador = true;
    }

    public IEnumerator Esperas()
    {
        TurnoOponente();

        yield return new WaitForSeconds(1f);


        ResetaCronometroOponente();

        telaturnoOponente.SetActive(false);

        if(numeroTurno == 2)
        {
            StartCoroutine(GeraCartasDoOponente());
        }

        if (numeroTurno >= 3)
        {
            //ProximaCartaOponente();
        }

        yield return new WaitForSeconds(3f);

        iaOponente.ControleDeAcoes();

        yield return new WaitForSeconds(3f);

        turnoOponente = false;

        //==========TURNO JOGADOR==========//

        TurnoJogador();

        TelaTurnoJogador(true);

        yield return new WaitForSeconds(1f);

        

        TelaTurnoJogador(false);

        ResetaCronometroJogador();

        //ProximaCartaJogador();
    }

    public void TurnoOponente() 
    {
        numeroTurno++;

        turnoOponente = true;

        ResetaUltimoIDCasas();

        baralhoOponente.casaReferenciaDeMaiorPosicao = 15;

        Cursor.visible = false;

        ResetaMovimentoDasCartas();

        TiraOsEfeitosDosAtaques();

        VerificaEfeitosDosAtaques();
    }
    
    public void TurnoJogador()
    {
        numeroTurno++;

        sistemaDeCombate.travarJogador = false;

        botaoPassaTurno.SetActive(true);

        Cursor.visible = true;

        ResetaMovimentoDasCartas();

        TiraOsEfeitosDosAtaques();

        VerificaEfeitosDosAtaques();
    }

    public void ResetaCronometroJogador()
    {
        cronometro.tempoJogador = 120;
        cronometro.ParaCronometro_Oponente();
        cronometro.IniciaCronometro_Jogador();
    }

    public void ResetaCronometroOponente()
    {
        cronometro.tempoOponente = 120;
        cronometro.ParaCronometro_Jogador();
        cronometro.IniciaCronometro_Oponente();
    }

    public void TelaTurnoJogador(bool _comando)
    {
        telaTurnoPlayer.SetActive(_comando);
    }
    public IEnumerator GeraCartasDoJogador()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            baralhoJogador.NumeroAleatorio();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator GeraCartasDoOponente()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);

            baralhoOponente.ProximaCartaAleatoriaOponente();

            yield return new WaitForSeconds(0.5f);

            baralhoOponente.casaReferenciaDeMaiorPosicao--;
        }
    }

    public void VerificaEfeitosDosAtaques()
    {
        aplicaEfeitosDoTurnoSeguinte.AplicaNocaute();
        aplicaEfeitosDoTurnoSeguinte.AplicaVirose();
        aplicaEfeitosDoTurnoSeguinte.AplicaQueimadura();
        aplicaEfeitosDoTurnoSeguinte.AplicaSangramento();
    }
    public void TiraOsEfeitosDosAtaques()
    {
        foreach (CartaDaCena cartaCena in bancoCartas.geralCartaCenaLista)
        {
            if(cartaCena.aplicaEfeitoDoAtaque.GetNocauteada() == true)
            {
                cartaCena.aplicaEfeitoDoAtaque.RemoveTempoNocauteada();

                break;
            }

            if (cartaCena.aplicaEfeitoDoAtaque.GetRepelido() == true)
            {
                cartaCena.aplicaEfeitoDoAtaque.SetRepelido(false);

                break;
            }
        }
    }
    public void ResetaMovimentoDasCartas()
    {
        foreach(CartaDaCena cartaCena in bancoCartas.geralCartaCenaLista)
        {
            if (cartaCena != null && cartaCena.aplicaEfeitoDoAtaque.GetNocauteada() == false) 
            {
                cartaCena.estado.SetMoveuSe(false);
                cartaCena.estado.SetPodeAtacar(true);
            }
        }
    }
    public void ResetaUltimoIDCasas()
    {
        foreach (Casa casaB in ia_MapeamentoDeCases.listaCase)
        {
            if (casaB.GetUltimoID() != -1)
            {
                casaB.SetUltimoID(-1);
            }
        }
    }
}
