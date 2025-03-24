using UnityEngine;
using System;

public class SwordCollision : MonoBehaviour
{
    //Eden: define static event that sends the hit players index (0 for p1, 1 for p2)
    //Eden: HealthManager script is then able to use this info to do things
    public static event Action<int, Vector3> OnPlayerHitEvent; //Eden: added this Vector3 to track pos ofplayer

    //Eden: These keep track of which player the sword is attached to 
    private string swordAttachedPlayerName;
    private int swordAttachedPlayerIndex;

    private void Start()
    {
        //Eden: bcs the sword id a child of each player I can get the name and index
        if (transform.parent != null)
        {
            swordAttachedPlayerName = transform.parent.gameObject.name;
            string[] parts = swordAttachedPlayerName.Split('_');
            if (parts.Length > 1)
            {
                int.TryParse(parts[1], out swordAttachedPlayerIndex);
            }
        }
    }

    //Eden: Detects collisions with triggers
    private void OnTriggerEnter(Collider other)
    {
        // Eden: Checks if collided object has tag UpperBody, if so then
        if (other.CompareTag("UpperBody"))
        {
            //Eden: get parent of the upper body that was hit (this is the player that has been hit)
            GameObject hitPlayer = other.transform.parent.gameObject;

            //Eden: this ignores collisions with the players own sword
            if (hitPlayer.name != swordAttachedPlayerName)
            {
                int hitPlayerIndex = -1;
                string[] parts = hitPlayer.name.Split('_');
                if (parts.Length > 1)
                {
                    int.TryParse(parts[1], out hitPlayerIndex);
                }

                //Eden: for valid hits
                if (hitPlayerIndex != -1)
                {
                    //Eden: just some debugs to log the hits
                    if (hitPlayerIndex == 1)
                    {
                        Debug.Log("PLAYER 2 hit");
                    }
                    else if (hitPlayerIndex == 0)
                    {
                        Debug.Log("PLAYER 1 hit");
                    }

                    Vector3 hitPosition = other.bounds.center;
                    Debug.Log("Hit Position: " + hitPosition);

                    //Eden: now we broadcast the hit event so that HealthManager can react
                    OnPlayerHitEvent?.Invoke(hitPlayerIndex, hitPlayer.transform.position);
                }
            }
        }
    }
}
