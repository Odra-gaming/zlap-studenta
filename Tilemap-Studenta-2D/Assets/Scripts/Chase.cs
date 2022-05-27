using UnityEngine;

public class Chase : Behavior
{
    public float chaseSpeedMultiplier = 1f;

    private void OnEnable()
    {
        enemy.movement.speedMultiplier = chaseSpeedMultiplier;
    }

    private void OnDisable()
    {
        enemy.movement.speedMultiplier = 1f;
        enemy.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Do nothing while the enemy is frightened
        if (node != null && enabled && !enemy.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            // Find the available direction that moves closet to pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // If the distance in this direction is less than the current
                // min distance then this direction becomes the new closest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (enemy.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            enemy.movement.SetDirection(direction);
        }
    }

}
