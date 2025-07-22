using UnityEngine;

public class LizardAutoRunner : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Animator animator;

    void Start()
    {
        // Play the walk/run animation
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("Lizard@run");  // 🟡 Replace with actual animation name if different
        }
    }

    void Update()
    {
        // Move forward along Z axis
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
