using TMPro;
using UnityEngine;
using System.Collections;

public class PainelDano : MonoBehaviour
{
    [SerializeField] TMP_Text textoDano;
    [SerializeField] TMP_Text textoEfeitoNegativo;

    public void MostrarDano(int dano)
    {
        textoDano.text = "- " + dano.ToString();
        StartCoroutine(Intervalo());
    }
    public void MostrarEfeito(string efeito)
    {
        textoEfeitoNegativo.text = efeito;
        StartCoroutine(Intervalo());
    }

    private IEnumerator Intervalo()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Animacao());
    }
   
    IEnumerator Animacao()
    {
        RectTransform rect = GetComponent<RectTransform>();
        CanvasGroup canvas = GetComponent<CanvasGroup>();

        Vector3 inicio = rect.localPosition;
        Vector3 fim = inicio + Vector3.up * 150;

        float tempo = 0;
        float duracao = 3f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;

            float t = tempo / duracao;

            rect.localPosition = Vector3.Lerp(inicio, fim, t);

            canvas.alpha = 1 - t;

            yield return null;
        }

        Destroy(gameObject);
    }
}
