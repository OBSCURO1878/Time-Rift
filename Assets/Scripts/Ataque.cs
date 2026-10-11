using System.Collections;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    [Header("Área de Ataque")]
    [SerializeField] private Transform pontoAtaque;
    [SerializeField] private float raioAtaque = 1f;
    [SerializeField] private int dano = 1;
    [SerializeField] private LayerMask inimigoLayer;

    [Header("Clava")]
    [SerializeField] private Transform clavaPivot;
    [SerializeField] private float anguloInicial = -45f;
    [SerializeField] private float anguloFinal = 60f;
    [SerializeField] private float duracaoAtaque = 0.2f;
    [SerializeField] private Color gizmoColor = Color.red;

    private bool atacando = false;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        if (anim == null)
            anim = GetComponent<Animator>() ?? GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // Se pressionar J e não estiver atacando, executa o ataque
        if (Input.GetKeyDown(KeyCode.J) && !atacando)
        {
            StartCoroutine(ExecutarAtaque());
        }
    }

    private IEnumerator ExecutarAtaque()
    {
        atacando = true;

        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        // Como o Objeto PontoAtaque vira junto com o corpo (graças ao eulerAngles do Movimento), 
        // a origem sempre será a posição global exata do objeto na cena!
        Vector2 origem = pontoAtaque != null ? (Vector2)pontoAtaque.position : (Vector2)transform.position;

        Debug.Log("Executando ataque na posição: " + origem);

        // Procura inimigos dentro do círculo de ataque
        Collider2D[] inimigos = Physics2D.OverlapCircleAll(origem, raioAtaque, inimigoLayer);

        Debug.Log("Inimigos encontrados: " + inimigos.Length);

        foreach (Collider2D inimigo in inimigos)
        {
            Inimigo alvo = inimigo.GetComponent<Inimigo>();
            if (alvo != null)
            {
                alvo.ReceberDano(dano);
            }
        }

        // Animação visual da clava (Rotaciona localmente baseada no Pivot)
        if (clavaPivot != null)
        {
            float tempo = 0f;
            while (tempo < duracaoAtaque)
            {
                tempo += Time.deltaTime;
                float progresso = tempo / duracaoAtaque;
                float angulo = Mathf.Lerp(anguloInicial, anguloFinal, progresso);

                clavaPivot.localRotation = Quaternion.Euler(0f, 0f, angulo);
                yield return null;
            }
            clavaPivot.localRotation = Quaternion.identity;
        }

        atacando = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha o círculo vermelho perfeitamente onde o PontoAtaque estiver na Scene
        Vector2 origem = pontoAtaque != null ? (Vector2)pontoAtaque.position : (Vector2)transform.position;

        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(origem, raioAtaque);
        Gizmos.DrawLine(transform.position, origem);
        Gizmos.DrawCube(origem, Vector3.one * 0.1f);
    }
}
