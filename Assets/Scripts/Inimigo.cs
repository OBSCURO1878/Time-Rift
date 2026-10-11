using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vida = 3;

    [Header("Combate")]
    [SerializeField] private int dano = 1;
    [SerializeField] private float intervaloDano = 1f;

    [Header("Experiência")]
    [SerializeField] private int experiencia = 10;

    private float proximoDano;

    public void ReceberDano(int danoRecebido)
    {
        vida -= danoRecebido;

        Debug.Log(
            "Inimigo recebeu " +
            danoRecebido +
            " de dano. Vida restante: " +
            vida
        );

        if (vida <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        // Envia a pontuação para o GameController unificado
        if (GameController.instance != null)
        {
            GameController.instance.AdicionarPontos(experiencia);
        }

        Debug.Log(
            "Inimigo derrotado! +" +
            experiencia +
            " XP"
        );

        Destroy(gameObject);
    }
}
