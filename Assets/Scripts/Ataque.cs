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

    private bool olhandoParaDireita = true;
    private bool atacando = false;

    private void Update()
    {
        AtualizarDirecao();

        if (Input.GetKeyDown(KeyCode.J) && !atacando)
        {
            StartCoroutine(ExecutarAtaque());
        }
    }

    private void AtualizarDirecao()
    {
        float movimento = Input.GetAxisRaw("Horizontal");

        if (movimento > 0)
        {
            olhandoParaDireita = true;
        }
        else if (movimento < 0)
        {
            olhandoParaDireita = false;
        }

        if (pontoAtaque != null)
        {
            Vector3 posicao = pontoAtaque.localPosition;

            if (olhandoParaDireita)
            {
                posicao.x = Mathf.Abs(posicao.x);
            }
            else
            {
                posicao.x = -Mathf.Abs(posicao.x);
            }

            pontoAtaque.localPosition = posicao;
        }

        if (clavaPivot != null)
        {
            Vector3 escala = clavaPivot.localScale;
            escala.x = olhandoParaDireita ? 1f : -1f;
            clavaPivot.localScale = escala;
        }
    }

    private IEnumerator ExecutarAtaque()
    {
        atacando = true;

        // Procura inimigos dentro da área de ataque
        Collider2D[] inimigos = Physics2D.OverlapCircleAll(
            pontoAtaque.position,
            raioAtaque,
            inimigoLayer
        );

        foreach (Collider2D inimigo in inimigos)
        {
            Inimigo alvo = inimigo.GetComponent<Inimigo>();

            if (alvo != null)
            {
                alvo.ReceberDano(dano);
            }
        }

        // Animação da clava
        if (clavaPivot != null)
        {
            float inicio = anguloInicial;
            float fim = anguloFinal;

            if (!olhandoParaDireita)
            {
                inicio = -anguloInicial;
                fim = -anguloFinal;
            }

            float tempo = 0f;

            while (tempo < duracaoAtaque)
            {
                tempo += Time.deltaTime;

                float progresso = tempo / duracaoAtaque;

                float angulo = Mathf.Lerp(inicio, fim, progresso);

                clavaPivot.localRotation = Quaternion.Euler(
                    0f,
                    0f,
                    angulo
                );

                yield return null;
            }

            clavaPivot.localRotation = Quaternion.identity;
        }

        atacando = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (pontoAtaque != null)
        {
            Gizmos.DrawWireSphere(
                pontoAtaque.position,
                raioAtaque
            );
        }
    }
}