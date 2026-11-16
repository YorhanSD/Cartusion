using System.Collections;
using UnityEngine;

public class VerificaCouraca : MonoBehaviour
{
    public ControleDeDano controleDeDano;
    void Start()
    {
        controleDeDano = GetComponent<ControleDeDano>();
    }
    
    public void AplicaDanoACouraca(CartaDaCena _cartaAtacante, CartaDaCena _defende)
    {
        foreach (Ataque ataque in _cartaAtacante.listaAtaque)
        {
            if (ataque.GetAtaqueAtivado() == true && _cartaAtacante.aplicaEfeitoDoAtaque.GetPerfuraCouraca() == true)
            {
                controleDeDano.ChecaEspecie(_cartaAtacante, _defende);

                Debug.Log($"A carta : {_cartaAtacante.dados.nome} seu Ataque: {ataque.nome} ignora couraça e aplica dano direto a vida da criatura oponente");

                break;
            }
        }

        if (_defende.dados.couracaAtual <= 0)
        {
            controleDeDano.ChecaEspecie(_cartaAtacante, _defende); // SEM COURAÇA O DANO É DIRETO NA VIDA

            Debug.Log($"A carta : {_defende.dados.nome} não possuí mais couraça");
        }
        else
        {
            _defende.dados.couracaAtual -= 10;

            StartCoroutine(Intervalo( _defende));

            Debug.Log($"A carta : {_defende.dados.nome} perdeu 10 pontos de couraça");
        }
    }

    private IEnumerator Intervalo( CartaDaCena _defende)
    {
        yield return new WaitForSeconds(1f);

        if(_defende.dados.vidaAtual > 0)
        {
            controleDeDano.CriaPainelDano(_defende.gameObject.transform, 10);

            controleDeDano.CriaPainelEfeitoDoAtaque(_defende.gameObject.transform, "Couraça");

            _defende.uiCarta.AtualizarUI(_defende.dados);
        }
    }
}
