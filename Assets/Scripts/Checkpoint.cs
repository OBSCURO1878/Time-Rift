using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static Vector3 ultimaPosicao;
    private static int checkpointId = 0; // 0 = nenhum, >0 = id do checkpoint ativo

    [SerializeField]
    [Tooltip("ID único deste checkpoint (1,2,3,...).")]
    private int id = 1;

    private Animator anim;
    private bool jaAtivado = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

        // CORREÇÃO: Atualizado para o nome novo da sua animação "Bandeira_Balançando"
        if (checkpointId > 0 && checkpointId == id)
        {
            jaAtivado = true;
            if (anim != null)
            {
                anim.Play("Bandeira_Balançando");
            }
        }
        else
        {
            jaAtivado = false;
        }

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
        if (other.CompareTag("Player") && !jaAtivado)
        {
            jaAtivado = true;
            ultimaPosicao = transform.position; // Salva a posição exata da bandeira
            checkpointId = id;

            // Ativa a animação visual da bandeira subindo!
            if (anim != null)
            {
                anim.SetTrigger("Ativar");
            }

            Debug.Log($"Checkpoint {id} ativado visualmente e fisicamente!");
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

    public static void ResetCheckpoint()
    {
        checkpointId = 0;
        ultimaPosicao = Vector3.zero;
        Debug.Log("Checkpoint resetado.");
    }
}
