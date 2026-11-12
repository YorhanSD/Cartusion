using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public int idUI;

    public string nomeUI;

    public string nomeAtaqueSelecionadoUI;

    public Slider barraCouraca;

    public Slider barraVida;

    public int vidaAtual;

    public int vidaMAX;

    public int couracaAtual;

    public int couracaMAX;

    public int ataqueUI;

    public TextMeshProUGUI nomeTMPRO;

    public TextMeshProUGUI ataqueTMPRO;

    public TextMeshProUGUI vidaMaximaTMPRO;

    public TextMeshProUGUI vidaAtualTMPRO;

    public TextMeshProUGUI couracaAtualTMPRO;

    //public TextMeshProUGUI couracaMaximaTMPRO;

    public TextMeshProUGUI reacaoTMPRO;

    public TextMeshProUGUI ataqueSelecionadoTMPRO;

    public CartaRuntime cartaRuntime;

    public void AtualizarUI(CartaRuntime _carta)
    {
        nomeAtaqueSelecionadoUI = _carta.nomeAtaqueSelecionado;

        barraCouraca.value = _carta.couracaAtual;
        barraCouraca.maxValue = _carta.couracaMaxima;

        barraVida.maxValue = _carta.vidaMaxima;
        barraVida.value = _carta.vidaAtual;

        vidaMAX = _carta.vidaMaxima;
        vidaAtual = _carta.vidaAtual;

        couracaMAX = _carta.couracaMaxima;
        couracaAtual = _carta.couracaAtual;

        if (vidaAtualTMPRO != null && vidaMaximaTMPRO != null && couracaAtualTMPRO != null)
        {
            vidaAtualTMPRO.text = vidaAtual.ToString();
            vidaMaximaTMPRO.text = vidaMAX.ToString();
            
            couracaAtualTMPRO.text = couracaAtual.ToString();
        }
        else
        {
            Debug.Log(this.gameObject.name + " tem componentes nulos");
        }

        if (nomeTMPRO != null && ataqueTMPRO != null && couracaAtualTMPRO != null && reacaoTMPRO != null && ataqueSelecionadoTMPRO != null)
        {
            nomeTMPRO.text = _carta.nome;
            ataqueTMPRO.text = $"AT {_carta.ataqueAtual.ToString()}";
            couracaAtualTMPRO.text = $"{_carta.couracaAtual.ToString()}";
            reacaoTMPRO.text = $"RE {_carta.reacao.ToString()}";
            ataqueSelecionadoTMPRO.text = $"{_carta.nomeAtaqueSelecionado.ToString()}";
        }
        else
        {
            Debug.Log(this.gameObject.name + " tem componentes nulos");
        }
    }
}
