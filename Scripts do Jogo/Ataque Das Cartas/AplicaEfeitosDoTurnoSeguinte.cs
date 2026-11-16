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
            if (_carta.aplicaEfeitoDoAtaque.GetNocauteada() == true)
            {
                //CONTROLE DE TURNOS É O RESPONSÁVEL POR RESETAR O MOVIMENTO

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
            }

            _carta.uiCarta.AtualizarUI(_carta.dados); // Atualiza UI da Carta
        }
    }
    public void AplicaQueimadura()
    {
        if (bancoCards == null) return;

        for (int i = bancoCards.geralCartaCenaLista.Count - 1; i >= 0; i--)
        {
            CartaDaCena carta = bancoCards.geralCartaCenaLista[i];

            if (carta == null)
                continue;

            if (carta.aplicaEfeitoDoAtaque.GetQueimando())
            {
                if (carta.dados.couracaAtual > 0)
                {
                    if (carta.dados.vidaAtual > 0)
                    {
                        carta.dados.couracaAtual -= 20;

                        controleDeDano.CriaPainelDano(carta.transform, 20);

                        controleDeDano.CriaPainelEfeitoDoAtaque(carta.transform, "Queimando");
                    }
                }
                else
                {
                    if (carta.dados.vidaAtual > 0)
                    {
                        carta.dados.vidaAtual -= 10;

                        controleDeDano.CriaPainelDano(carta.transform, 10);

                        controleDeDano.CriaPainelEfeitoDoAtaque(carta.transform, "Queimando");

                        if (carta.dados.vidaAtual <= 0)
                        {
                            sistemaCombate.VerificaMortePorEfeito(carta);

                            continue;
                        }
                    }
                }
            }

            if (carta != null && carta.uiCarta != null)
            {
                carta.uiCarta.AtualizarUI(carta.dados);
            }
        }

    }
    public void AplicaSangramento()
    {
        if (bancoCards == null) return;

        for (int i = bancoCards.geralCartaCenaLista.Count - 1; i >= 0; i--)
        {
            CartaDaCena carta = bancoCards.geralCartaCenaLista[i];

            if (carta == null)
                continue;

            if (carta.aplicaEfeitoDoAtaque.GetSangrando() && carta != null)
            {
                if (carta.dados.vidaAtual > 0 && carta != null)
                {
                    carta.dados.vidaAtual -= 20;

                    controleDeDano.CriaPainelDano(carta.gameObject.transform, 20);
                    controleDeDano.CriaPainelEfeitoDoAtaque(carta.gameObject.transform, "Sangrando");

                    if (carta.dados.vidaAtual <= 0)
                    {
                        sistemaCombate.VerificaMortePorEfeito(carta); // Verifica se a carta morreu
                        continue;
                    }
                }
            }

            if (carta != null && carta.uiCarta != null)
            {
                carta.uiCarta.AtualizarUI(carta.dados);
            }
        }
    }
}
