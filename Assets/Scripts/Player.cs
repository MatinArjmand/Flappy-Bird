using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float gravity = -15f;
    public float strength = 5.2f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.1f, 0.1f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void OnDisable()
    {
        Pipes.speed = 5f;
        BackGroundParallax.animationSpeed = 0.2f;
        Parallax.animationSpeed = 0.83f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.up * strength;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        if (FindObjectOfType<GameManager>().GetScore() % 3 == 0)
        {
            float temp = Pipes.speed;
            Pipes.speed += 0.0018f;
            BackGroundParallax.animationSpeed *= Pipes.speed / temp;
            Parallax.animationSpeed *= Pipes.speed / temp;
        }
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().gameOver();
        } else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
