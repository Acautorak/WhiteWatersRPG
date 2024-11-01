using System;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TestSolve : MonoBehaviour
{

    ListNode CreateTestList1()
    {
        // Create nodes
        ListNode node1 = new ListNode(1);
        ListNode node2 = new ListNode(2);
        ListNode node3 = new ListNode(4);

        node1.next = node2;
        node2.next = node3;

        return node1;
    }
    ListNode CreateTestList2()
    {
        ListNode node1 = new ListNode(1);
        ListNode node2 = new ListNode(3);
        ListNode node3 = new ListNode(4);

        node1.next = node2;
        node2.next = node3;

        return node1;
    }

    public ListNode GetIntersectionNode(ListNode list1, ListNode list2)
    {
        ListNode pom1 = list1;
        ListNode pom2 = list2;

        if (list1 == null || list2 == null)
            return null;



        while (pom1 != pom2)
        {
            if (pom1 == pom2)
                return pom1;

            pom1 = (pom1.next == null) ? list2 : pom1.next;
            pom2 = (pom2.next == null) ? list1 : pom2.next;
        }


        return pom1;


    }


    void Start()
    {
        ListNode list1 = CreateTestList1();
        ListNode list2 = CreateTestList2();
        Debug.Log($"{GetIntersectionNode(list1, list2)}");
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