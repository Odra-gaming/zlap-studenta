using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        if (nextDirection != Vector2.zero)
        {

            SetDirection(nextDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("aaaaaaaaaaaaaaaaa");
            this.direction = -direction;
            nextDirection = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction //0.1875f
        float distance = 0.8f;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.14f, direction, distance, obstacleLayer);
        Debug.DrawRay(transform.position, direction*distance, Color.magenta, 3f, false);
        return hit.collider != null;
    }

}
