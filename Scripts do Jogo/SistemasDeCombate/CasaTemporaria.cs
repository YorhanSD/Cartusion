using UnityEngine;

public class CasaTemporaria : MonoBehaviour
{
    private Casa printCasaAtacante;
    private Casa printCasaDefensor;

    public IA_MapeamentoDeCases ia_MapeamentoDeCases;
    public EntraESaiCartas entraESaiCartas;
    public AtaquesDeMovimento ataquesDeMovimento;

    void Start()
    {
        entraESaiCartas = GetComponent<EntraESaiCartas>();
        ia_MapeamentoDeCases = GetComponent<IA_MapeamentoDeCases>();
        ataquesDeMovimento = GetComponent<AtaquesDeMovimento>();
    }
    public void CasaDoDefensor(Casa _casaDefensor)
    {
        if(_casaDefensor == null)
            return;

        printCasaDefensor = _casaDefensor;

        Debug.Log($"printCasaDefensor recebeu a posição : {printCasaDefensor.GetPosicaoCasa()}");
    }

    public Casa GetCasaDefensor()
    {
        return printCasaDefensor;
    }

    public void CasaDoAtacante(Casa _casaAtacante)
    {
        if (_casaAtacante == null)
            return;

        printCasaAtacante = _casaAtacante;

        Debug.Log($"printCasaAtacante recebeu a posição : {printCasaAtacante.GetPosicaoCasa()}");
    }

    public Casa GetCasaAtacante()
    {
        return printCasaAtacante;
    }
 
    public void ConfereOResultadoDaBatalha(CartaDaCena _ataca, CartaDaCena _defende)
    {
        if (_ataca == null && _defende == null)
            return;

        if (_defende.dados.vidaAtual > 0)
        {
            DefensorContinuaNaCasa(_ataca, _defende);

            Debug.Log($"{_ataca.dados.nome} não conseguiu conquistar a casa de {_defende.dados.nome}");

            ataquesDeMovimento.VerificaAvancar(_ataca, _defende);

            ataquesDeMovimento.VerificarRepelir(_ataca, _defende);
        }
        else
        {
            AtacanteEntrouNaCasa(_ataca, _defende);

            Debug.Log($"{_ataca.dados.nome} conseguiu conquistar a casa de {_defende.dados.nome}");
        }
    }
    public void DefensorContinuaNaCasa(CartaDaCena _ataca, CartaDaCena _defende)
    {
        Retorno(_ataca);

        Debug.Log($"Então, {_defende.dados.nome} continua na casa : {printCasaDefensor.GetPosicaoCasa()}");
    }
    public void AtacanteEntrouNaCasa(CartaDaCena _ataca, CartaDaCena _defende)
    {
        DefensorMorreu(_defende);

        Debug.Log($"{_defende.dados.nome} foi derrotado em batalha");

        MoveParaCasaQueDestruiuCarta(_ataca);
    }
    public void DefensorMorreu(CartaDaCena _defende)
    {
        GetCasaDefensor().CartaSai(_defende);

        Debug.Log($"Carta Defensor : {_defende.dados.nome} Morreu");
    }
    
    public void Retorno(CartaDaCena _ataca)
    {
        Casa casaRetorno = _ataca.casaDaCarta.GetCasaAtual(); //CASA DE RETORNO DO ATAQUE

        if (casaRetorno == null)
        {
            Debug.LogError("Carta não possui casa atual.");
            return;
        }

        if (_ataca == null)
        {
            Debug.Log("A carta de retorno é nula !");
            return;
        }

        if (GetCasaDefensor() == null)
        {
            Debug.Log("A casa do defensor é nula !");
            return;
        }

        if (GetCasaAtacante() == null)
        {
            Debug.Log("A casa do atacante é nula !");
            return;
        }

        if(_ataca.estado.GetCartaMorreu() == true)
        {
            return;
        }

        
        if(_ataca.estado.GetCartaEstaAvancando() == true)
        {
            return;
        }
        

        if(_ataca.estado.GetCartaEstaSendoAtacada() == true)
        {
            return;
        }

        casaRetorno.CartaEntra(_ataca, casaRetorno.transform);

        Debug.Log($"E {_ataca.dados.nome} retorna para sua casa que está na posição : {printCasaAtacante.GetPosicaoCasa()}");
    }
    public void MoveParaCasaQueDestruiuCarta(CartaDaCena _ataca)
    {
        if (printCasaAtacante == null && printCasaDefensor == null)
            return;

        Debug.Log($"{_ataca.dados.nome} sai de sua casa com a posição : {printCasaAtacante.GetPosicaoCasa()}");

        GetCasaDefensor().CartaEntra(_ataca, printCasaDefensor.transform);

        Debug.Log($"E {_ataca.dados.nome} entra na casa com a posição : {printCasaDefensor.GetPosicaoCasa()}");
    }
}
