using UnityEngine;

public class Movimento : MonoBehaviour
{
    private float horizontalInput;
    private Rigidbody2D rb;
    [SerializeField] private int velocity = 5; 

    [SerializeField] private Transform peDoPersonagem; 
    [SerializeField] private LayerMask chaoLayer;  

    [SerializeField] private GameObject menuPausePainel;

    private bool estaNoChao;
    private bool jogoPausado = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        // 1. Sempre verifica o Pause primeiro
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

        // 2. Bloqueia completamente o Update se o jogo estiver pausado
        if (jogoPausado) return;

        // 3. Comandos de movimentação só rodam se o jogo NÃO estiver pausado
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao) 
        {
            rb.AddForce(Vector2.up * 500);
        }

        estaNoChao = Physics2D.OverlapCircle(peDoPersonagem.position, 0.2f, chaoLayer); 
    }

    private void FixedUpdate() 
    {
        // Interrompe a movimentação física se o jogo estiver pausado
        if (jogoPausado) return;

        rb.linearVelocity = new Vector2(horizontalInput * velocity, rb.linearVelocity.y); 
    }

    // Funções públicas que você pode chamar tanto via teclado (ESC) quanto por botões na tela
    public void PauseGame()
    {
        Time.timeScale = 0f;
        jogoPausado = true;
        if (menuPausePainel != null) menuPausePainel.SetActive(true);
        Debug.Log("Jogo Pausado");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        jogoPausado = false;
        if (menuPausePainel != null) menuPausePainel.SetActive(false);
        Debug.Log("Jogo Despausado");
    }
}
