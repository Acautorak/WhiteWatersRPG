using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TestSolve : MonoBehaviour
{

    ListNode CreateTestListWithCycle()
    {
        // Create nodes
        ListNode node1 = new ListNode(1);
        ListNode node2 = new ListNode(2);

        node1.next = node2;
        node2.next = node1;



        // Return the head of the list
        return node1;
    }

    public bool HasCycle(ListNode head)
    {
        if(head == null || head.next == null)
        return false;

        ListNode pom1 = head;
        ListNode pom2 = head.next;

        while(pom2.next != null)
        {
            pom1 = pom1.next;
            pom2 = pom2.next.next;

            if(pom1 == pom2)
            {
                return true;
            }
        }
        return false;
    }
   

    void Start()
    {
        Debug.Log($"{HasCycle(CreateTestListWithCycle())}");
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