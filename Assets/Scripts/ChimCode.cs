using UnityEngine;
using UnityEngine.InputSystem;

public class ChimCode : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    Animator anim;

    public AudioClip flapSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            (Keyboard.current.spaceKey.wasPressedThisFrame ||
            Keyboard.current.upArrowKey.wasPressedThisFrame ||
            Mouse.current.leftButton.wasPressedThisFrame)
            && birdIsAlive)
        {
            myRigidBody.linearVelocity = Vector2.up * flapStrength;
            anim.SetBool("isFlying", true);

            AudioSource.PlayClipAtPoint(
            flapSound,
            Camera.main.transform.position
            );
        }
        else
        {
            anim.SetBool("isFlying", false);
        }

        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathBorder"))
        {
            logic.gameOver();
        }
    }
}
