using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    public EnemyMovement movement { get; private set; }
    public Home home { get; private set; }
    public Scatter scatter { get; private set; }
    public Chase chase { get; private set; }
    public Frightened frightened { get; private set; }
    public Behavior initialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        home = GetComponent<Home>();
        scatter = GetComponent<Scatter>();
        chase = GetComponent<Chase>();
        frightened = GetComponent<Frightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        //frightened.Disable();
        //chase.Disable();
        //scatter.Enable();

        if (home != initialBehavior)
        {
            //home.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("aaa");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("bbb");
            if (frightened.enabled)
            {
                FindObjectOfType<GameManager>().EnemyPowered(this);
            }
            else
            {
                
                FindObjectOfType<GameManager>().PlayerLost();
            }
        }
    }

}
