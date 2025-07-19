using UnityEngine;

public class Roxo : MonoBehaviour
{
    PlayerController playerController;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()

    {

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }



    // Update is called once per frame

    void Update()

    {



    }



    private void OnTriggerEnter2D(Collider2D col)

    {

        if (col.CompareTag("Player"))

        {

            playerController.AddRoxo(1);

            Destroy(gameObject);

        }

    }
}
