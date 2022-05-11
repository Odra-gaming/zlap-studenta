using UnityEngine;

public class Home : Behavior
{
    public Vector3 homePosition;
    public Vector3 exitPosition;

    private void OnEnable()
    {
        enemy.SetPosition(homePosition);
    }

    private void OnDisable()
    {
        enemy.SetPosition(exitPosition);
    }

}
