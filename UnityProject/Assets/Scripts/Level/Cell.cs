using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    
    // Enum
    public enum Status
    {
        unselected, selected, used
    }

    // Public Variables
    public Material unselectedMaterial;
    public Material selectedMaterial;


    // Private Variables
    private MeshRenderer meshRenderer;
    private Status status;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void UpdateStatus()
    {
        switch(status)
        {
            case Status.unselected:
                meshRenderer.enabled = true;
                meshRenderer.material = unselectedMaterial;
                break;
            case Status.selected:
                meshRenderer.enabled = true;
                meshRenderer.material = selectedMaterial;

                AudioManager.instance.PlayAudio(AudioManager.PlayType.SilentEffects, "CellChange");
                break;
            case Status.used:
                meshRenderer.enabled = false;
                break;
        }
    }

    public void SetStatus(Status newStatus, bool overrideUsed = false)
    {
        if (status == Status.used && !overrideUsed)
            return;

        status = newStatus;
        UpdateStatus();
    }

    public bool IsUsed()
    {
        return status == Status.used;
    }
}
