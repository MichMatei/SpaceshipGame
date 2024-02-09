using UnityEngine;

public class CleanTheTrash : MonoBehaviour
{
    public GameObject theTrash;
    public Collider trashDispenser;
    public Collider playerCollider;

    bool inRange = false;
    bool trashPicked = false;

    bool cleanTrashMalfunction = false;
    bool ifStatementCanRunOnce = true;

    private void Start()
    {
        theTrash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TasksScript.tasksScriptInstance.cleanShipTaskActive && ifStatementCanRunOnce)
        {
            ifStatementCanRunOnce = false;

            theTrash.SetActive(true);
        }

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            theTrash.SetActive(false);
            TasksScript.tasksScriptInstance.eToInteract.fontSize = 0;
            trashPicked = true;
            TasksScript.tasksScriptInstance.staminaSlider.value -= 5f;
        }

        if (trashPicked && trashDispenser.bounds.Intersects(playerCollider.bounds))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TasksScript.tasksScriptInstance.taskText.text = "Clean Ship - Completed";
                TasksScript.tasksScriptInstance.staminaSlider.value -= 5f;
                trashPicked = false;

                ifStatementCanRunOnce = true;
                TasksScript.tasksScriptInstance.cleanShipTaskActive = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(theTrash.activeInHierarchy)
        {
            TasksScript.tasksScriptInstance.eToInteract.fontSize = 20;
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TasksScript.tasksScriptInstance.eToInteract.fontSize = 0;
        inRange = false;
    }
}
