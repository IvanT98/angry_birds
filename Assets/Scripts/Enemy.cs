using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float CollisionContactPointThreshold = -0.5f;

    [SerializeField] private GameObject onDestroyParticles;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var collider = col.collider;
        var bird = collider.GetComponent<Bird>();
        var enemy = collider.GetComponent<Enemy>();
        var contactPoint2Ds = col.contacts;

        if (enemy)
        {
            return;
        }
        
        if (!bird)
        {
            if (contactPoint2Ds.Length <= 0 || (contactPoint2Ds.Length > 0 &&
                                                contactPoint2Ds[0].normal.y >= CollisionContactPointThreshold))
            {
                return;
            }
        }

        var enemyPosition = transform.position;
        var deathParticles = Instantiate(onDestroyParticles, enemyPosition, Quaternion.identity);
        
        Destroy(gameObject);
        // TODO: Wait for particles to play before destroying them.
        Destroy(deathParticles);
    }
}
