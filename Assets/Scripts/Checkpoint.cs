using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isLastCheckpoint = false;

    public GameObject LevelCompletedUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>() != null)
        {
            if (isLastCheckpoint)
            {
                Debug.Log("Level Completed");
                LevelCompletedUI.SetActive(true);
            }
        }
    }
}
