using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapulta : MonoBehaviour
{
    HingeJoint2D giroCatapulta;
    Rigidbody2D catapultaBody;
    Vector3 posicionCatapulta;
    public Transform salidaBellotas;
    public GameObject bellota;
    public GameObject paredDerecha;
    public GameObject puntoSalidaCamara;
    public Camera camara;
    GameObject bellotaNueva;
    Vector3 posicionBellota;
    

    // Start is called before the first frame update
    void Start()
    {
        giroCatapulta = GetComponent<HingeJoint2D>();
        catapultaBody = GetComponent<Rigidbody2D>();
        giroCatapulta.useLimits = true;
        posicionBellota = transform.position;
    }
    
    private void OnMouseDown()
    {
        catapultaBody.AddTorque(20000.0f);

        bellotaNueva = (GameObject)Instantiate(bellota, salidaBellotas.position, transform.rotation);
        volviendo = false;
    }

    private void OnMouseUp()
    {
        giroCatapulta.useMotor = false;
        catapultaBody.AddTorque( -100000.0f);
    }
    void OnMouseDrag()
    {
        var puntoClicEnPantalla = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        puntoClicEnPantalla.z = 0;

    }

 
    

    private bool volviendo = false;

    void FixedUpdate()
    {
        float step = 10 * Time.deltaTime;
        if (bellotaNueva)
        {
            if (!volviendo)
            {
                camara.transform.position = new Vector3(bellotaNueva.transform.position.x, camara.transform.position.y, -2); //z a -2 para que se vea, no sé por qué
            }
            if (camara.transform.position.x > puntoSalidaCamara.transform.position.x) { 
                if (bellotaNueva.transform.position.x > paredDerecha.transform.position.x) {
                    camara.transform.position = Vector3.MoveTowards(camara.transform.position,puntoSalidaCamara.transform.position, step);
                    volviendo = true;
                }
                Rigidbody2D bodyBellota = bellotaNueva.GetComponent<Rigidbody2D>();
                if (bodyBellota.velocity.magnitude <= 0.1) {
                    camara.transform.position = Vector3.MoveTowards(camara.transform.position, puntoSalidaCamara.transform.position, step);
                    volviendo = true;
                }
            }
        }

    }



}
