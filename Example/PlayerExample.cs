using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerExample : MonoBehaviour
{
    [SerializeField] private InputActionAsset Map;
    [SerializeField] private InputActionReference Mov;
    [SerializeField] private CamaraExample camara;

    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform normal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 input = Mov.action.ReadValue<Vector2>();
        Vector3 movimiento = Vector3.zero;
        
        movimiento.z =  input.x;
        movimiento.x = -input.y;


        rb.linearVelocity = movimiento * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Zona zona)) 
        {
            camara.NuevaPosicionCamara(zona.positionCamera, zona.lerpTime, zona.modoCamara);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.TryGetComponent(out Zona zona))
    //    {
    //        camara.NuevaPosicionCamara(normal, zona.lerpTime);
    //    }
    //}
}
