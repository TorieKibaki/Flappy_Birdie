using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered) return;

        if (other.GetComponent<Bird>() != null)
        {
            isTriggered = true;
            GameManager.Instance.AddScore(1);
        }
    }
}