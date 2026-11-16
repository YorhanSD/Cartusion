using UnityEngine;

public class Regras_Jogo : MonoBehaviour
{
    // AMBOS OS JOGADORES TÊM 2 MINUTOS PARA JOGAR.

    // EM TODOS OS TURNOS, O JOGADOR DEVE USAR TODAS AS CARTAS, COLOCANDO-AS NAS CASAS DISPONÍVEIS.

    // APÓS SACAR 3 CARTAS DE CRIATURA, O JOGADOR PODE SACAR 1 CARTA DO BARALHO DE CONSUMÍVEIS NO TURNO SEGUINTE.

    // APÓS USAR 1 CARTA CONSUMÍVEL O JOGADOR PODE SACAR UMA CARTA DO MONTE DE CARTAS DEFENSIVAS.

    public GameObject telaDeVitoria;
    public GameObject telaDeDerrota;

    public void AtivaTela_Vitoria()
    {
        telaDeVitoria.SetActive(true);
    }

    public void AtivaTela_Derrota()
    {
        telaDeDerrota.SetActive(true);
    }
}
