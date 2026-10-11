using UnityEngine;

public class Movimento : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private int velocity = 5;

    [SerializeField] private LayerMask chaoLayer;

    [SerializeField] private GameObject menuPausePainel;

    [SerializeField] private float JumpForce = 10f;

    private int pulosRestantes = 2;
    private bool jogoPausado = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // 1. Verifica o Pause primeiro
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // 2. Bloqueia os comandos se o jogo estiver pausado
        if (jogoPausado) return;

        // 3. Movimento horizontal
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Walk", false);
        }



        // 4. Pulo duplo
        if (Input.GetKeyDown(KeyCode.Space) && pulosRestantes > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            pulosRestantes--;

            anim.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
    {
        // Interrompe a movimentação física se o jogo estiver pausado
        if (jogoPausado) return;

        rb.linearVelocity = new Vector2(
            horizontalInput * velocity,
            rb.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o personagem tocou em um objeto pertencente ao Layer do chão
        if ((chaoLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            // Verifica se o contato foi por cima, ou seja, o personagem pousou
            foreach (ContactPoint2D contato in collision.contacts)
            {
                if (contato.normal.y > 0.5f)
                {
                    pulosRestantes = 2;
                    break;
                }
            }
            anim.SetBool("Jump", false);

        }
    }

    // Funções do Pause
    public void PauseGame()
    {
        Time.timeScale = 0f;
        jogoPausado = true;

        if (menuPausePainel != null)
            menuPausePainel.SetActive(true);

        Debug.Log("Jogo Pausado");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        jogoPausado = false;

        if (menuPausePainel != null)
            menuPausePainel.SetActive(false);

        Debug.Log("Jogo Despausado");
    }
}