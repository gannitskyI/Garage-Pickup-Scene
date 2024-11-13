using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Collider coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        if (coll == null) coll = gameObject.AddComponent<Collider>();
    }

    public void OnPickup(Transform attachPoint)
    {
        rb.isKinematic = true; 
        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
    }

    public void OnDrop()
    {
        rb.isKinematic = false;      
        transform.SetParent(null);
    }
}
