using System;
using System.Collections.Generic;

namespace Top_K_Frequent_Elements
{
  class Program
  {
    static void Main(string[] args)
    {
      Program p = new Program();
      var nums = new int[] { 3, 0, 1, 0 };
      var result = p.TopKFrequent(nums, 1);
      Console.WriteLine(string.Join(",", result));
    }

    public class Node
    {
      public int Value { get; set; }
      public int Count { get; set; }
      public Node(int val, int count)
      {
        Value = val;
        Count = count;
      }
    }

    public class NodeComparer : IComparer<Node>
    {
      public int Compare(Node x, Node y)
      {
        // When the count are same we need to compare on the value
        return x.Count == y.Count
              ? Comparer<int>.Default.Compare(x.Value, y.Value)
              : Comparer<int>.Default.Compare(x.Count, y.Count);

      }
    }

    public class Max : IComparer<int>
    {
      public int Compare(int a, int b)
      {
        if (b > a) return 1;
        else if (b == a) return 0;
        else return -1;
      }
    }

    public int[] TopKFrequent(int[] nums, int k)
    {
      if (nums == null || nums.Length == 0 || k == 0) return new int[0];
      // Create the frequency for each nos.
      var countMap = GetItemFrequency(nums);
      PriorityQueue<int, int> pq = new PriorityQueue<int, int>(new Max());
      foreach(var kvp in countMap)
      {
        pq.Enqueue(kvp.Key, kvp.Value);
      }
      var res = new List<int>();
      while (k-- > 0)
      {
        res.Add(pq.Dequeue());
      }
      return res.ToArray();
    }

    private Dictionary<int, int> GetItemFrequency(int[] nums)
    {
      Dictionary<int, int> countMap = new Dictionary<int, int>();

      foreach(int n in nums)
      {
        if (!countMap.ContainsKey(n))
          countMap[n] = 0;
        countMap[n]++;
      }

      return countMap;
    }

    private SortedSet<Node> GetMaxHeap(Dictionary<int, int> countMap, int k)
    {
      SortedSet<Node> result = new SortedSet<Node>(new NodeComparer());
      foreach(var item in countMap)
      {
        // add an item into the sortedSet
        result.Add(new Node(item.Key, item.Value));
        // When the heap count is gt K, remove the min element from the heap.
        // This heap will always have the max count element of k size, which has asked in the question to return the k most frequest element
        if (result.Count > k) result.Remove(result.Min);
      }

      return result;
    }
  }
}
