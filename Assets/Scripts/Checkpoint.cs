using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static Vector3 ultimaPosicao;
    // 0 = nenhum checkpoint, >0 = id do checkpoint ativo
    private static int checkpointId = 0;

    [SerializeField]
    [Tooltip("ID único deste checkpoint (1,2,3,...). Use 0 para ponto de spawn inicial.)")]
    private int id = 1;

    private void Start()
    {
        // Se não existe checkpoint ativo (id == 0),
        // o primeiro ponto será a posição inicial do personagem.
        if (checkpointId == 0)
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
            checkpointId = id;

            Debug.Log($"Checkpoint {id} ativado!");
        }
    }

    public static Vector3 GetUltimaPosicao()
    {
        return ultimaPosicao;
    }

    public static bool ExisteCheckpoint()
    {
        return checkpointId != 0;
    }

    public static int GetCheckpointId()
    {
        return checkpointId;
    }

    // Zera o checkpoint (usar ao finalizar fase / reiniciar jogo)
    public static void ResetCheckpoint()
    {
        checkpointId = 0;
        ultimaPosicao = Vector3.zero;
        Debug.Log("Checkpoint resetado.");
    }
}
