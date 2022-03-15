using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class MyComponent
{
    //[SerializeField] float floatRV;
    public float floatRV;
    [SerializeField] protected int intRV = 1; //there are different types of protection level, you have to specify 
    [SerializeField] string stringRV = "Abc"; //the default without specifying it might be "internal"
}
