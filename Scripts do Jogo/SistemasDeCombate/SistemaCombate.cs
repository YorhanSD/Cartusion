using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SistemaCombate : MonoBehaviour
{
    //MONOBEHAVIOUR NÃO PODE SER CRIADO COM NEW

    BancoCards bancoCartas;
    BancoDeAtaques bancoDeAtaques;

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

        bancoDeAtaques = GetComponent<BancoDeAtaques>();

        //controlePontos = FindObjectOfType<ControlePontos>();
    }

    public void UmContraUm(int IDDefensor, int IDAtacante)
    {
        CartaDaCena ataca = bancoCartas.geralCartaCenaLista.Find(c => c.dados.ID == IDAtacante);
        CartaDaCena defende = bancoCartas.geralCartaCenaLista.Find(c => c.dados.ID == IDDefensor);

        if (ataca == null || defende == null) return;

        if (controlaTurnos.turnoOponente == false && ataca.CompareTag("Carta Jogador") && defende.CompareTag("Carta Oponente"))
        {
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

            AplicaEfeitosNoDefensor(ataca, defende);
            AplicaEfeitosNoAtacante(ataca);
            VerificaMortePorBatalha(ataca, defende);
            ataca.estado.SetCartaEstaAtacando(false);
            defende.estado.SetCartaSendoAtacada(true);
        }
        else if (controlaTurnos.turnoOponente == true && ataca.CompareTag("Carta Oponente") && defende.CompareTag("Carta Jogador"))
        {;
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

            AplicaEfeitosNoDefensor(ataca, defende);
            AplicaEfeitosNoAtacante(ataca);
            VerificaMortePorBatalha(ataca, defende);
            ataca.estado.SetCartaEstaAtacando(false);
            defende.estado.SetCartaSendoAtacada(true);
        }
    }
    void AplicaEfeitosNoAtacante(CartaDaCena atacante)
    {
        foreach (Ataque ataque in atacante.listaAtaque)
        {
            if (!ataque.GetAtaqueAtivado())
                continue;

            AplicaEfeitoDoAtaque efeito = atacante.GetComponent<AplicaEfeitoDoAtaque>();

            if (ataque.avancar)
            {
                efeito.SetAvancar(true);
            }

            if (ataque.imuneReacao)
            {
                efeito.SetImuneReacao(true);
            }

            if (ataque.ignoraCouraca)
            {
                efeito.SetPerfuraCouraca(true);
            }

            break;
        }
    }
    void AplicaEfeitosNoDefensor(CartaDaCena atacante, CartaDaCena defensor)
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

            if (ataque.nocauteia && defensor.aplicaEfeitoDoAtaque.GetPodeSerNocauteada() == true)
            {
                efeito.SetNocauteada(true);

                defensor.estado.SetMoveuSe(true);
                defensor.estado.SetPodeAtacar(false);

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

    public void VerificaMortePorEfeito(CartaDaCena _carta)
    {
        if (_carta.dados.vidaAtual <= 0)
        {
            _carta.estado.SetCartaMorreu(true);

            Casa casaAtual = _carta.casaDaCarta.GetCasaAtual();

            casaAtual.CartaSai(_carta);

            bancoCartas.geralCartaRuntimeLista.Remove(_carta.dados);
            bancoCartas.geralCartaCenaLista.Remove(_carta);

            RemoveAtaquesDaLista(_carta);

            Destroy(_carta.gameObject);
        }
    }

    public void VerificaMortePorReacao(CartaDaCena _ataca)
    {
        Debug.Log("Chamou o método dano por reação");

        if (_ataca.dados.vidaAtual <= 0)
        {
            Debug.Log("Carta : " + _ataca.dados.nome + " Morreu por dano de reação");

            Debug.Log("ID da Carta que Morreu : " + _ataca.dados.ID);

            Debug.Log("Posicao da Casa onde ela Morreu : " + _ataca.casaDaCarta.GetCasaAtual().GetPosicaoCasa());

            _ataca.estado.SetCartaMorreu(true);

            _ataca.estado.SetEstaAtivada(true);

            Casa casaAtribuida = _ataca.casaDaCarta.GetCasaAtual();

            casaAtribuida.CartaSai(_ataca);


            bancoCartas.geralCartaRuntimeLista.Remove(_ataca.dados);
            bancoCartas.geralCartaCenaLista.Remove(_ataca);

            RemoveAtaquesDaLista(_ataca);


            Destroy(_ataca.gameObject);
        }
    }

    public void VerificaMortePorBatalha(CartaDaCena _ataca, CartaDaCena _defende)
    {
        if (_defende.dados.vidaAtual <= 0)
        {
            _defende.estado.SetCartaMorreu(true);

            casaTemporaria.ConfereOResultadoDaBatalha(_ataca, _defende);

            // REMOVE defensor

            bancoCartas.geralCartaRuntimeLista.Remove(_defende.dados);
            bancoCartas.geralCartaCenaLista.Remove(_defende);

            RemoveAtaquesDaLista(_defende);

            Destroy(_defende.gameObject); 
        }
        else
        {
            casaTemporaria.ConfereOResultadoDaBatalha(_ataca, _defende);
        }
    }

    public void RemoveAtaquesDaLista(CartaDaCena _cartaQueMorreu)
    {
        foreach(Ataque ataque in bancoDeAtaques.geralAtaqueLista)
        {
            if(ataque.id == _cartaQueMorreu.listaAtaque[0].id)
            {
                bancoDeAtaques.geralAtaqueLista.Remove(ataque);
            }

            if (ataque.id == _cartaQueMorreu.listaAtaque[1].id)
            {
                bancoDeAtaques.geralAtaqueLista.Remove(ataque);
            }
        }
    }
}

