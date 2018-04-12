using UnityEngine;

public class Player : MonoBehaviour
{
    private bool m_WalkingRight;
    private bool m_WalkingLeft;
    private bool m_Jumping; 
    private Animator m_anim;
    private Rigidbody2D m_rb2d;

    private const float BASE_JUMP_MULTIPLIER = 100f;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private float m_JumpForce;

    [SerializeField]
    private float m_MaxSpeed;

    [HideInInspector] public bool jump = false;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        //TODO smarter anim transition params and state variables for automatic animation selection

        //if(m_Jumping) // This should be done with a collider that is triggered when slime is in contact with ground
        //    return;



        float h = Input.GetAxis("Horizontal");

        if (h * m_rb2d.velocity.x < m_MaxSpeed)
            m_rb2d.AddForce(Vector2.right * h * m_Speed);

        if (Mathf.Abs(m_rb2d.velocity.x) > m_MaxSpeed)
            m_rb2d.velocity = new Vector2(Mathf.Sign(m_rb2d.velocity.x) * m_MaxSpeed, m_rb2d.velocity.y);

        if (h > 0)
        {
            m_WalkingRight = true;
            m_WalkingLeft = false;
            m_anim.SetBool("WalkRight", true);
            m_anim.SetBool("WalkLeft", false);

        }
        else if (h < 0)
        {
            m_WalkingLeft = true;
            m_WalkingRight = false;
            m_anim.SetBool("WalkLeft", true);
            m_anim.SetBool("WalkRight", false);
        }
        else
        {
            m_WalkingRight = false;
            m_WalkingLeft = false;
            m_anim.SetBool("WalkLeft", false);
            m_anim.SetBool("WalkRight", false);
        }

        if (jump)
        {
            m_anim.SetTrigger("Jump");
            m_rb2d.AddForce(new Vector2(0f, BASE_JUMP_MULTIPLIER * m_JumpForce));
            jump = false;
            m_Jumping = true;
            //Invoke("JumpFalse", 1.5f);
        }

    }

    //private void JumpFalse()
    //{
    //    m_Jumping = false;
    //}
}
