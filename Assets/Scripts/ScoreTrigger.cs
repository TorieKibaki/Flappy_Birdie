
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bird>() != null)
        {
            GameManager.Instance.AddScore(1);

            Destroy(gameObject);
        }
    }
}
