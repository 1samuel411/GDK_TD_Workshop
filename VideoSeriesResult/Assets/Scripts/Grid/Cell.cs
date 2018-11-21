using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public enum Status { unselected = 0, selected = 1, used = 2};
    public Status status;

    public Material selectedMaterial;
    public Material unSelectedMaterial;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {

    }

    void Update()
    {
        switch(status)
        {
            case Status.unselected:
                meshRenderer.enabled = true;
                meshRenderer.material = unSelectedMaterial;
                break;
            case Status.selected:
                meshRenderer.enabled = true;
                meshRenderer.material = selectedMaterial;
                break;
            case Status.used:
                meshRenderer.enabled = false;
                break;
        }
    }
}
