using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Baralho : MonoBehaviour
{
    //LISTAS EXCLUSIVAS PARA CARTAS CLONES
    public List<CartaDaCena> deckJogador = new List<CartaDaCena>();

    [SerializeField] private List<CartaDaCena> cenaTemp = new List<CartaDaCena>();
    [SerializeField] private List<CartaDaCena> bancoDeCartasSelecionadas = new List<CartaDaCena>();
    [SerializeField] private List<CartaOriginal> dadosTemp = new List<CartaOriginal>();


    [SerializeField] UICard uiPrefab;
    [SerializeField] Transform uiParent;

    BancoCards bancoCartas;
    BancoDeAtaques bancoDeAtaques;

    public Canvas canvas;

    SalvaJogoPC salvaJogoPC;

    public int numeroAleatorio;
    public Transform baralhoTransform;

    public void Awake()
    {
        salvaJogoPC = GetComponent<SalvaJogoPC>();
        bancoCartas = GetComponent<BancoCards>();
        bancoDeAtaques = GetComponent<BancoDeAtaques>();
        FiltraCartas();
    }

    public void FiltraCartas()
    {
        SalvaEscolhaPersonagem salvaEscolhaPersonagem = salvaJogoPC.PersonagemSalvo();
        //Debug.Log($"Personagem escolhido : {salvaEscolhaPersonagem.GetNomePersonagemEscolhido()}");

        for (int i = 0; i < 12; i++)
        {
            if (cenaTemp[i].dados.especieSelecionada == salvaEscolhaPersonagem.GetEspecieDominante() || cenaTemp[i].dados.especieSelecionada == salvaEscolhaPersonagem.GetEspecieRecessiva())
            {
                //Debug.Log($"Especie da Carta : {cenaTemp[i].dados.especieSelecionada}");
                //Debug.Log($"Especie Dominante : {salvaEscolhaPersonagem.GetEspecieDominante()} e Especie Recessiva : {salvaEscolhaPersonagem.GetEspecieRecessiva()}");

                bancoDeCartasSelecionadas.Add(cenaTemp[i]);
               
            }
        }
    }
    public void NumeroAleatorio()
    {
        numeroAleatorio = UnityEngine.Random.Range(0, 6);

        CriaDuplicata();
    }
    public void CriaDuplicata()
    {
        CartaDaCena cartaClone = Instantiate(
            bancoDeCartasSelecionadas[numeroAleatorio],
           baralhoTransform,
           false

       );

        cartaClone.tag = "Carta Jogador";

        CartaRuntime cartaRuntime = new CartaRuntime();
        cartaRuntime.cartaOriginal = bancoDeCartasSelecionadas[numeroAleatorio].cartaBase; ; 
        cartaRuntime.Inicializar(bancoCartas.contaID);

        cartaClone.dados = cartaRuntime;

        cartaClone.GravaUI(cartaRuntime);
        cartaClone.PrintaDados(cartaRuntime);

        bancoCartas.geralCartaCenaLista.Add(cartaClone);
        bancoCartas.geralCartaRuntimeLista.Add(cartaRuntime);

        for (int i = 0; i < 2; i++)
        {
            bancoDeAtaques.contaAtaqueID++;

            cartaClone.listaAtaque[i].id = bancoDeAtaques.contaAtaqueID;

            bancoDeAtaques.geralAtaqueLista.Add(cartaClone.listaAtaque[i]);
            //Debug.Log("Ataque adicionado com sucesso !");
            //Debug.Log("ID do ataque : " + cartaClone.listaAtaque[i].id);
            //Debug.Log("Nome do ataque : " + cartaClone.listaAtaque[i].nome);
        }

        deckJogador.Add(cartaClone);

        cartaClone.transform.localPosition = Vector3.zero;

        bancoCartas.contaID++;
    }
}