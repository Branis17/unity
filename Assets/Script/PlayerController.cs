using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    private CharacterController controller;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        if (animator == null)
            Debug.LogError("? Animator non trouvé sur le joueur ou ses enfants !");
        if (controller == null)
            Debug.LogError("? CharacterController manquant sur le joueur !");
    }

    void Update()
    {
        if (animator == null || controller == null)
            return; // Stoppe l'exécution si un composant est manquant

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;


        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
            transform.forward = direction;

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
