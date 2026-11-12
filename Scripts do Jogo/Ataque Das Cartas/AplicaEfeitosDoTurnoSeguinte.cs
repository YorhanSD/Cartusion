using UnityEngine;

public class AplicaEfeitosDoTurnoSeguinte : MonoBehaviour
{
    IA_MapeamentoDeCases ia_MapeamentoDeCases;
    BancoCards bancoCards;
    SistemaCombate sistemaCombate;
    ControleDeDano controleDeDano;
    void Start()
    {
        controleDeDano = GetComponent<ControleDeDano>();
        ia_MapeamentoDeCases = GetComponent<IA_MapeamentoDeCases>();
        bancoCards = GetComponent<BancoCards>();
        sistemaCombate = GetComponent<SistemaCombate>();
    }
    public void AplicaNocaute()
    {
        foreach (CartaDaCena _carta in bancoCards.geralCartaCenaLista)
        {
            if (_carta.aplicaEfeitoDoAtaque.GetNocauteada())
            {
                _carta.SetMoveuSe(true);
                _carta.SetPodeAtacar(false);

                controleDeDano.CriaPainelEfeitoDoAtaque(_carta.gameObject.transform, "Noucateado");
            }
        }
    }
    public void AplicaVirose()
    {
        foreach (CartaDaCena _carta in bancoCards.geralCartaCenaLista)
        {
            if (_carta.aplicaEfeitoDoAtaque.GetDoente())
            {
                _carta.dados.ataqueAtual -= 10;
                _carta.dados.reacao -= 10;
                                
                controleDeDano.CriaPainelEfeitoDoAtaque(_carta.gameObject.transform, "Doente");

                _carta.uiCarta.AtualizarUI(_carta.dados); // Atualiza UI da Carta
            }
        }
    }
    public void AplicaQueimadura()
    {
        foreach (CartaDaCena _carta in bancoCards.geralCartaCenaLista)
        {
            if (_carta.aplicaEfeitoDoAtaque.GetQueimando())
            {
                if (_carta.dados.couracaAtual > 0)
                {
                    if (_carta.dados.vidaAtual > 0)
                    {
                        _carta.dados.couracaAtual -= 10;
                    }
                }
                else
                {
                    if (_carta.dados.vidaAtual > 0)
                    {
                        _carta.dados.vidaAtual -= 10;

                        controleDeDano.CriaPainelDano(_carta.gameObject.transform, 10);
                        controleDeDano.CriaPainelEfeitoDoAtaque(_carta.gameObject.transform, "Queimadura");
                    }
                }

                _carta.uiCarta.AtualizarUI(_carta.dados);

                sistemaCombate.VerificaMortePorEfeito(_carta); // Verifica se a carta morreu
            }
        }
    }
    public void AplicaSangramento()
    {
        foreach (CartaDaCena _carta in bancoCards.geralCartaCenaLista)
        {
            if (_carta.aplicaEfeitoDoAtaque.GetSangrando())
            {
                if (_carta.dados.vidaAtual > 0)
                {
                    _carta.dados.vidaAtual -= 20;

                    controleDeDano.CriaPainelDano(_carta.gameObject.transform, 20);
                    controleDeDano.CriaPainelEfeitoDoAtaque(_carta.gameObject.transform, "Sangrando");
                }
            }
            _carta.uiCarta.AtualizarUI(_carta.dados);

            sistemaCombate.VerificaMortePorEfeito(_carta); // Verifica se a carta morreu
        }
    }
}
