using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PaddleScript : MonoBehaviour
{
    public float Speed = 1.0f;
    public Vector2 Direction;
    float PaddleHeight = 1.0f;

    public GameObject BallObj;

    void Start()
    {
        PaddleHeight = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 BallPos = BallObj.transform.position;
        if (BallPos.y > transform.position.y)
        {
            Direction = Vector2.up;
        }
        else
        {
            Direction = Vector2.down;
        }

        float yPos = transform.position.y;
        float PaddleHalfHeight = PaddleHeight / 2.0f;
        if (yPos - PaddleHalfHeight < (GameManager.Instance.BottomLeft.y))
        {
            transform.position = new Vector3(transform.position.x, GameManager.Instance.BottomLeft.y + PaddleHalfHeight);
        }
        else if (yPos + PaddleHalfHeight > (GameManager.Instance.TopRight.y))
        {
            transform.position = new Vector3(transform.position.x, GameManager.Instance.TopRight.y - PaddleHalfHeight);
        }

        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    public void SetInitialPosition()
    {
        Vector2 NewPos = new Vector2(GameManager.Instance.BottomLeft.x, 0);
        NewPos += Vector2.right * transform.localScale.x;
        transform.position = NewPos;
    }
}
