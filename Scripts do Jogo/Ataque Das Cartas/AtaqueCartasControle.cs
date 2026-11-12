using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtaqueCartasControle : MonoBehaviour
{
    public TextMeshProUGUI textoEspecieDoAtaque;
    public TextMeshProUGUI textoAtaqueI;
    public TextMeshProUGUI textoAtaqueNeutro;
    public TextMeshProUGUI nomeCarta;
    public GameObject telaAtaques;
    public CartaDaCena cartaPrint;
    private bool abreTela = false;

    public void NomeInformadoNaTela()
    {
        nomeCarta.text = cartaPrint.dados.nome;
    }
    public void MudaInformacoesDosBotoes()
    {
        textoAtaqueNeutro.text = cartaPrint.listaAtaque[0].nome;
        textoEspecieDoAtaque.text = cartaPrint.listaAtaque[1].especie;
        textoAtaqueI.text = cartaPrint.listaAtaque[1].nome;
    }
    public void BotaoAtaqueII() //Ataque com o fator espécie
    {
        cartaPrint.dados.nomeAtaqueSelecionado = textoAtaqueI.text;
        cartaPrint.uiCarta.AtualizarUI(cartaPrint.dados);

        cartaPrint.listaAtaque[0].SetAtaqueAtivado(false);

        cartaPrint.listaAtaque[1].SetAtaqueAtivado(true);
    }
    public void BotaoAtaqueI() //Ataque neutro, causa dano normal a qualquer carta
    {
        cartaPrint.dados.nomeAtaqueSelecionado = textoAtaqueNeutro.text;
        cartaPrint.uiCarta.AtualizarUI(cartaPrint.dados);

        cartaPrint.listaAtaque[0].SetAtaqueAtivado(true);

        cartaPrint.listaAtaque[1].SetAtaqueAtivado(false);
    }
    public void TelaAtaques()
    {
        abreTela = !abreTela;
        telaAtaques.SetActive(abreTela);
    }
    public void RecebeInformacoesDaCarta(CartaDaCena _cartaSelecionada)
    {
        cartaPrint = _cartaSelecionada;

        NomeInformadoNaTela();
        MudaInformacoesDosBotoes();
    }
}   
