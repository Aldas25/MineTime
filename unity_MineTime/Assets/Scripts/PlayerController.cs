using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public GameObject itemPrefab;

    private Rigidbody2D rb;
    private Vector2 movement;


    private GameObject itemToCollect;
    //private Item itemInHand;
    private int itemInHandId = 0;

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
        // TODO: animations!

        if (Input.GetKeyDown(KeyCode.E)) 
            CollectItem();
        if (Input.GetKeyDown(KeyCode.Q))
            DropItem();
        if (Input.GetMouseButtonDown(0))
            UseItem();

        float mouseScroll = Input.mouseScrollDelta.y;

        if (mouseScroll > 0) {
            itemInHandId++;
            if (itemInHandId >= inventory.size)
                itemInHandId -= inventory.size;
        } else if (mouseScroll < 0) {
            itemInHandId--;
            if (itemInHandId < 0)
                itemInHandId += inventory.size;
        }
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

    void CollectItem () {
        if (itemToCollect == null) 
            return;

        inventory.CollectItem (itemToCollect);
    }

    void DropItem () {
        //if (itemInHand == null) 
         //   return;
            
        //GameObject instantiatedItem = Instantiate (itemPrefab, transform.position, Quaternion.identity);
        //instantiatedItem.GetComponent<ItemObject> ().ChangeItem(itemInHand);
        //itemInHand = null;
    }

    void UseItem () {
        //if (itemInHand == null)
         //   return;

        
    }
}
