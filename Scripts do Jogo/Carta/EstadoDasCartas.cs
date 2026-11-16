using UnityEngine;

public class EstadoDasCartas : MonoBehaviour
{
    public CartaDaCena estaCarta;

    [SerializeField] private bool cartaAtiva = false;

    [SerializeField] private bool podeAtacar = true;

    [SerializeField] private bool moveuSe = false;

    [SerializeField] private bool atacando = false;

    [SerializeField] private bool morreu = false;

    [SerializeField] private bool sendoAtacada = false;

    [SerializeField] private bool estaAvancando = false;

    public void Start()
    {
        estaCarta = GetComponent<CartaDaCena>();
    }
    public void SetCartaEstaAvancando(bool _avancando)
    {
        estaAvancando = _avancando;
    }
    public bool GetCartaEstaAvancando()
    {
        return estaAvancando;
    }
    public void SetCartaSendoAtacada(bool _atacada)
    {
        sendoAtacada = _atacada;
    }
    public bool GetCartaEstaSendoAtacada()
    {
        return sendoAtacada;
    }
    public void SetCartaMorreu(bool _morreu)
    {
        morreu = _morreu;
    }
    public bool GetCartaMorreu()
    {
        return morreu;
    }
    public void SetCartaEstaAtacando(bool _atacando)
    {
        atacando = _atacando;
    }
    public bool GetCartaEstaAtacando()
    {
        return atacando;
    }
    public void SetPodeAtacar(bool _atacou)
    {
        podeAtacar = _atacou;
    }
    public bool GetPodeAtacar()
    {
        return podeAtacar;
    }
    public void SetEstaAtivada(bool _ativada)
    {
        cartaAtiva = _ativada;
    }
    public bool GetEstaAtivada()
    {
        return cartaAtiva;
    }
    public bool GetMoveuSe()
    {
        return moveuSe;
    }
    public void SetMoveuSe(bool _moveuSe)
    {
        moveuSe = _moveuSe;
    }

}
