using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    bool openDoor = false;
    bool closeDoor = false;

    public GameObject door;

    Vector3 doorOpen;
    Vector3 doorClosed;

    private void Start()
    {
        doorOpen = door.transform.position + Vector3.up * 3.5f;
        doorClosed = door.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (openDoor)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, doorOpen, 2f * Time.deltaTime);
        }

        if (closeDoor)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, doorClosed, 1f * Time.deltaTime);

            if (Vector3.Distance(door.transform.position,doorClosed) < .1)
            {
                door.transform.position = doorClosed;
                closeDoor = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        openDoor = true;
        closeDoor = false;
    }

    private void OnTriggerExit(Collider other)
    {
        openDoor = false;
        closeDoor = true;
    }
}
