using UnityEngine;

public class Frightened : Behavior
{
    public SimpleAnimation basic;
    public SimpleAnimation scawed;
    public bool eaten { get; private set; }

    public override void Disable()
    {
        base.Disable();

    }

    private void Eaten()
    {
        eaten = true;
        enemy.SetPosition(enemy.home.homePosition);
        enemy.home.Enable(20);//duration

    }

    private void OnEnable()
    {
        basic.enabled = false;
        scawed.enabled = true;
        enemy.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        basic.enabled = true;
        scawed.enabled = false;
        enemy.movement.speedMultiplier = 1f;
        eaten = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // If the distance in this direction is greater than the current
                // max distance then this direction becomes the new farthest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (enemy.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            enemy.movement.SetDirection(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (enabled)
            {
                Eaten();
            }
        }
    }

}
