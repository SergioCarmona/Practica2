using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPorTiempo : MonoBehaviour {

    void Start () {

        //Lo destruyo a los 10 segundos
        Destroy(gameObject, 10);

    }
}