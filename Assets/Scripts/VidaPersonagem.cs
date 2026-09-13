using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPersonagem : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 3;

    [Header("Interface")]
    [SerializeField] private Image[] coracoes;

    private int vidaAtual;

    private void Start()
    {
        vidaAtual = vidaMaxima;

        AtualizarCoracoes();
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual < 0)
        {
            vidaAtual = 0;
        }

        Debug.Log(
            "Jogador recebeu " +
            dano +
            " de dano. Vida atual: " +
            vidaAtual
        );

        AtualizarCoracoes();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void AtualizarCoracoes()
    {
        if (coracoes == null)
            return;

        for (int i = 0; i < coracoes.Length; i++)
        {
            coracoes[i].enabled = i < vidaAtual;
        }
    }

    private void Morrer()
    {
        Debug.Log("Jogador morreu!");

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public int GetVidaAtual()
    {
        return vidaAtual;
    }
}