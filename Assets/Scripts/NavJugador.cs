using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class NavJugador : MonoBehaviour
    {
        
        NavMeshAgent agente;

        void Start()
        {
            //Capturamos el componente agente del jugador
            agente = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(rayo, out hit, 100))
                {
                    agente.SetDestination(hit.point);
                }
            }
        }
    }
}