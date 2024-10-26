using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isLastCheckpoint;

    public GameObject LevelCompletedUI;
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>() != null)
        {
            if (isLastCheckpoint)
            {
                GameManager.instance.OnLevelComplete();
            }
        }
    }
}
