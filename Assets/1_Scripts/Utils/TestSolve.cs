using System;
using UnityEngine;

public class TestSolve : MonoBehaviour
{

    public int Search(int[] nums, int target)
    {
        int leftIndex = 0;
        int rightIndex = nums.Length - 1;
        int midIndex = 0;

        while (leftIndex <= rightIndex)
        {
            midIndex = (leftIndex + (rightIndex - leftIndex)) / 2;

            if(nums[midIndex] == target) return midIndex;

            if(nums[leftIndex] <= nums[midIndex])
            return target;

        }

            return midIndex;
    }




    private void Start()
    {
        int[] nums = { };
        Debug.Log(Search(nums, 1).ToString());
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