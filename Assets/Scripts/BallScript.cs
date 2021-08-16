using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallScript : MonoBehaviour
{
    public float speed = 1;
    public Vector2 Direction;

    public AudioSource audioSource;
    public AudioClip HitClip;
    public AudioClip LoseClip;
    public AudioClip WallHitClip;
    public AudioClip WinClip;

    public bool IsPaused = true;

    // Start is called before the first frame update
    void Start()
    {
        Direction = new Vector2(Random.Range(50, 100) * RandomSign(), Random.Range(10, 25) * RandomSign());
        Direction.Normalize();
        InvokeRepeating("SpeedIncrease", 5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPaused == false)
        {
            transform.Translate(Direction * speed * Time.deltaTime);
        }
    }

    void SpeedIncrease()
    {
        speed += 0.5f;
    }

    int RandomSign()
    {
        int val = Random.value > 0.5 ? 1 : -1;

        return val;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(DOTween.IsTweening(transform) == false)
        {
            transform.GetChild(0).DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.5f);
        }

        if (other.name == "Bottom")
        {
            audioSource.PlayOneShot(WallHitClip);

            Direction = Vector2.Reflect(Direction, Vector2.up);

        }
        else if (other.name == "Top")
        {
            audioSource.PlayOneShot(WallHitClip);

            Direction = Vector2.Reflect(Direction, Vector2.down);
        }
        else if (other.name == "Paddle")
        {
            audioSource.PlayOneShot(HitClip);
            //Direction = Vector2.Reflect(Direction, Vector2.left);
            Vector2 CurrentNormal = new Vector2();
            CurrentNormal = GameManager.Instance.PaddleObj.GetComponent<PaddleScript>().NormalObj.right;
            Direction = Vector2.Reflect(Direction, CurrentNormal);
            
        }
        else if (other.name == "AI_Paddle")
        {
            audioSource.PlayOneShot(HitClip);
            GameManager.Instance.IncreaseAILevel();
            Vector2 NormalToReflect = Vector2.right.Rotate(Random.Range(-5.0f, 5.0f));//adds some randomeness to AI
            NormalToReflect.Normalize();
            Direction = Vector2.Reflect(Direction, NormalToReflect);
        }
        else if (other.name == "Left")
        {
            audioSource.PlayOneShot(WinClip);
            GameManager.Instance.YouWin();
        }
        else if (other.name == "Right")
        {
            audioSource.PlayOneShot(LoseClip);
            GameManager.Instance.YouLose();
        }

        Direction.Normalize();
    }

}

public static class Vector2Extension{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
