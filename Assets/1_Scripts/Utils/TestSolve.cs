using System;
using System.Collections.Generic;
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

    public ListNode MergeTwoLists(ListNode list1, ListNode list2) 
    {
        ListNode start = new ListNode(-1);
        ListNode pom = start;

        while(list1 != null && list2 != null)
        {
            if(list1.val <= list2.val)
            {
                
                pom.next = list1;
                list1 = list1.next;
            }
            else
            {
                pom.next = list2;
                list2 = list2.next;
            }

            pom = pom.next;
        }   

        pom.next = list1 ?? list2;

        return start.next;   
    }


    void Start()
    {
        ListNode list1 = CreateTestList1();
        ListNode list2 = CreateTestList2();
        Debug.Log($"{MergeTwoLists(list1, list2)}");
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