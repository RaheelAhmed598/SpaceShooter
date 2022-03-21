using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//collecteable powerup of fireshot
public class PowerUp : MonoBehaviour
{
    
    void Start()
    {
        Invoke("HidePowerUp", 10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        //player get the collectable powerup  through trigger
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerController>().bullets_PowerUp = true;
            this.gameObject.SetActive(false);
        }
    }

    //at certain time hide power
    public void HidePowerUp()
    {
        this.gameObject.SetActive(false);
    }
}
