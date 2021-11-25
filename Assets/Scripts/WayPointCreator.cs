using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointCreator : MonoBehaviour
{
    public GameObject waypoint;
    CarMove carro;

    bool criar = false;

    private void Start()
    {
        carro = GetComponent<CarMove>();
    }

    IEnumerator colocar()
    {
        yield return new WaitForSeconds(0.6f);
        GameObject way = Instantiate(waypoint, transform.position, Quaternion.identity);
        way.GetComponent<Waypoint>().velocidadeRecomendada = carro.VelKM;
        if (criar)
        {
            StartCoroutine(colocar());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            criar = !criar;

            if (criar)
            {
                StartCoroutine(colocar());
            }
        }
    }
}
