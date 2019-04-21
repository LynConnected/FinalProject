using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactNormal : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private NormalGameController ngameController;

    void Start()
    {
        GameObject ngameControllerObject = GameObject.FindWithTag("NormalGameController");
        if (ngameControllerObject != null)
        {
            ngameController = ngameControllerObject.GetComponent<NormalGameController>();
        }
        if (ngameController == null)
        {
            Debug.Log("Cannot find 'NormalGameController' script");
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
            ngameController.GameOver();
        }

        ngameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}