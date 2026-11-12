using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SistemaCombate : MonoBehaviour
{
    //MONOBEHAVIOUR NÃO PODE SER CRIADO COM NEW

    BancoCards bancoCartas;

    public EntraESaiCartas entraESaiCartas;

    [SerializeField] private ControlaTurnos controlaTurnos;

    IA_MapeamentoDeCases ia_MapeamentoDeCases;

    public ControlePontos controlePontos;

    public bool travarJogador = false;

    public CasaTemporaria casaTemporaria;

    public ControleDeDano controleDeDano;

    public VerificaDanoPorReacao verificaDanoPorReacao;

    public VerificaCouraca verificaCouraca;

    
    public void Start()
    {
        verificaCouraca = GetComponent<VerificaCouraca>();

        verificaDanoPorReacao = GetComponent<VerificaDanoPorReacao>();

        controleDeDano = GetComponent<ControleDeDano>();

        casaTemporaria = GetComponent<CasaTemporaria>();

        entraESaiCartas = GetComponent<EntraESaiCartas>();

        bancoCartas = GetComponent<BancoCards>();

        controlaTurnos = GetComponent<ControlaTurnos>();

        ia_MapeamentoDeCases = GetComponent<IA_MapeamentoDeCases>();

        //controlePontos = FindObjectOfType<ControlePontos>();
    }

    public void UmContraUm(int IDDefensor, int IDAtacante)
    {
        CartaDaCena ataca = bancoCartas.geralCartaCenaLista.Find(c => c.dados.ID == IDAtacante);
        CartaDaCena defende = bancoCartas.geralCartaCenaLista.Find(c => c.dados.ID == IDDefensor);

        if (ataca == null || defende == null) return;

        if (controlaTurnos.turnoOponente == false && ataca.CompareTag("Carta Jogador") && defende.CompareTag("Carta Oponente"))
        {
            //ChamaGravaUltimoID(ataca);
            verificaCouraca.AplicaDanoACouraca(ataca, defende);
            verificaDanoPorReacao.DanoPorReacao(ataca, defende);
            defende.PrintaDados(defende.dados);

            if (defende.uiCarta != null)
            {
                defende.uiCarta.AtualizarUI(defende.dados);
            }
            else
            {
                Debug.LogError($"Carta {defende.printNome} está sem UI ligada!");
            }

            AplicaEfeitos(ataca, defende);
            VerificaMorte(ataca, defende);
            ataca.SetCartaEstaAtacando(false);
        }
        else if (controlaTurnos.turnoOponente == true && ataca.CompareTag("Carta Oponente") && defende.CompareTag("Carta Jogador"))
        {
            //ChamaGravaUltimoID(ataca);
            verificaCouraca.AplicaDanoACouraca(ataca, defende);
            verificaDanoPorReacao.DanoPorReacao(ataca, defende);
            defende.PrintaDados(defende.dados);

            if (defende.uiCarta != null)
            {
                defende.uiCarta.AtualizarUI(defende.dados);
            }
            else
            {
                Debug.LogError($"Carta {defende.printNome} está sem UI ligada!");
            }

            AplicaEfeitos(ataca, defende);
            VerificaMorte(ataca, defende);
            ataca.SetCartaEstaAtacando(false);
        }
    }
    void AplicaEfeitos(CartaDaCena atacante, CartaDaCena defensor)
    {

        foreach (Ataque ataque in atacante.listaAtaque)
        {
            if (!ataque.GetAtaqueAtivado())
                continue;

            AplicaEfeitoDoAtaque efeito =
                defensor.GetComponent<AplicaEfeitoDoAtaque>();

            if (ataque.virose)
            {
                efeito.SetDoente(true);

                Debug.Log($"{atacante.dados.nome} deixou {defensor.dados.nome} doente");

                controleDeDano.CriaPainelEfeitoDoAtaque(defensor.gameObject.transform,"Doente");
            }

            if (ataque.nocauteia)
            {
                efeito.SetNocauteada(true);

                Debug.Log($"{atacante.dados.nome} nocauteou {defensor.dados.nome}");

                controleDeDano.CriaPainelEfeitoDoAtaque(defensor.gameObject.transform, "Nocauteada");
            }

            if (ataque.queimadura)
            {
                efeito.SetQueimando(true);

                Debug.Log($"{atacante.dados.nome} queimou {defensor.dados.nome}");

                controleDeDano.CriaPainelEfeitoDoAtaque(defensor.gameObject.transform, "Queimada");
            }

            if (ataque.sangramento)
            {
                efeito.SetSangrando(true);

                Debug.Log($"{atacante.dados.nome} fez {defensor.dados.nome} sangrar");

                controleDeDano.CriaPainelEfeitoDoAtaque(defensor.gameObject.transform, "Sangrando");
            }

            if (ataque.repulsao)
            {
                efeito.SetRepelido(true);

                Debug.Log($"{atacante.dados.nome} repeliu {defensor.dados.nome}");

                controleDeDano.CriaPainelEfeitoDoAtaque(defensor.gameObject.transform, "Repelida");
            }

            break;
        }
    }
    void ChamaGravaUltimoID(CartaDaCena _ataca)
    {
        foreach (Casa casa in ia_MapeamentoDeCases.listaCase)
        {
            if (casa.GetIDCartaOcupante() == _ataca.dados.ID)
            {
                casa.GravaUltimoID(_ataca.dados.ID); //Grava o id da carta atacante na sua própria casa
            }
        }
    }

    public void VerificaMortePorEfeito(CartaDaCena _carta)
    {
        if (_carta.dados.vidaAtual <= 0)
        {
            _carta.SetCartaMorreu(true);

            bancoCartas.geralCartaRuntimeLista.Remove(_carta.dados);
            bancoCartas.geralCartaCenaLista.Remove(_carta);

            Destroy(_carta.gameObject);
        }
    }

    public void VerificaMorte(CartaDaCena _ataca, CartaDaCena _defende)
    {
        if (_defende.dados.vidaAtual <= 0)
        {
            _defende.SetCartaMorreu(true);

            //MoveAteCasaQueDestruiuCarta(_ataca,_defende);

            casaTemporaria.ConfereOResultadoDaBatalha(_ataca, _defende);

            // REMOVE defensor
            bancoCartas.geralCartaRuntimeLista.Remove(_defende.dados);
            bancoCartas.geralCartaCenaLista.Remove(_defende);

            Destroy(_defende.gameObject); 
        }
        else
        {
            casaTemporaria.ConfereOResultadoDaBatalha(_ataca, _defende);
        }
    }
}

