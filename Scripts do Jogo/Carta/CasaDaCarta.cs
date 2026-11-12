using UnityEngine;

public class CasaDaCarta : MonoBehaviour
{
    public CartaDaCena estaCarta;
    private Casa casaAntiga;
    private Casa casaAtual;
    
    void Start()
    {
        estaCarta = GetComponent<CartaDaCena>();
    }
    public void SetCasaAtual(Casa _casaAtual)
    {
        casaAtual = _casaAtual;
    }
    public Casa GetCasaAtual()
    {
        return casaAtual;
    }
    public void SetCasaAntiga(Casa _casaAntiga)
    {
        casaAntiga = _casaAntiga;
    }
    public Casa GetCasaAntiga()
    {
        return casaAntiga;
    }
}
