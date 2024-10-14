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
    public Dictionary<int, Friend> friends = new Dictionary<int, Friend>();

    public int SmallestChair(int[][] times, int targetFriend)
    {
        for (int i = 0; i < times.Length; i++)  
        {
            for (int j = 0; j < times[i].Length; j++) 
            {
               // Debug.Log($"Element at times[{i},{j}] = {times[i][j]}");
            }
        }
        MapChairs(times);
        CreateMapFriends(friends);
        return 0;
    }

    public Dictionary<int, int> MapChairs(int [][] times)
    {
        Dictionary<int, int> map = new Dictionary<int,int>();

        for (int i = 0; i < times.Length; i++)  
        {   
            map[i] = -1;       
        }

        foreach (KeyValuePair<int,int> kvp in map)
        {
           // Debug.Log($"Key: {kvp.Key}: Value {kvp.Value}");
        }

        return map;
    }

    public int ReturnTargetChair(int targetFriend)
    {
        return friends[targetFriend].chair;
    }

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
