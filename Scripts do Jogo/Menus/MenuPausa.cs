using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject painelPause;

    private bool jogoPausado = false;

    void Start()
    {
        painelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (jogoPausado)
                ContinuarJogo();
            else
                PausarJogo();
        }
    }

    public void PausarJogo()
    {
        jogoPausado = true;
        painelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinuarJogo()
    {
        jogoPausado = false;
        painelPause.SetActive(false);
        Time.timeScale = 1f;
    }
}
