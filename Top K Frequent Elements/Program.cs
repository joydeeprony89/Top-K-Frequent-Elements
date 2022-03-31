using System;
using System.Collections.Generic;

namespace Top_K_Frequent_Elements
{
  class Program
  {
    static void Main(string[] args)
    {
      Program p = new Program();
      var nums = new int[] { 1,0,1,2 };
      var result = p.TopKFrequent(nums, 2);
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

    public int[] TopKFrequent(int[] nums, int k)
    {
      if (nums == null || nums.Length == 0 || k == 0) return new int[0];
      // Create the frequency for each nos.
      var countMap = GetItemFrequency(nums);
      // Create the maxHeap with max count as k
      var maxHeap = GetMaxHeap(countMap, k);
      var res = new List<int>();
      foreach(var item in maxHeap)
      {
        res.Add(item.Value);
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
