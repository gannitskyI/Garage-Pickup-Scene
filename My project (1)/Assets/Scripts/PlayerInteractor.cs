using UnityEngine; 
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float pickupRange = 8f;
    [SerializeField] private float holdSmoothness = 20f;
    [SerializeField] private float throwForce = 4f;
    [SerializeField] private float verticalOffset = 0.5f;

    private PickupableObject heldObject;
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject != null) DropObject();
            else TryPickupObject();
        }
    }

    private void FixedUpdate()
    {
        if (heldObject == null) return;

        Vector3 targetPosition = holdPoint.position + playerCamera.transform.forward * verticalOffset;
        float offset = Mathf.Clamp(Mathf.Cos(playerCamera.transform.eulerAngles.x * Mathf.Deg2Rad), -1f, 1f) * verticalOffset;
        targetPosition += playerCamera.transform.up * offset;

        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, holdSmoothness * Time.deltaTime);
        heldObject.transform.rotation = Quaternion.Slerp(heldObject.transform.rotation, playerCamera.transform.rotation, holdSmoothness * Time.deltaTime);
    }

    private void TryPickupObject()
    {
        if (!Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, pickupRange)) return;

        PickupableObject pickupObject = hit.transform.GetComponent<PickupableObject>();
        if (pickupObject == null) return;

        pickupObject.OnPickup(holdPoint);
        heldObject = pickupObject;
    }

    private void DropObject()
    {
        if (heldObject == null) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);
        }

        heldObject.OnDrop();
        heldObject = null;
    }
}
