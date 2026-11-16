using UnityEngine;

public class AtaquesDeMovimento : MonoBehaviour
{
    IA_MapeamentoDeCases ia_MapeamentoDeCases;

    private void Start()
    {
        ia_MapeamentoDeCases = GetComponent<IA_MapeamentoDeCases>();
    }
    public bool VerificaAvancar(CartaDaCena _ataca, CartaDaCena _defende)
    {
        if (_ataca.estado.GetCartaEstaAtacando() == true && _ataca.aplicaEfeitoDoAtaque.GetAvancar() == true && _defende.casaDaCarta.GetCasaAntiga().GetIDCartaOcupante() == -1)
        {
            Casa casaAtualAtacante = _ataca.casaDaCarta.GetCasaAtual();

            casaAtualAtacante.CartaSai(_ataca);

            AtivarLockdown(casaAtualAtacante.GetPosicaoCasa());

            //Debug.Log($"A carta do ataque : {_ataca.dados.nome} com o id {_ataca.dados.ID}");
            //Debug.Log($"Deixou a casa : {casaAtualAtacante.GetPosicaoCasa()}");

            _ataca.estado.SetCartaEstaAvancando(true);

            //Debug.Log($"A carta : {_ataca.dados.nome} sai de sua casa para avançar");
            //Debug.Log($"A carta : {_ataca.dados.nome} que acabou de avançar, tem o ID : {_ataca.dados.ID}");

            Casa casaRetorno = _defende.casaDaCarta.GetCasaAntiga();

            //Debug.Log($"Posicao da casa antiga da carta de defesa : {_defende.dados.nome}, {casaRetorno.GetPosicaoCasa()}");

            if (casaRetorno == null)
            {
                Debug.LogError("Carta não possui casa antiga.");

                return false;
            }

            casaRetorno.CartaEntra(_ataca, casaRetorno.transform);

            Casa casaAtualDefensor = _defende.casaDaCarta.GetCasaAtual();

            casaAtualDefensor.CartaEntra(_defende, casaAtualDefensor.transform);

            //Debug.Log($"A carta na defesa: {_defende.dados.nome} com o id {_defende.dados.ID}");
            //Debug.Log($"Entrou na casa : {casaAtualDefensor.GetPosicaoCasa()}");

            //Debug.Log($"A carta : {_defende.dados.nome} foi ultrapassada pela carta " + _ataca.dados.nome);
            //Debug.Log($"A carta que ultrapassou {_defende.dados.nome}, {_ataca.dados.nome} possui o ID : " + _ataca.dados.ID);
        }

        return false;
    }
    public void VerificarRepelir(CartaDaCena _ataca, CartaDaCena _defende)
    {
        if (_ataca.estado.GetCartaEstaAtacando() == true && _defende.aplicaEfeitoDoAtaque.GetRepelido() == true && _defende.casaDaCarta.GetCasaAntiga().GetIDCartaOcupante() == -1)
        {
            Casa casaAtual = _defende.casaDaCarta.GetCasaAtual();

            Casa casaRetorno = _defende.casaDaCarta.GetCasaAntiga();

            if (casaRetorno == null)
            {
                Debug.LogError("Carta não possui casa antiga.");
                return;
            }

            casaAtual.CartaSai(_defende); // CARTA QUE LEVA O GOLPE DEVE SAIR DA SUA CASA ATUAL PRIMEIRO

            casaRetorno.CartaEntra(_defende, casaRetorno.transform); // DEPOIS ELA DEVE ENTRAR NA CASA DE RETORNO

            Debug.Log($"A carta : {_defende.dados.nome} foi repelida até sua casa antiga !");
        }
    }

    public void AtivarLockdown(int _casaQueNaoSeraFechada)
    {
        foreach (Casa casa in ia_MapeamentoDeCases.listaCase)
        {
            if (casa.GetPosicaoCasa() != _casaQueNaoSeraFechada)
            {
                casa.Lockdown();
                Debug.Log($"Casa {casa.GetPosicaoCasa()} está com a saída Bloqueada !");
            }
        }
    }
}
