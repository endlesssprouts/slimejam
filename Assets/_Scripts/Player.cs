using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private Animator m_Anim;
    private Rigidbody2D m_rb2d;

    private bool m_WalkingRight;
    private bool m_WalkingLeft;
    private float m_Height;

    private const float BASE_JUMP_MULTIPLIER = 100f;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private float m_JumpForce;

    [HideInInspector] public bool jump = false;

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_rb2d = GetComponent<Rigidbody2D>();
        m_Height = GetComponent<BoxCollider2D>().size.y;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        Vector2 pos = new Vector2(transform.position.x, transform.position.y - (m_Height / 2));
        Vector2 groundedRay = new Vector2(0, -0.1f);
        Debug.DrawRay(pos, groundedRay);
        if(Physics2D.Raycast(pos, groundedRay).collider != null)
            jump = false;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        m_Anim.SetFloat("H", h);
        if(h > 0.1 || h < -0.1)
            m_rb2d.velocity = new Vector2(h > 0.1 ? m_Speed : -m_Speed, m_rb2d.velocity.y);
        else
            m_rb2d.velocity = new Vector2(0, m_rb2d.velocity.y);

        if (jump)
        {
            m_Anim.SetTrigger("Jump");
            m_rb2d.AddForce(new Vector2(0f, BASE_JUMP_MULTIPLIER * m_JumpForce));
        }

    }
}
