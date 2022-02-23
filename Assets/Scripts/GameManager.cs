using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

  public GameObject enemigo, coleccionable;
  public Vector3 posicion;
  public int numeroEnemigos;
  public float esperaInicial;
  public float esperaEntreEnemigos;
  public float esperaEntreOlas, esperaEntreColeccionables;
  public int vidas = 3;
   public int muertos = 0;
   public string usuario;
   public bool isPausa;
   public bool isDisparo;

  void Start() {  
  vidas = 3;
  muertos = 90;
  usuario = "";
  isDisparo = false;

  
    //Busco el objeto llamado GameManager
    GameObject gameManager = GameObject.Find("GameManager");

    //Le indico que no se destruya al cargar otra escena 
    DontDestroyOnLoad(gameManager);

    //Cargo la escena de inicio
    cambiarEscena("Inicio");
   // SceneManager.LoadScene("Inicio");
    
    //LLamo a la rutina de crear enemigos
    StartCoroutine(crearEnemigos());

    //LLamo a la rutina de crear coleccionables
    StartCoroutine(crearColeccionables());
    
    //Le indico que no se destruya al cargar otra escena 
    DontDestroyOnLoad(gameManager);


   

    if (vidas<1)
    {
      //SceneManager.LoadScene("Inicio");
    }

    }
		
  IEnumerator crearEnemigos()
  {
    //Espero un tiempo antes de crear enemigos
    yield return new WaitForSeconds(esperaInicial);

    //Bucle durante toda la vida del juego
    while (true)
    {
      //Bucle de número de enemigos
      for (int i = 0; i < numeroEnemigos; i++)
      {
        //Instancio el enemigo en una posición aleatoria del tablero
        Vector3 posicionEnemigo = new Vector3(Random.Range(-posicion.x, posicion.x), posicion.y, Random.Range(-posicion.z, posicion.z));
        Quaternion rotacionEnemigo = Quaternion.identity;
        Instantiate(enemigo, posicionEnemigo, rotacionEnemigo);

        //Espero un tiempo entre la creación de cada enemigo
        yield return new WaitForSeconds(esperaEntreEnemigos);
      }

      //Espero un tiempo entre oleadas de enemigos
      yield return new WaitForSeconds(esperaEntreOlas);
    }
  }

  IEnumerator crearColeccionables()
  {
    yield return new WaitForSeconds(esperaInicial);
    while (true)
    {
      //Instancio el coleccionable en una posición aleatoria del tablero
      Vector3 posicionColeccionable = new Vector3(Random.Range(-posicion.x, posicion.x), posicion.y, Random.Range(-posicion.z, posicion.z));
      Quaternion rotacionColeccionable = Quaternion.identity;
      Instantiate(coleccionable, posicionColeccionable, rotacionColeccionable);

      //Espero un tiempo entre la creación de cada coleccionable
      yield return new WaitForSeconds(esperaEntreColeccionables);
    }

  }
  
  
  public void cambiarEscena(string nombreEscena){

    SceneManager.LoadScene(nombreEscena);
		
  }
  
  public void pausar()
  {
    if (!isPausa)
    {
      Time.timeScale = 0;
    }
    else
    {
      Time.timeScale = 1;
    }
    //cambio el valor del Booleano
    isPausa = !isPausa;
  }

}