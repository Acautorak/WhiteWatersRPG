using System;
using UnityEngine;

public class TestSolve : MonoBehaviour
{
    string numString = "";
    int suma;


    public int Devide(int devidend, int devisor)
    {
        int k = 0;
        int pomocna = devidend;

        if (Math.Sign(devidend) == Math.Sign(devisor))
        {
            if (devidend > 0)
            {
                while (pomocna > 0)
                {
                    pomocna -= devisor;
                    k++;
                }
                k--;
            }
            else
            {
                while (pomocna < 0)
                {
                    pomocna -= devisor;
                    k++;
                }
                k--;
            }
        }
        else
        {
            if (devidend < 0)
            {
                while (pomocna < 0)
                {
                    pomocna += devisor;
                    k--;
                }
                k++;
            }
            else
            {
                while (pomocna > 0)
                {
                    pomocna += devisor;
                    k--;
                }
                k++;
            }
        }
        return k;
    }

    private void Start()
    {
        Debug.Log(Devide(7, 3).ToString());
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