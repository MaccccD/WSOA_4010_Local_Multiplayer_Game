using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 3f); // Dumi: This script is attached to the particle effect prefab in the inspector bc each time an instatiation occurs, a copy of the original template
                             // and what we want is the effect to  play and the destroy as opposed to a constant bleeding episode.
                             // //This line ensures that the instances that are created also get destroyed when the duration of the blood splash effect is over 
    }
}
