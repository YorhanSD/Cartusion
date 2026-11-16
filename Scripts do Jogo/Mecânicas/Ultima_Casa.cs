using UnityEngine;

public class Ultima_Casa : MonoBehaviour
{
    CartaNucleo cartaNucleo;

    [System.Obsolete]
    private void Start()
    {
        cartaNucleo = FindObjectOfType<CartaNucleo>();
    }

    public void OnTriggerEnter2D(Collider2D _carta)
    {
        if (_carta.gameObject.tag == "Carta Oponente" && this.gameObject.name == "Casa 0" || _carta.gameObject.tag == "Carta Oponente" && this.gameObject.name == "Casa 1")
        {
            
        }

        if (_carta.gameObject.tag == "Carta Jogador" && this.gameObject.name == "Casa 14" || _carta.gameObject.tag == "Carta Jogador" && this.gameObject.name == "Casa 15")
        {
           
        }
    }
}
