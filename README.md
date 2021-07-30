# PriorityQueue

Priority queue implementations in C#

## Table of contents

* [Features](#features)
* [Instalation](#instalation)
* [Example usage](#example-usage)
* [Implementation comparison](#implementation-comparison)
* [Time complexity](#time-complexity)
* [Sources](#sources)

## Features

* Three default implementations of the priority queue data structure
* Easy to use with a custom comparer
* Easy way to create your own implementation by implementing the `IPriorityQueue` interface

## Instalation

`Install-Package PriorityQueues`

More info: [nuget package](https://www.nuget.org/packages/PriorityQueues/)

## Example usage

First we'll define a sample class that which will be used in our queue.
```cs
class Element
{
    public string Name { get; set; }
    public float Priority { get; set; }
}
```

Then we can create a new priority queue using one of the implementations, and pass in a comparer
```cs
// Create a new instance of BinaryHeapPriorityQueue
IPriorityQueue<Element> myPriorityQueue = new BinaryPriorityQueue<Element>((a, b) => a.Priority.CompareTo(b.Priority)); // this will produce a min-heap, use b.Priority.CompareTo(a.Priority) for a max-heap
// Insert some elements
myPriorityQueue.Enqueue(new Element { Priority = 5, Name = "A" });
myPriorityQueue.Enqueue(new Element { Priority = 7, Name = "B" });
myPriorityQueue.Enqueue(new Element { Priority = 4, Name = "C" });
// Get the top element (one with the highest priority value) and remove it
myPriorityQueue.Dequeue(); // Name: "B", Priority: 7
// Get the top element's value without removing it
myPriorityQueue.Peek(); // Name: "A", Priority: 5;
// Get the top element again, this time it will be removed
myPriorityQueue.Dequeue(); // Name: "A", Priority: 5
// Clear all remaining elements
myPriorityQueue.Clear(); 
myPriorityQueue.IsEmpty(); // true
```

For more information on usage, check the Tests project.

## Implementation comparison

There are three default implementations. `BinaryPriorityQueue` and `MappedBinaryPriorityQueue` both use a [binary heap](https://en.wikipedia.org/wiki/Binary_heap) as their underyling data structure, but the latter also stores all elements in a dictionary for faster lookup and element removal at the expense of slight memory and computational overhead. `FibonacciPriorityQueue` uses a [fibonacci heap](https://en.wikipedia.org/wiki/Fibonacci_heap) for faster amortized enqueueing and dequeueing times.

## Time complexity

|Operation|BinaryPriorityQueue|MappedBinaryPriorityQueue|FibonacciPriorityQueue|
|---|---|---|---|
|Peek|O(1)|O(1)|O(1)|
|Enqueue|O(log n)|O(log n)|Θ(1)|
|Dequeue|O(log n)|O(log n)|Θ(1)|
|IsEmpty|O(1)|O(1)|O(1)|
|Remove|O(log n)|O(log n)|O(n)|
|Contains|O(n)|O(1)|O(n)|
|Clear|O(n)|O(n)|O(1)|
|DecreaseKey|N/A|N/A|Θ(1)

Θ - amortized time

## Sources
Inspired by [WilliamFiset](https://www.youtube.com/channel/UCD8yeTczadqdARzQUp29PJw)'s [playlist](https://www.youtube.com/watch?v=wptevk0bshY&list=PLDV1Zeh2NRsCLFSHm1nYb9daYf60lCcag&index=1) explaining how priority queues work, along with sample implementation code

[Priority queue on Wikipedia](https://en.wikipedia.org/wiki/Priority_queue)

[Binary heap on Wikipedia](https://en.wikipedia.org/wiki/Binary_heap)

Fibonacci heap implementation based on [Gabor Makrai](https://github.com/gabormakrai)'s [Java implementation](https://github.com/gabormakrai/dijkstra-performance/blob/master/DijkstraPerformance/src/com/keithschwarz/FibonacciHeap.java)
