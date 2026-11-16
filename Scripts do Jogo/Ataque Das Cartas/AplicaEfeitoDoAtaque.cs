using UnityEngine;

public class AplicaEfeitoDoAtaque : MonoBehaviour
{
    public CartaDaCena estaCarta;

    private int tempoNocauteada = 3;
    private bool podeSerNocauteada = true;

    [SerializeField] private bool perfura;
    [SerializeField] private bool sangrando;
    [SerializeField] private bool queimando;
    [SerializeField] private bool virose;
    [SerializeField] private bool nocauteada;
    [SerializeField] private bool repelido;
    [SerializeField] private bool imuneReacao;
    [SerializeField] private bool avancar;

    public void Start()
    {
        estaCarta = GetComponent<CartaDaCena>();
    }
    public void SetPerfuraCouraca(bool _perfura)
    {
        perfura = _perfura;
    }
    public bool GetPerfuraCouraca()
    {
        return perfura;
    }
    public void SetImuneReacao(bool _imune)
    {
        imuneReacao = _imune;
    }
    public bool GetImuneReacao()
    {
        return imuneReacao;
    }
    public void SetAvancar(bool _fugir)
    {
        avancar = _fugir;
    }
    public bool GetAvancar()
    {
        return avancar;
    }
    public void RemoveTempoNocauteada()
    {
        tempoNocauteada -= 1;

        Debug.Log("Turnos sem poder ser nocauteada: " + tempoNocauteada);

        if(tempoNocauteada <= 1)
        {
            SetNocauteada(false);

            SetPodeSerNocauteada(true); //QUANDO ACABA ELA PODE SER NOCAUTEADA NOVAMENTE

            tempoNocauteada = 1;

            return;
        }
    }
    public void SetPodeSerNocauteada(bool _pode)
    {
        podeSerNocauteada = _pode;
    }
    public bool GetPodeSerNocauteada()
    {
        return podeSerNocauteada;
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
