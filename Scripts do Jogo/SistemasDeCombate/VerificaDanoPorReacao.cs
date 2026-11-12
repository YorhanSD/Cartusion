using UnityEngine;

public class VerificaDanoPorReacao : MonoBehaviour
{
    public SistemaCombate sistemaCombate;
    public CasaTemporaria casaTemporaria;
    ControleDeDano controleDeDano;

    void Start()
    {
        controleDeDano = GetComponent<ControleDeDano>();
        casaTemporaria = GetComponent<CasaTemporaria>();
        sistemaCombate = GetComponent<SistemaCombate>();
    }

    public bool AtaqueDaCartaPermiteDanoPorReacao(CartaDaCena _ataca)
    {
        foreach (Ataque ataque in _ataca.listaAtaque)
        {
            if(ataque.GetAtaqueAtivado() == true && ataque.imuneReacao == true)
            {
                Debug.Log($"A carta : {_ataca.dados.nome} com seu Ataque: {ataque.nome} possuí proteção contra a reação da carta adversária");
                return false;
            }
        }

        return false;
    }

    public bool DanoPorReacao(CartaDaCena _ataca, CartaDaCena _defende)
    {
        AtaqueDaCartaPermiteDanoPorReacao(_ataca);

        if(_ataca.dados.couracaAtual > 0)
        {
            return false;
        }

        if (_ataca.dados.vidaAtual > 0)
        {
            _ataca.dados.vidaAtual -= _defende.dados.reacao;

            controleDeDano.CriaPainelDano(_ataca.gameObject.transform, _defende.dados.reacao);

            controleDeDano.CriaPainelEfeitoDoAtaque(_ataca.gameObject.transform, "Reação");

            _ataca.uiCarta.AtualizarUI(_ataca.dados);

            sistemaCombate.VerificaMorte(_ataca, _defende);

            Debug.Log($"Atacante: {_ataca.dados.nome} sofre: {_defende.dados.reacao} de Dano de Reação da Carta: {_defende.dados.nome}");
        }

        return true;

        /*
        if (_ataca.dados.couracaAtual > 0)
        {
            _ataca.dados.couracaAtual -= 10;

            controleDeDano.CriaPainelDano(_ataca.gameObject.transform, 10);

            _ataca.uiCarta.AtualizarUI(_ataca.dados);

            //casaTemporaria.ConfereOResultadoDaBatalha(_defende, _ataca);
        }
        else
        {
            

            //casaTemporaria.ConfereOResultadoDaBatalha(_defende, _ataca);
            /*
            _ataca.uiCarta.AtualizarUI(_ataca.dados);

            if (_ataca.dados.vidaAtual <= 0)
            {
                _ataca.SetCartaMorreu(true);

                foreach (Casa casa in ia_MapeamentoDeCases.listaCase)
                {
                    if (casa.GetIDCartaOcupante() == _ataca.dados.ID)
                    {
                        casa.DesocuparCasa(_ataca);

                        casa.CartaEntra(_defende, casa.transform);

                        break;
                    }
                }

                // REMOVE defensor
                bancoCartas.geralCartaRuntimeLista.Remove(_ataca.dados);
                bancoCartas.geralCartaCenaLista.Remove(_ataca);

                Destroy(_ataca.gameObject);
           
        }
        */



    }

}
