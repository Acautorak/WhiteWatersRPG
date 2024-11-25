using System;
using UnityEngine;

public class TestSolve : MonoBehaviour
{

    public bool Search(int[] nums, int target)
    {
        int leftIndex = 0;
        int rightIndex = nums.Length - 1;
        int midIndex = 0;

        while (leftIndex <= rightIndex)
        {
            if(nums[leftIndex] == target || nums[rightIndex] == target) return true;
            midIndex = (leftIndex + (rightIndex - leftIndex)) / 2;
            Debug.Log("mid indeks je: " + midIndex); 

            if(nums[midIndex] == target) return true;

            if(nums[leftIndex] <= nums[midIndex]) //levi je sortiran
            {
                if(nums[leftIndex] <= target && target < nums[midIndex]) 
                    rightIndex = midIndex - 1; //levi je sortiran i broj je u tom ospegu, uradi petlju
                else
                {
                leftIndex = midIndex +1;  // levi je sortiran i target nije u  opsegu, predji na desni
                Debug.Log(leftIndex.ToString());
                }
            }
            else //levi nije sortiran i
            {

                if(nums[midIndex +1] == target) return true;
                if(nums[rightIndex] == target) return true;

                if(nums[midIndex +1] < nums[rightIndex]) // desni niz je sortiran
                {
                    if(nums[midIndex] < target && target < nums[rightIndex]) // target je u desnom podnizu
                    {
                        leftIndex = midIndex +1;
                    }
                    else // vrati se na levi podniz
                    {
                    rightIndex = midIndex -1;
                    }
                }
                else // desni nije sortiran 
                {
                    leftIndex ++;
                    rightIndex --;
                }
            }
            
            
            return false;

        }

            return false;
    }


    private void Start()
    {
        int[] nums = {2,3,0,1};
        Debug.Log(Search(nums, 0).ToString());
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