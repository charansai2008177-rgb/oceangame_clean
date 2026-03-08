using InventoryFramework;
using UnityEngine;

public class TreeResource : MonoBehaviour
{
    [Header("Items")]
    public Item woodItem;
    public int woodAmount = 3;

    [Header("Prefabs")]
    public GameObject fallingTreePrefab;

    [Header("UI")]
    public InteractionUi interactUI;

    [Header("Settings")]
    public float chopTime = 3f;
    public float respawnTime = 30f;

    private bool playerNearby = false;
    private bool treeChopped = false;

    private float timer = 0f;

    private Animator playerAnimator;
    private ItemPickupHandler playerInventory;

    private MeshRenderer treeRenderer;
    private Collider treeCollider;

    void Start()
    {
        treeRenderer = GetComponent<MeshRenderer>();
        treeCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (treeChopped) return;

        if (playerNearby)
        {
            interactUI.gameObject.SetActive(true);
        }

        if (playerNearby && Input.GetKey(KeyCode.E))
        {
            timer += Time.deltaTime;

            interactUI.UpdateFill(timer / chopTime);

            if (playerAnimator != null)
                playerAnimator.SetTrigger("chop");

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

    void ChopTree()
    {
        if (treeChopped) return;
        treeChopped = true;

        // 1. Hide the tree instantly
        treeRenderer.enabled = false;
        treeCollider.enabled = false;
        interactUI.gameObject.SetActive(false);

        // 2. Give the wood
        if (playerInventory != null)
        {
            playerInventory.PickupItem(woodItem, woodAmount);
        }


        // 4. Start the respawn timer
        Invoke(nameof(RespawnTree), respawnTime);
    }

    void HideTreeVisuals()
    {
        treeRenderer.enabled = false;
    }

    void RespawnTree()
    {
        // Reset everything for the next round
        treeRenderer.enabled = true;
        treeCollider.enabled = true;
        treeChopped = false;

        // Reset the animation to Idle
        Animator treeAnim = GetComponent<Animator>();
        treeAnim.Play("TreeIdle");
    }

    void ResetChop()
    {
        timer = 0f;
        interactUI.ResetFill();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe"))
        {
            // Tell the tree to shake!
            GetComponent<TreeShake>().TriggerShake();

            // Optional: Play a "thwack" sound effect here
        }
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