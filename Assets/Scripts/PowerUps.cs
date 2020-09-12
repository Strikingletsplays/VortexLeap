using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private BallController _ballController;

    // Start is called before the first frame update
    void Start()
    {
        _ballController = FindObjectOfType<BallController>();
    }
    IEnumerator SpeedPowerUp()
    {
        //Set super speed active
        _ballController.PowerupSuperSpeed = true;
        yield return new WaitForSeconds(2);
        //Set super speed to false
        _ballController.PowerupSuperSpeed = false;
        Destroy(gameObject);
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SpeedPowerUp());
            GetComponent<MeshRenderer>().enabled = false;
        }
            
    }
    private void OnEnable()
    {
        if (CompareTag("SpeedPowerUp"))
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
