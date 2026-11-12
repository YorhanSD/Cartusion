using UnityEngine;

public class IA_EscolheAtaque : MonoBehaviour
{
    public Baralho_Oponente baralho_Oponente;

    void Start()
    {
        baralho_Oponente = GetComponent<Baralho_Oponente>();
    }
    public void SetEscolheAtaqueDaCarta(CartaDaCena _carta)
    {
        _carta.listaAtaque[1].SetAtaqueAtivado(true);
        _carta.dados.nomeAtaqueSelecionado = _carta.listaAtaque[1].nome;
        _carta.uiCarta.AtualizarUI(_carta.dados);

        Debug.Log($"IA definiu o Ataque : {_carta.listaAtaque[1].nome} para a Carta : {_carta.dados.nome}");
    }
}
