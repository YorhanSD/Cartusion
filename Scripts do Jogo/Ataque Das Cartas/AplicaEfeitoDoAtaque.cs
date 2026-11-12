using UnityEngine;

public class AplicaEfeitoDoAtaque : MonoBehaviour
{
    public CartaDaCena estaCarta;
    private CartaDaCena cartaEncostada;

    private bool sangrando;
    private bool queimando;
    private bool virose;
    private bool nocauteada;
    private bool repelido;

    public void Start()
    {
        estaCarta = GetComponent<CartaDaCena>();
    }
    public void SetNocauteada(bool _nocauteada)
    {
        nocauteada = _nocauteada;
    }
    public bool GetNocauteada()
    {
        return nocauteada;
    }
    public void SetDoente(bool _virose)
    {
        virose = _virose;
    }
    public bool GetDoente()
    {
        return virose;
    }
    public void SetQueimando(bool _queimando)
    {
        queimando = _queimando;
    }
    public bool GetQueimando()
    {
        return queimando;
    }
    public void SetSangrando(bool _sangrando)
    {
        sangrando = _sangrando;
    }
    public bool GetSangrando()
    {
        return sangrando;
    }
    public void SetRepelido(bool _repelido)
    {
        repelido = _repelido;
    }
    public bool GetRepelido()
    {
        return repelido;
    }
   
}
