using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{ 
    Rigidbody rb;
    [SerializeField] float thrust = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnMouseDown()
    {
        rb.AddForce(-transform.right * thrust, ForceMode.Impulse);
    }
}
