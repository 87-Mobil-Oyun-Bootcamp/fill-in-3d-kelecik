using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterControl : MonoBehaviour
{
    bool determinCheck;
    [SerializeField]
    Animator printerAnim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cube"))
        {
            CubeSpawner.instance.EnableMeshRenderer(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            determinCheck = true;
        }
    }
    private void FixedUpdate()
    {
        PrinterAnim();
    }
    private void Start()
    {
        Invoke("Activate", 4f);
    }

    void Activate()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void PrinterAnim()
    {
        printerAnim.SetBool("determinCheck", determinCheck);
    }

    void CloseDeterminCheck()
    {
        determinCheck = false;
    }

}
