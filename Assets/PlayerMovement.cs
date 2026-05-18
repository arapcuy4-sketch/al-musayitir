using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    [Header("Movement")]
    public float speed = 4f;
    public float gravity = -15f;
    public float jumpHeight = 1.2f;

    [Header("Audio")]
    public AudioClip footstepSound;
    private AudioSource audioSource;

    Vector3 velocity;
    bool isGrounded;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (controller == null || cam == null) return;

        // =====================
        // GROUND CHECK
        // =====================
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // =====================
        // INPUT
        // =====================
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // =====================
        // ANIMATION
        // =====================
        float moveAmount = direction.magnitude;

        if (anim != null)
        {
            anim.SetFloat("Speed", moveAmount, 0.1f, Time.deltaTime);
        }

        // =====================
        // MOVEMENT + ROTATION
        // =====================
        if (moveAmount >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime
            );

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            // 🔊 FOOTSTEP PLAY
            if (isGrounded && footstepSound != null)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = footstepSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
        }
        else
        {
            // STOP SOUND kalau diam
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // =====================
        // JUMP
        // =====================
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (anim != null)
                anim.SetTrigger("Jump");
        }

        // =====================
        // GRAVITY
        // =====================
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}