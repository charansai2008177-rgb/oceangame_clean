using InventoryFramework;
using UnityEngine;

public class TreeResource : MonoBehaviour
{
    [Header("Items")]
    public Item woodItem;
    public int woodAmount = 3;

    [Header("References")]
    public GameObject Player;
    public InteractionUi interactUI;
    private Animator playerAnimator;
    private ItemPickupHandler playerInventory;

    [Header("Settings")]
    public float chopTime = 3f;
    public float respawnTime = 30f;
    public float timeBeforeDisappear = 5f;

    private bool playerNearby = false;
    private float timer = 0f;
    private bool treeChopped = false;

    private Rigidbody rb;
    private Collider treeCollider;
    private MeshRenderer[] treeRenderers;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        treeCollider = GetComponent<Collider>();
        // Get all renderers in case the tree has multiple parts (leaves/trunk)
        treeRenderers = GetComponentsInChildren<MeshRenderer>();

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        SetupPhysics();
    }

    void SetupPhysics()
    {
        rb.isKinematic = true;
        rb.useGravity = true;
        // This forces the "weight" to the bottom so it doesn't sink as easily
        rb.centerOfMass = new Vector3(0, -2f, 0);
        // Prevents the tree from sliding around like ice
        rb.linearDamping = 1f;
        rb.angularDamping = 1f;
    }

    void Update()
    {
        if (treeChopped) return;

        if (playerNearby)
        {
            interactUI.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                if (playerAnimator != null) playerAnimator.SetTrigger("chop");

                timer += Time.deltaTime;
                interactUI.UpdateFill(timer / chopTime);

                if (timer >= chopTime) ChopTree();
            }
        }

        if (Input.GetKeyUp(KeyCode.E)) ResetChop();
    }

    void ResetChop()
    {
        timer = 0f;
        interactUI.ResetFill();
    }

    void ChopTree()
    {
        if (treeChopped) return;

        treeChopped = true;
        interactUI.gameObject.SetActive(false);

        // 1. Activate Physics
        rb.isKinematic = false;

        // 2. Calculate Direction
        Vector3 fallDirection = (transform.position - Player.transform.position).normalized;

        // 3. APPLY FORCE AT THE TOP (Leverage)
        // We push the top of the tree (Vector3.up * 5) so it tips over the base
        Vector3 forcePoint = transform.position + Vector3.up * 5f;
        rb.AddForceAtPosition(fallDirection * 1f, forcePoint, ForceMode.Impulse);

        // 4. Inventory
        if (playerInventory != null) playerInventory.PickupItem(woodItem, woodAmount);

        // 5. Cleanup
        Invoke(nameof(HideTree), timeBeforeDisappear);
        Invoke(nameof(RespawnTree), respawnTime);
    }

    void HideTree()
    {
        foreach (var renderer in treeRenderers) renderer.enabled = false;
        treeCollider.enabled = false;
        rb.isKinematic = true;
    }

    void RespawnTree()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        treeChopped = false;
        timer = 0f;

        foreach (var renderer in treeRenderers) renderer.enabled = true;
        treeCollider.enabled = true;
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
            interactUI.gameObject.SetActive(false);
            ResetChop();
        }
    }
}