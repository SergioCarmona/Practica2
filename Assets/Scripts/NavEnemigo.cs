using UnityEngine;
using UnityEngine.AI;

public class NavEnemigo : MonoBehaviour {

    NavMeshAgent agente;
    GameObject jugador;
    private MeshRenderer meshRenderer;
    public int vidas;

    //Variable para el script de GameManager
    private GameManager scriptGameManager;
    void Start () {
            
        

        scriptGameManager = GameObject.FindObjectOfType<GameManager>();

        //Busco el jugador
        jugador = GameObject.Find("Jugador");

        //Capturamos en nav mesh agent del enemigo
        agente = GetComponent<NavMeshAgent>();

        //Capturamos el SkinnedMeshRenderer del children del enemigo para cambiarle el material
        meshRenderer = GetComponent<MeshRenderer>();

    }

    void Update () {

        //Muevo el enemigo hacia el jugador (si no lo han matado aún)
        if (jugador != null)
        {
            if (jugador.GetComponent<Jugador>().huir)
            {
                //Huye del jugador
                agente.SetDestination(transform.position - jugador.transform.position);
                //Color temporal
                meshRenderer.material.color = Color.blue;
            }
            else
            {
                //Persigue al jugador
                agente.SetDestination(jugador.transform.position);
                //Color original
                meshRenderer.material.color = Color.red;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Trampa2")
        {
           

            Destroy(gameObject);
        }

        //Si se choca con el jugador
        if (other.gameObject.tag == "Jugador")
        {
            if (jugador.GetComponent<Jugador>().huir)
            {
                
                Destroy(gameObject);
                
            }
            else
            {
                
                quitarVida();
                
                //Paro el tiempo del juego para que no se creen más enemigos
                Time.timeScale = 0;
            }
        }


    }
    
    
    void quitarVida(){

        //Resto una vida
        vidas--;
        //Actualizo el contador de vidas
        
    }
    
    
}