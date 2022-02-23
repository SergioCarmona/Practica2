using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador : MonoBehaviour {

    public float velocidad;
    public bool huir = false;
    private float tiempo;
    private Rigidbody rb;
    GameObject jugador;
    
  
    //Variable para el número de vidas


//Variable para asociar el objeto Texto Vidas
   

    private GameObject Enemigo;

    
    

//variable para la posición inicial del jugador
    Vector3 posicionInicial;
    
    //Variable para el script de GameManager
    private GameManager scriptGameManager;

    
    //CAJA DE TEXTO PARA EL USUARIO
    [SerializeField] private Text textoVidas;
    [SerializeField] private Text textoMuertos;
    [SerializeField] private Text textoTiempo2;

    
    
    void Start () {
        
       
        scriptGameManager = GameObject.FindObjectOfType<GameManager>();

        Enemigo = GameObject.Find("Enemigo");
        jugador = GameObject.Find("Jugador");
        //Capturo el rigidbody del jugador al iniciar el juego
        rb = GetComponent<Rigidbody>();
        //Inicializo el texto del contador de vidas

        textoVidas.text = "Vidas: " + scriptGameManager.vidas;
        textoMuertos.text = "Mataos: " + scriptGameManager.muertos;
        textoTiempo2.text = " ";
        


//Capturo la posición inicial del jugador para cuando pierda una vida poder reposicionarlo
        posicionInicial = transform.position;

    }
    void FixedUpdate () {

        textoMuertos.text = "Mataos: " + scriptGameManager.muertos;
        
        //Capturo el movimiento en los ejes
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        
        //Genero el vector de movimiento
        Vector3 movimiento = new Vector3(movimientoH, 0, movimientoV);

        //Muevo el jugador
        transform.position += movimiento * velocidad;
        
        if (movimiento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimiento), 0.15f);
        }

        //Si los enemigos están huyendo y no se ha acabado el tiempo, decremento el tiempo
        if (huir && tiempo > 0)
        {

            tiempo -= Time.deltaTime;
            //Lo muestro en consola
            Debug.Log(tiempo);
            textoTiempo2.text = "Tiempo: "+tiempo;
            
            
            
            
        }
        else
        {
            huir = false;
            textoTiempo2.text = "";
            
        }
        if (scriptGameManager.vidas == 0){
  

    
            
            //FINALIZA EL JUEGO
            if (isTiempo)
            {
                StartCoroutine(Finalizar());
            }
           
            //Pongo isTiempo a false para que deje de contar el script Tiempo
            isTiempo = false;
            
           
        }
        
        
        
        
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Si atraviesa con el coleccionable
        if (other.gameObject.CompareTag("Coleccionable"))
        {

 
            //Borro el coleccionable
            other.gameObject.SetActive(false);

            
            //Inicio el contador hacia atrás y pongo a true el booleano
            tiempo = 10;
            
            huir = true;
            
           
        }
        
        if (other.gameObject.tag == "Trampa2")
        {
   
            quitarVida();
                
            //Paro el tiempo del juego para que no se creen más enemigos
            Time.timeScale = 0;
            //Capturo un array con todos los objetos que tengan la etiqueta enemigo
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

            //Recorro ese array y los destruyo
            foreach (GameObject enemigo in enemigos)
            {
                Destroy(enemigo);
            }
            Time.timeScale = 1;
         
        }
        
        
        //Si se choca con el jugador
        if (other.gameObject.tag == "Enemigo")
        {
            if (jugador.GetComponent<Jugador>().huir)
            {
 
             Destroy(other.gameObject);
            matar();
            

            }
            else
            {
                
                quitarVida();
                
                //Paro el tiempo del juego para que no se creen más enemigos
                Time.timeScale = 0;
                //Capturo un array con todos los objetos que tengan la etiqueta enemigo
                GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

                //Recorro ese array y los destruyo
                foreach (GameObject enemigo in enemigos)
                {
                    Destroy(enemigo);
                }
                Time.timeScale = 1;
                
                
            }
        }
        
    }
    
    /* Añadir en la declaración de variables de JugadorController */

//Variable para comprobar si debo seguir incrementando el tiempo transcurrido
    public bool isTiempo = true;

    
    void quitarVida(){

        //Resto una vida
        scriptGameManager.vidas--;
        //Actualizo el contador de vidas
        textoVidas.text = "Vidas: " + scriptGameManager.vidas;
        //Devuelvo el Jugador a su posición inicial y le quito la fuerza
        transform.position = posicionInicial;
        rb.velocity = Vector3.zero;
    }

    void matar()
    {
        scriptGameManager.muertos++;
        textoMuertos.text = "Muertos: " + scriptGameManager.muertos;
    }
    
    private IEnumerator Finalizar()
    {
        //genera una espera de 5 segundos
        yield return new WaitForSeconds(0);
        //Cambia a la escena de creditos
        scriptGameManager.cambiarEscena("Fin");
        scriptGameManager.vidas = 2;

        
    }

}