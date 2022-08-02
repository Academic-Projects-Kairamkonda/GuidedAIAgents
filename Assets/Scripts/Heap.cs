using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap <T> where T: IHeapItem<T>
{
    /// <summary>
    /// items to hold for a heap
    /// </summary>
    T[] items;

    /// <summary>
    /// keeps track of the item count
    /// </summary>
    int currentItemCount;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="maxHeapSize">creates new heap</param>
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    /// <summary>
    /// add the item to the current count
    /// </summary>
    /// <param name="item">node</param>
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    /// <summary>
    /// removes the first node
    /// </summary>
    /// <returns>first item in node</returns>
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);

        return firstItem;
    }

    /// <summary>
    /// Updates Item in the list
    /// </summary>
    /// <param name="item"></param>
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    /// <summary>
    /// property to current count
    /// </summary>
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    /// <summary>
    /// check the item in the List
    /// </summary>
    /// <param name="item">neighbour node</param>
    /// <returns></returns>
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    /// <summary>
    /// sort the node, helps to remove the first item
    /// </summary>
    /// <param name="item">heap index</param>
    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if(childIndexLeft<currentItemCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight<currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight])<0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex])<0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// sort up the items, helps to adds and update item
    /// </summary>
    /// <param name="item"></param>
    void SortUp(T item)
    {
        int parentIndex=(item.HeapIndex-1)/2;
        while (true)
        {
            T parentItem = items[parentIndex];

            if(item.CompareTo(parentItem)>0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }


    /// <summary>
    /// Swaps the two items in the node
    /// </summary>
    /// <param name="itemA">first index</param>
    /// <param name="itemB">second index </param>
    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

/// <summary>
/// Interface with type T 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IHeapItem<T>: IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}