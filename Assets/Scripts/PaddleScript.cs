using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public Vector2 Direction;
    public float MaxSpeed = 1.0f;
    float AccelVal = 0.0f;
    public Transform NormalObj;
    public float RotateMultiplier = 0;
    Vector2 PostitionAtStart;
    public float Speed = 5.0f;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float Coeff = Time.deltaTime * Speed; ;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            AccelVal += Coeff;
            RotateMultiplier += Time.deltaTime * 10;

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            AccelVal -= Coeff;
            RotateMultiplier -= Time.deltaTime * 10;

        }
        else
        {
            AccelVal = Mathf.Lerp(AccelVal, 0, Coeff);
            if(Mathf.Abs(RotateMultiplier ) > 0.1f)
            {
                RotateMultiplier = Mathf.Lerp(RotateMultiplier, 0, Time.deltaTime * 10);
            }
            else
            {
                RotateMultiplier = 0.0f;
            }
        }
        AccelVal = Mathf.Clamp(AccelVal, -MaxSpeed, MaxSpeed);
        transform.position = new Vector2(PostitionAtStart.x, transform.position.y + (AccelVal * Time.deltaTime));
        transform.rotation = Quaternion.Euler(0, 0, RotateMultiplier);

    }

    public void SetInitialPosition()
    {
        Vector2 NewPos = new Vector2(GameManager.Instance.TopRight.x, 0);
        NewPos -= Vector2.right * transform.localScale.x;
        transform.position = NewPos;
        PostitionAtStart = NewPos;

    }
}
