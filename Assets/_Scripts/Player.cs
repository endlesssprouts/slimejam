using UnityEngine;

public class Player : MonoBehaviour
{
    private bool m_WalkingRight;
    private bool m_WalkingLeft;
    private bool m_Jumping;

    private const float BASE_JUMP_MULTIPLIER = 100f;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private float m_JumpForce;

    private void Update()
    {
        //TODO smarter anim transition params and state variables for automatic animation selection

        if(m_Jumping)
            return;

        if(!m_WalkingRight && Input.GetKeyDown(KeyCode.D))
        {
            m_WalkingRight = true;
            m_WalkingLeft = false;
            GetComponent<Animator>().SetBool("WalkLeft", false);
            GetComponent<Animator>().SetBool("WalkRight", true);
        }
        else if(Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * Time.deltaTime * m_Speed;

        else if(!m_WalkingLeft && Input.GetKeyDown(KeyCode.A))
        {
            m_WalkingLeft = true;
            m_WalkingRight = false;
            GetComponent<Animator>().SetBool("WalkRight", false);
            GetComponent<Animator>().SetBool("WalkLeft", true);
        }
        else if(Input.GetKey(KeyCode.A))
            transform.position -= Vector3.right * Time.deltaTime * m_Speed;
        else
        {
            m_WalkingRight = false;
            m_WalkingLeft = false;
            GetComponent<Animator>().SetBool("WalkRight", false);
            GetComponent<Animator>().SetBool("WalkLeft", false);
        }

        if((m_WalkingRight || m_WalkingLeft) && Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dir = Vector2.up + (m_WalkingRight ? Vector2.right : Vector2.left);
            Vector2 force = dir * BASE_JUMP_MULTIPLIER * m_JumpForce;
            GetComponent<Rigidbody2D>().AddForce(force);
            GetComponent<Animator>().SetTrigger("Jump");
            m_Jumping = true;
            Invoke("JumpFalse", 1.5f);
        }
    }

    private void JumpFalse()
    {
        m_Jumping = false;
    }
}
