using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class CamaraExample : MonoBehaviour
{
    private delegate void comportamientoCamara();
    private comportamientoCamara movimientoCamara; 

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 positionOriginal;
    [SerializeField] private Quaternion rotationOriginal;

    [SerializeField] private Vector3 vector;

    public enum seguimiento 
    {
        Vacio,
        Panomerica,
        Truck
    }


    //[SerializeField] private float lerpTime;

    private void Start()
    {
        positionOriginal = transform.position;
        rotationOriginal = transform.rotation;
        movimientoCamara = Truck;
    }

    private void Update()
    {
        movimientoCamara();
    }

    private void Panoramica() 
    {
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }

    private void Truck() 
    {
        transform.position = Quaternion.Euler(21,-90,0) * vector + target.transform.position;
    }

    public void NuevaPosicionCamara(Transform newPosition, float lerpTime, seguimiento control = seguimiento.Truck)
    {
        StartCoroutine(LerpCamara(newPosition, lerpTime, control));
    }

    IEnumerator LerpCamara(Transform newPosition, float lerpTime, seguimiento control)
    {
        movimientoCamara = (control == seguimiento.Panomerica) ? Panoramica : Vacio;

        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        for (float t = 0; t < 1; t += Time.deltaTime / lerpTime)
        {
            transform.position = Vector3.Lerp(currentPosition, newPosition.position, t);
            transform.rotation = Quaternion.Lerp(currentRotation, newPosition.rotation, t);

            yield return new WaitForEndOfFrame();
        }

        ChangetModoCamara(control);
    }

    private void ChangetModoCamara(seguimiento control) 
    {

        switch (control)
        {
            case seguimiento.Panomerica:
                movimientoCamara = Panoramica;
                break;

            case seguimiento.Truck:
                movimientoCamara = Truck;
                break;

            case seguimiento.Vacio:
                movimientoCamara = Vacio;
                break;
        }
    }
    
    private void Vacio() { }
}
