using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    [SerializeField] public GameObject dialogPrefab;
    [SerializeField] public GameObject mainCanvas;

    private bool actionAxisInUse = true;
    private GameObject player;
    private bool dialogIsInitiated = false;
    private DialogText currentDialog;
    private DialogDisplayer currentDisplayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        ProcessInput();
    }

    public void StartDialog(DialogText newDialog)
    {
        dialogIsInitiated = true;
        player.GetComponent<PlayerMovement>().DisableControl();
        currentDialog = newDialog;
        GameObject currentDialogObject = Instantiate(dialogPrefab, mainCanvas.transform);
        currentDisplayer = currentDialogObject.GetComponent<DialogDisplayer>();
        currentDisplayer.SetDialogText(currentDialog.GetDialogText());
    }

    public void ProcessInput()
    {
        if (ShouldProcessInput())
        {
            actionAxisInUse = true;
            if (currentDialog.IsNextDialog())
            {
                currentDialog = currentDialog.GetNextDialog();
                currentDisplayer.SetDialogText(currentDialog.GetDialogText());
            }
            else
            {
                EndDialog();
            }
        }
    }

    public void EndDialog()
    {
        dialogIsInitiated = false;
        currentDisplayer.CloseDialog();
        player.GetComponent<PlayerMovement>().EnableControl();
        currentDialog = null;
    }

    private bool ShouldProcessInput()
    {
        if (dialogIsInitiated)
        {
            if(!actionAxisInUse && Input.GetAxis("Jump") != 0)
            {
                return true;
            }

        }
        return false;
    }

    private void ValideAxisInUse()
    {
        if(Input.GetAxis("Jump") != 0)
        {
            actionAxisInUse = true;
        }
        else
        {
            actionAxisInUse = false;
        }
    }

}
