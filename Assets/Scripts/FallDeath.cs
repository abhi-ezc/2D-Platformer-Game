using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public Vector3 SpawnPosition;

    public GameObject GameOverUI;
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
            Destroy(col.gameObject);
            GameOverUI.SetActive(true);
        }
    }
}
