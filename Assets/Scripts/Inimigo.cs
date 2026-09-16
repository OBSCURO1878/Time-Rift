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
        Experiencia jogadorXP = FindAnyObjectByType<Experiencia>();

        if (jogadorXP != null)
        {
            jogadorXP.AdicionarXP(experiencia);
        }

        Debug.Log(
            "Inimigo derrotado! +" +
            experiencia +
            " XP"
        );

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CausarDano(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CausarDano(collision.gameObject);
        }
    }

    private void CausarDano(GameObject jogador)
    {
        if (Time.time < proximoDano)
            return;

        VidaPersonagem vidaJogador =
            jogador.GetComponent<VidaPersonagem>();

        if (vidaJogador != null)
        {
            vidaJogador.ReceberDano(dano);

            proximoDano = Time.time + intervaloDano;
        }
    }
}