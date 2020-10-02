using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public GameObject itemPrefab;
    public Animator animator;
    public Transform spriteTransform;

    private Rigidbody2D rb;
    private Vector2 movement;


    private GameObject itemToCollect;
    //private Item itemInHand;
    private int itemInHandId = 0;
    public ItemObject itemInHand;

    private GameManager gameManager;
    private Inventory inventory;

    void Awake () {
        rb = gameObject.GetComponent<Rigidbody2D>  ();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager> ();
        inventory = gameObject.GetComponent<Inventory> ();
    }

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetBool ("Horizontal", movement.x!=0);
        animator.SetBool ("Vertical", movement.y != 0);
        if ((movement.x < 0 && spriteTransform.localScale.x > 0) || (movement.x > 0 && spriteTransform.localScale.x < 0))
            spriteTransform.localScale = new Vector2(-spriteTransform.localScale.x, spriteTransform.localScale.y);

        if (Input.GetKeyDown(KeyCode.E)) 
            CollectItem();
        if (Input.GetKeyDown(KeyCode.Q))
            DropItem();
        if (Input.GetMouseButtonDown(0))
            UseItem();

        float mouseScroll = Input.mouseScrollDelta.y;

        if (mouseScroll > 0) 
            ScrollRight ();
        else if (mouseScroll < 0) 
            ScrollLeft ();
    }

    void FixedUpdate () {
        Move();
    }

    void Move () {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Item")) {
            if (itemToCollect == null)
                itemToCollect = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Item")) {
            if (itemToCollect == col.gameObject)
                itemToCollect = null;
        }
    }

    void ScrollRight () {
        itemInHandId++;
        if (itemInHandId >= inventory.size)
            itemInHandId -= inventory.size;
        UpdateItemInHand ();
    }

    void ScrollLeft () {
        itemInHandId--;
        if (itemInHandId < 0)
            itemInHandId += inventory.size;
        UpdateItemInHand (); 
    }

    void CollectItem () {
        if (itemToCollect == null) 
            return;

        inventory.CollectItem (itemToCollect);
        UpdateItemInHand (); 
    }

    void DropItem () {
        if (itemInHand.item == null) 
            return;
            
        GameObject instantiatedItem = Instantiate (itemPrefab, transform.position, Quaternion.identity);
        instantiatedItem.GetComponent<ItemObject> ().ChangeItem(itemInHand.item);
        inventory.RemoveItem(itemInHandId);
        UpdateItemInHand ();
    }

    void UseItem () {
        itemInHand.UseItem ();
        UpdateItemInHand ();
    }

    void UpdateItemInHand () {
        Item newItem = inventory.GetItem(itemInHandId);

        if (itemInHand.item == newItem)
            return;

        itemInHand.ChangeItem(newItem);
    }
}
