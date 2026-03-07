using InventoryFramework;
using UnityEngine;

public class TreeResource : MonoBehaviour
{
    public Item woodItem;
    public int woodAmount = 3;
    private Animator playerAnimator;
    public GameObject Player;

    public ChopUI chopUI;
    public float chopTime = 3f;

    public float respawnTime = 30f;   // time for tree to grow again

    private bool playerNearby = false;
    private float timer = 0f;

    private bool treeChopped = false;
    private bool chopping = false;

    private ItemPickupHandler playerInventory;

    private Rigidbody rb;
    private Collider treeCollider;
    private MeshRenderer treeRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        treeCollider = GetComponent<Collider>();
        treeRenderer = GetComponent<MeshRenderer>();

        rb.isKinematic = true;
    }

    void Update()
    {
        if (treeChopped) return;

        if (playerNearby && Input.GetKey(KeyCode.E))
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("chop");
            }

            chopping = true;

            timer += Time.deltaTime;

            chopUI.gameObject.SetActive(true);
            chopUI.UpdateBar(timer / chopTime);

            if (timer >= chopTime)
            {
                ChopTree();
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            ResetChop();
        }
    }

    void ResetChop()
    {
        timer = 0f;
        chopping = false;
        chopUI.gameObject.SetActive(false);
    }

    void ChopTree()
    {
        treeChopped = true;

        ResetChop();

        if (playerInventory != null)
        {
            playerInventory.PickupItem(woodItem, woodAmount);
        }

        // Enable physics for falling tree
        rb.isKinematic = false;

        rb.AddForce(transform.right * 500f, ForceMode.Impulse);
        rb.AddTorque(Vector3.forward * 200f, ForceMode.Impulse);

        // disable interaction
        treeCollider.enabled = false;

        // start respawn
        Invoke(nameof(RespawnTree), respawnTime);
    }

    void RespawnTree()
    {
        // hide tree while respawning
        treeRenderer.enabled = false;

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.identity;

        Invoke(nameof(EnableTree), 2f);
    }

    void EnableTree()
    {
        treeRenderer.enabled = true;
        treeCollider.enabled = true;

        treeChopped = false;
        timer = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            playerInventory = other.GetComponent<ItemPickupHandler>();

            playerAnimator = other.GetComponent<Animator>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            ResetChop();
        }
    }
}
