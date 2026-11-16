using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberaCasas : MonoBehaviour
{
    public List<Bloqueador> listaBloqueadores = new List<Bloqueador>();
    IA_MapeamentoDeCases ia_MapeamentoDeCases;

    //Bloqueador bloqueador;
    public GameObject[] bloqueadores;

    [System.Obsolete]
    void Start()
    {
        ia_MapeamentoDeCases = GetComponent<IA_MapeamentoDeCases>();
        //bloqueador = FindObjectOfType<Bloqueador>();
    }
    public void Liberar(int posicaoCasa)
    {
        StartCoroutine(LiberaCasasBloqueadas(posicaoCasa));
    }
    private void DesativaBloqueador(int posicao)
    {
        Collider2D colisor = bloqueadores[posicao].GetComponent<Collider2D>();

        if (colisor != null)
            colisor.enabled = false;
    }

    private void AtivaBloqueador(int posicao)
    {
        Collider2D colisor = bloqueadores[posicao].GetComponent<Collider2D>();

        if (colisor != null)
            colisor.enabled = true;
    }
    public IEnumerator LiberaCasasBloqueadas(int posicaoCasa)
    {
        yield return new WaitForSeconds(0.5f);

        bloqueadores[posicaoCasa].SetActive(false);
        //DesativaBloqueador(posicaoCasa);

        // Se a posição da casa for um número par então:
        // Exemplo, carta entrou na casa 12.
        // As casas liberadas são estas:

        if (posicaoCasa % 2 == 0)
        {
            if (posicaoCasa != 14 && posicaoCasa != 15)
            {
                posicaoCasa += 2; //CASA 14
                bloqueadores[posicaoCasa].SetActive(false);
                //DesativaBloqueador(posicaoCasa);
                ia_MapeamentoDeCases.listaCase[posicaoCasa].SetEstaBloqueado(false);

                //Debug.Log($"Número da casa da direita que deve ficar livre : {posicaoCasa}");
            }
            if (posicaoCasa != 0 && posicaoCasa != 1)
            {
                posicaoCasa -= 1; //CASA 13
                bloqueadores[posicaoCasa].SetActive(false);
                //DesativaBloqueador(posicaoCasa);
                ia_MapeamentoDeCases.listaCase[posicaoCasa].SetEstaBloqueado(false);

                //Debug.Log($"Número da casa de baixo que deve ficar livre : {posicaoCasa}");
            }
            if (posicaoCasa != 0 && posicaoCasa != 1)
            {
                posicaoCasa -= 3; //CASA 10
                bloqueadores[posicaoCasa].SetActive(false);
                //DesativaBloqueador(posicaoCasa);
                ia_MapeamentoDeCases.listaCase[posicaoCasa].SetEstaBloqueado(false);

                //Debug.Log($"Número da casa da esquerda que deve ficar livre : {posicaoCasa}");
            }
        }
        else if (posicaoCasa % 2 != 0)
        {
            // Se a posição da casa é um número ímpar então:

            // Exemplo, carta entrou na casa 13.

            // As casas liberadas são estas:

            if (posicaoCasa != 14 && posicaoCasa != 15)
            {
                posicaoCasa += 2; //CASA 15
                bloqueadores[posicaoCasa].SetActive(false);
                //DesativaBloqueador(posicaoCasa);
                ia_MapeamentoDeCases.listaCase[posicaoCasa].SetEstaBloqueado(false);

                //Debug.Log($"Número da casa da direita que deve ficar livre : {posicaoCasa}");
            }
            if (posicaoCasa != 0 && posicaoCasa != 1)
            {
                posicaoCasa -= 3; //CASA 12
                bloqueadores[posicaoCasa].SetActive(false);
                //DesativaBloqueador(posicaoCasa);
                ia_MapeamentoDeCases.listaCase[posicaoCasa].SetEstaBloqueado(false);

                //Debug.Log($"Número da casa de cima que deve ficar livre : {posicaoCasa}");
            }
            if (posicaoCasa != 0 && posicaoCasa != 1)
            {
                posicaoCasa -= 1; //CASA 11
                bloqueadores[posicaoCasa].SetActive(false);
                //DesativaBloqueador(posicaoCasa);
                ia_MapeamentoDeCases.listaCase[posicaoCasa].SetEstaBloqueado(false);

                //Debug.Log($"Número da casa da esquerda que deve ficar livre : {posicaoCasa}");
            }
        }

    }
}
