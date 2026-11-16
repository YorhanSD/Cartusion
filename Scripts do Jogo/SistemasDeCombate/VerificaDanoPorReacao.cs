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

    public bool AtaqueDaCartaPermiteDanoPorReacao(CartaDaCena _cartaAtacante)
    {
        foreach (Ataque ataque in _cartaAtacante.listaAtaque)
        {
            if(ataque.GetAtaqueAtivado() == true && _cartaAtacante.aplicaEfeitoDoAtaque.GetImuneReacao() == true)
            {
                Debug.Log($"A carta : {_cartaAtacante.dados.nome} com seu Ataque: {ataque.nome} possuí proteção contra a reação da carta adversária");
                return false;
            }
        }

        return false;
    }

    public bool DanoPorReacao(CartaDaCena _ataca, CartaDaCena _defende)
    {
        AtaqueDaCartaPermiteDanoPorReacao(_ataca);

        if (_ataca.dados.vidaAtual > 0 && _ataca.dados.couracaAtual <= 0)
        {
            _ataca.dados.vidaAtual -= _defende.dados.reacao;

            controleDeDano.CriaPainelDano(_ataca.gameObject.transform, _defende.dados.reacao);

            controleDeDano.CriaPainelEfeitoDoAtaque(_ataca.gameObject.transform, "Reação");

            if (_ataca.dados.vidaAtual <= 0 && _ataca != null)
            {
                sistemaCombate.VerificaMortePorReacao(_ataca);
            }

            _ataca.uiCarta.AtualizarUI(_ataca.dados);

            Debug.Log($"Atacante: {_ataca.dados.nome} sofre: {_defende.dados.reacao} de Dano de Reação da Carta: {_defende.dados.nome}");
        }
        return true;
    }
}
