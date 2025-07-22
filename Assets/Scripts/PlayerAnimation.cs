using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animation anim;
    private bool isSliding = false;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animation>();

        if (anim != null && anim.GetClip("Run") != null)
        {
            anim.Play("Run");
        }
    }

    void Update()
    {
        if (isDead || anim == null) return;

        // Handle sliding
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            if (!isSliding && anim.GetClip("Runtoslide") != null)
            {
                anim.CrossFade("Runtoslide");
                isSliding = true;
            }
        }
        else
        {
            if (isSliding)
            {
                anim.CrossFade("Run");
                isSliding = false;
            }
        }
    }

    // Optional: Call this when the player dies
    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim.GetClip("Dizzy") != null)
        {
            anim.CrossFade("Dizzy");
        }
    }
}
