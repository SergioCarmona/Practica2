using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {

        //Si se choca con el jugador
        if (other.gameObject.tag == "Trampa")
        {
            //Destruyo al jugador
            Destroy(other.gameObject);
        }
    }
}
