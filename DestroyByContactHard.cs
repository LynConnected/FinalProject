using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactHard : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private HardGameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("HardGameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<HardGameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'HardGameController' script");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}