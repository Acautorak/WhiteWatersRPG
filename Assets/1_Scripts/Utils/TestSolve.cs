using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TestSolve : MonoBehaviour
{
    string numString = "";
    int suma;

    private int GetLucky(string s, int k)
    {
        foreach (char c in s)
        {
            int num = c - 96;
            numString += num.ToString();
        }
        

        
        foreach(char c in numString)
        {
            suma += (c - '0');
        }

        for (int i = 0; i<k; i++)
        {

        }

        return suma;
    }



    private void Start()
    {
        Debug.Log(GetLucky("aa", 1));
    }
}

public class ListNode
{
    public int val;
    public ListNode next;

    public ListNode(int val)
    {
        this.val = val;
    }
}