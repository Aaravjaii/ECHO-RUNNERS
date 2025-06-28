using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float laneDistance = 2.5f;
    private int currentLane = 1;

    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isCrouching = false; // ✅ Track crouch state

    public TimingWindow timingWindow;
    public ScoreManager scoreManager;
    public RatingTextSpawner ratingSpawner;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++;
        }

        // ✅ Jump only if grounded and not crouching
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            string rating = timingWindow.GetRating();
            ratingSpawner.SpawnRating(rating);
            ApplyScore(rating);
        }

        // ✅ Handle crouch toggle
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            if (!isCrouching)
            {
                Crouch();
                isCrouching = true;

                string rating = timingWindow.GetRating();
                ratingSpawner.SpawnRating(rating);
                ApplyScore(rating);
            }
        }
        else
        {
            if (isCrouching)
            {
                StandUp();
                isCrouching = false;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, forwardSpeed);
        rb.angularVelocity = Vector3.zero;

        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, rb.position.y, rb.position.z);
        rb.MovePosition(Vector3.Lerp(rb.position, targetPosition, Time.fixedDeltaTime * 10f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit Obstacle!");

            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }

    private void Crouch()
    {
        transform.localScale = new Vector3(1f, 0.5f, 1f);
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, 0.5f, pos.z);
    }

    private void StandUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, 1f, pos.z);
    }

    private void ApplyScore(string rating)
    {
        if (rating == "Perfect")
        {
            scoreManager.AddScore(20);
        }
        else if (rating == "Good")
        {
            scoreManager.AddScore(10);
        }
        else
        {
            scoreManager.ResetCombo();
        }
    }
}
