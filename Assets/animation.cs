using UnityEngine;

public class animation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null)
                animator.SetTrigger("chop");
        }
    }
}
