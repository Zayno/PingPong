using UnityEngine;

public class ScreenColliderManager : MonoBehaviour
{

    GameObject top;
    GameObject bottom;
    GameObject left;
    GameObject right;


    void Awake()
    {
        top = new GameObject("Top");
        bottom = new GameObject("Bottom");
        left = new GameObject("Left");
        right = new GameObject("Right");

        top.AddComponent<Rigidbody2D>();
        bottom.AddComponent<Rigidbody2D>();
        left.AddComponent<Rigidbody2D>();
        right.AddComponent<Rigidbody2D>();

        top.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        bottom.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        left.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        right.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;



    }


    void Start()
    {
        CreateScreenColliders();
    }


    void CreateScreenColliders()
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        //// Create top collider
        BoxCollider2D collider = top.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);

        top.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, topRightScreenPoint.y, 0f);
        top.GetComponent<BoxCollider2D>().isTrigger = true;
        //top.AddComponent<Rigidbody2D>();


        // Create bottom collider
        collider = bottom.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);

        //** Bottom needs to account for collider size
        bottom.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, bottomLeftScreenPoint.y - collider.size.y, 0f);
        bottom.GetComponent<BoxCollider2D>().isTrigger = true;
        //bottom.AddComponent<Rigidbody2D>();




        // Create left collider
        collider = left.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);

        //** Left needs to account for collider size
        left.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - collider.size.x, bottomLeftScreenPoint.y, 0f);
        left.GetComponent<BoxCollider2D>().isTrigger = true;
        //left.AddComponent<Rigidbody2D>();



        // Create right collider
        collider = right.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);

        right.transform.position = new Vector3(topRightScreenPoint.x, bottomLeftScreenPoint.y, 0f);
        right.GetComponent<BoxCollider2D>().isTrigger = true;
        //right.AddComponent<Rigidbody2D>();


    }
}