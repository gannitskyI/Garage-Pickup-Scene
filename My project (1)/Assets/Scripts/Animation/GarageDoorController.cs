using UnityEngine;
using DG.Tweening;

public class GarageDoorController : MonoBehaviour
{ 
    [SerializeField] private Transform leftDoor; 
    [SerializeField] private Transform rightDoor; 
    [SerializeField] private float openAngle = 90f; 
    [SerializeField] private float openDuration = 1f;  

    private Vector3 leftDoorClosedRotation;
    private Vector3 rightDoorClosedRotation;

    private void Start()
    { 
        leftDoorClosedRotation = leftDoor.eulerAngles;
        rightDoorClosedRotation = rightDoor.eulerAngles;
 
        OpenDoors();
    }

    private void OpenDoors()
    { 
        Vector3 leftDoorOpenRotation = leftDoorClosedRotation + new Vector3(0, openAngle, 0);
        Vector3 rightDoorOpenRotation = rightDoorClosedRotation - new Vector3(0, openAngle, 0);
 
        leftDoor.DORotate(leftDoorOpenRotation, openDuration).SetEase(Ease.OutQuad);
        rightDoor.DORotate(rightDoorOpenRotation, openDuration).SetEase(Ease.OutQuad);
    }

    public void CloseDoors()
    {
        leftDoor.DORotate(leftDoorClosedRotation, openDuration).SetEase(Ease.OutQuad);
        rightDoor.DORotate(rightDoorClosedRotation, openDuration).SetEase(Ease.OutQuad);
    }
}
