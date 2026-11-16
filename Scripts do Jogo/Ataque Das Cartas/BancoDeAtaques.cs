using System.Collections.Generic;
using UnityEngine;

public class BancoDeAtaques : MonoBehaviour
{
    BancoCards bancoCartas;

    public List<Ataque> geralAtaqueLista = new List<Ataque>();

    public int contaID = 0;
    public int contaAtaqueID = 0;
    public int numeroTotalDeCartasAtualmente = 12;

    void Start()
    {
        bancoCartas = GetComponent<BancoCards>();

        CriaAtaques();
    }

    public void CriaAtaques()
    {
        for (int i = 0; i < numeroTotalDeCartasAtualmente; i++)
        {
            AdicionaAtaqueDaquelaCarta();

            contaID++;
        }
    }

    public void AdicionaAtaqueDaquelaCarta()
    {
        for (int i = 0; i < 2; i++)
        {
            geralAtaqueLista.Add(bancoCartas.geralCartaCenaLista[contaID].listaAtaque[i]);

            geralAtaqueLista[contaAtaqueID].id = contaAtaqueID;

            contaAtaqueID++;
        }
    }
}
