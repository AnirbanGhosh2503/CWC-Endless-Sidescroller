using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim; 
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier; // Adjust the global gravity
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false; // Prevent double jumping
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop(); // Stop the dirt particle effect when jumping
            playerAudio.PlayOneShot(jumpSound, 1.0f); 
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // Reset the ground state when colliding with the ground
            dirtParticle.Play(); // Play the dirt particle effect when on the ground
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true; // Set game over state when colliding with an obstacle
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true); // Trigger death animation
            playerAnim.SetInteger("DeathType_int", 1); // Randomly select a death animation
            explosionParticle.Play();
            dirtParticle.Stop(); // Stop the dirt particle effect when game over
            playerAudio.PlayOneShot(crashSound, 3.0f);
        }
    }
}
