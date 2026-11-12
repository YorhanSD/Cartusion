using TMPro;
using UnityEngine;

public class ControleDeDano : MonoBehaviour
{
    public GameObject painelDanoEfeito;

    public void CriaPainelDano(Transform carta, int dano)
    {
        GameObject painel = Instantiate(painelDanoEfeito, carta, false);

        painel.transform.localPosition = Vector3.zero;

        PainelDano controlePainel = painel.GetComponent<PainelDano>();

        controlePainel.MostrarDano(dano);
    }

    public void CriaPainelEfeitoDoAtaque(Transform carta, string efeito)
    {
        GameObject painel = Instantiate(painelDanoEfeito, carta, false);

        painel.transform.localPosition = Vector3.zero;

        PainelDano controlePainel = painel.GetComponent<PainelDano>();

        controlePainel.MostrarEfeito(efeito);
    }


    public void ChecaEspecie(CartaDaCena ataca, CartaDaCena defende)
    {
        if (ataca.dados.especieSelecionada == "Celestial" && defende.dados.especieSelecionada == "Tenebroso")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual * 2; //Multiplica o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual * 2}] de dano [Super Eficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual * 2);
        }
        else if (ataca.dados.especieSelecionada == "Tenebroso" && defende.dados.especieSelecionada == "Celestial")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual / 2; //Divide o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual / 2}] de dano [Ineficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual / 2);
        }
        else if (ataca.dados.especieSelecionada == "Extinto" && defende.dados.especieSelecionada == "Celestial")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual * 2; //Multiplica o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual * 2}] de dano [Super Eficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual * 2);
        }
        else if (ataca.dados.especieSelecionada == "Celestial" && defende.dados.especieSelecionada == "Extinto")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual / 2; //Divide o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual / 2}] de dano [Ineficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual / 2);
        }
        else if (ataca.dados.especieSelecionada == "Espacial" && defende.dados.especieSelecionada == "Extinto")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual * 2; //Multiplica o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual * 2}] de dano [Super Eficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual * 2);
        }
        else if (ataca.dados.especieSelecionada == "Extinto" && defende.dados.especieSelecionada == "Celestial")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual / 2; //Divide o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual / 2}] de dano [Ineficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual / 2);
        }
        else if (ataca.dados.especieSelecionada == "Tenebroso" && defende.dados.especieSelecionada == "Espacial")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual * 2; //Multiplica o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual * 2}] de dano [Super Eficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual * 2);
        }
        else if (ataca.dados.especieSelecionada == "Espacial" && defende.dados.especieSelecionada == "Tenebroso")
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual / 2; //Divide o Dano por 2

            Debug.Log($"Atacante: {ataca.dados.nome} tem a espécie: {ataca.dados.especieSelecionada}");
            Debug.Log($"Defensor: {defende.dados.nome} tem a espécie: {defende.dados.especieSelecionada}");

            Debug.Log($"{ataca.dados.nome} com o ID: {ataca.dados.ID} aplica [{ataca.dados.ataqueAtual / 2}] de dano [Ineficaz] a {defende.dados.nome} com o ID: {defende.dados.ID}");

            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual / 2);
        }
        else
        {
            defende.dados.vidaAtual -= ataca.dados.ataqueAtual;
            CriaPainelDano(defende.gameObject.transform, ataca.dados.ataqueAtual);
        }
    }

}
