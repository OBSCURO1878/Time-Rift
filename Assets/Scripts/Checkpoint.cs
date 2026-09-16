using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static Vector3 ultimaPosicao;
    private static bool checkpointAtivado = false;

    private void Start()
    {
        // Se ainda não existe checkpoint,
        // o primeiro ponto será a posição inicial do personagem.
        if (!checkpointAtivado)
        {
            GameObject jogador = GameObject.FindGameObjectWithTag("Player");

            if (jogador != null)
            {
                ultimaPosicao = jogador.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ultimaPosicao = other.transform.position;
            checkpointAtivado = true;

            Debug.Log("Checkpoint ativado!");
        }
    }

    public static Vector3 GetUltimaPosicao()
    {
        return ultimaPosicao;
    }

    public static bool ExisteCheckpoint()
    {
        return checkpointAtivado;
    }
}