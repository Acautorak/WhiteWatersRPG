using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSolve : MonoBehaviour
{
    public struct Friend
    {
        public int arrival;
        public int leaving;
        public int chair;

        public Friend(int arrival, int leaving, int chair)
        {
            this.arrival = arrival;
            this.leaving = leaving;
            this.chair = chair;
        }
    }

    public int [][] times = new int[][] {new int[] {1,2}, new int[] {2,3}};

    public Dictionary<int, Friend> friendsMap = new Dictionary<int, Friend>();
    public Queue<int> chairsQueue = new Queue<int>();

    public int SmallestChair(int[][] times, int targetFriend)
    {
        for (int i = 0; i < times.Length; i++)  
        {
            for (int j = 0; j < times[i].Length; j++) 
            {
               // Debug.Log($"Element at times[{i},{j}] = {times[i][j]}");
            }
        }
        CreateMapFriends(friendsMap);
        return 0;
    }

   

    public int ReturnTargetChair(int targetFriend)
    {
        return friendsMap[targetFriend].chair;
    }


    public List<int> FindSmallestLargerTime(Dictionary<int, Friend> friends, int pomTime)
    {
        List<int> smallestLargerFriend = new List<int>(); 
        int smallestTime = int.MaxValue;     

        foreach (KeyValuePair<int, Friend> kvp in friends)
        {
            if (kvp.Value.arrival > pomTime && kvp.Value.arrival <= smallestTime)
            {
                smallestLargerFriend.Add(kvp.Key);
                smallestTime = kvp.Value.arrival;
            }

            if (kvp.Value.leaving > pomTime && kvp.Value.leaving <= smallestTime)
            {
                smallestLargerFriend.Add(kvp.Key);
                smallestTime = kvp.Value.leaving;
            }
        }

        return smallestLargerFriend;
    }

   /*  public void ChangeChairsInMapFriends(List<int> smallestLargerFriend, int time)
    {
        int freeChair = 0;
        int pomTime = ;

        foreach (int element in smallestLargerFriend)
        {
            if(friendsMap(element).Value
        }
    } */


    public void CreateMapFriends(Dictionary<int,Friend> friends)
    {
        int pom = 0;
        foreach(int[] element in times)
        {
            friends.Add(pom, new Friend(element[0], element[1], -1));
            Debug.Log($"Key: {pom}: Value {friends[pom].leaving}");
            pom++;
        }
        
    }


    void Start()
    {
       SmallestChair(times, 1);
       
    }
}
