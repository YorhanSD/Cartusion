using UnityEngine;

[CreateAssetMenu(fileName = "NovoAtaque", menuName = "Ataque/AtaqueOriginal")]
public class Ataque : ScriptableObject
{
    public int id;

    public string nome;

    [TextArea(7, 7)]

    public string descricao;

    public string especie;

    public bool imuneReacao;

    public bool ignoraCouraca;

    public bool nocauteia;

    public bool sangramento;

    public bool queimadura;

    public bool removeCouraca;

    public bool virose;

    public bool repulsao;

    public bool avancar;

    private bool ataqueAtivado;

    public void SetAtaqueAtivado(bool _ataqueAtivado)
    {
        ataqueAtivado = _ataqueAtivado;
    }

    public bool GetAtaqueAtivado()
    {
        return ataqueAtivado;
    }
}
