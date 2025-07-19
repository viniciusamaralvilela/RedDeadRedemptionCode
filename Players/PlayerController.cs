using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject npc;
    public float Speed = 2f;
    public float jumpForce = 0.5f;
    public Rigidbody2D rig;

    private int jumpsRemaining = 2;
    DialogueSystem dialogueSystem;
    public int laranja = 0;
    public int Verde = 0;
    public int Roxo = 0;
    public int ovo = 0;
    public TextMeshProUGUI WINTEXT;
    Animator anim;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask chaolayer;
    private bool estanochao;
    float tempoUltimoPulo = 0f;
    float delayPulo = 0.8f; // Delay mínimo entre ativações

    void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.freezeRotation = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // Movimento
        transform.position += new Vector3(moveInput, 0f, 0f) * Time.deltaTime * Speed;

        // Detecção de chão
        estanochao = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, chaolayer);
        if (estanochao) jumpsRemaining = 2;

        // Pulo
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0f);
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpsRemaining--;
        }

        // Animação e giro
        if (moveInput > 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (moveInput < 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (Time.time - tempoUltimoPulo > delayPulo)
        {
            anim.SetBool("pulo", !estanochao);

            // Se acabou de sair do chão (iniciou o pulo), atualiza o tempo
            if (!estanochao)
            {
                tempoUltimoPulo = Time.time;
            }
        }

        // Diálogo com NPC
        if (Vector3.Distance(transform.position, npc.transform.position) < 2f && Input.GetKeyDown(KeyCode.E))
        {
            dialogueSystem.Next();
        }

        // Verificação de vitória
        if (Vector3.Distance(transform.position, npc.transform.position) < 2f &&
            laranja == 1 && ovo == 1 && Verde == 1 && Roxo == 1)
        {
            WINTEXT.gameObject.SetActive(true);
        }
    }

    // Métodos públicos para coletáveis
    public void AddLaranja(int value) => laranja += value;
    public void AddVerde(int value) => Verde += value;
    public void AddRoxo(int value) => Roxo += value;
    public void AddOvo(int value) => ovo += value;
}
