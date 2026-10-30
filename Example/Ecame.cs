using UnityEngine;

public class Ecame : MonoBehaviour
{
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] float t;
    [SerializeField] float time;

    private void Update()
    {
        t += Time.deltaTime / time;
        transform.position = Vector3.Lerp(a.position, b.position, t);
        transform.rotation = Quaternion.Lerp(a.rotation, b.rotation, t);
    }
}
