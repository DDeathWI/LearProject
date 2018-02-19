using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Testing : MonoBehaviour {

    delegate void MyDelegate(int num);
    MyDelegate myDelegate;


    void Start()
    {
        myDelegate = PrintNum;


        myDelegate += DoubleNum;

        myDelegate(50);
    }

    void PrintNum(int num)
    {
        print("Print Num: " + num);
    }

    void DoubleNum(int num)
    {
        print("Double Num: " + num * 2);
    }


}
