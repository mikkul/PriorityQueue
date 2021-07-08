# PriorityQueue

Priority queue implementations in C#

## Table of contents

* [Features](#features)
* [Example usage](#example-usage)
* [Implementation comparison](#implementation-comparison)
* [Time complexity](#time-complexity)
* [TODO](#todo)
* [Sources](#sources)

## Features

* Two default implementations of the priority queue data structure
* Easy way to create your own implementation by implementing the `IPriorityQueue` interface

## Example usage

First define a class that implements `IPriorityElemennt` interface, which will be used in our queue.
```cs
class Element : IPriorityElement
{
    public string Name { get; set; }
    
    // required property
    public float Priority { get; set; }
}
```

Then we can create a new priority queue using one of the two default implementations
```cs
// Create a new instance of BinaryHeapPriorityQueue
IPriorityQueue<Element> myPriorityQueue = new BinaryHeapPriorityQueue<Element>();
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

## Implementation comparison

There are two deafult implementations: `BinaryHeapPriorityQueue` and `MappedBinaryHeapPriorityQueue`. Both use a [binary heap](https://en.wikipedia.org/wiki/Binary_heap) as their underyling data structure, but the latter also stores all elements in a dictionary for faster lookup and element removal at the expense of slight memory and computational overhead.

## Time complexity

|Operation|BinaryHeapPriorityQueue|MappedBinaryHeapPriorityQueue
|---|---|---|
|Peek|O(1)|O(1)|
|Enqueue|O(log n)|O(log n)|
|Dequeue|O(log n)|O(log n)|
|IsEmpty|O(1)|O(1)|
|Remove|O(n)|O(1)|
|Contains|O(n)|O(1)|
|Clear|O(n)|O(n)|

## TODO

* Add a fibonnaci heap based implementation
* Add tests

## Sources
Inspired by [WilliamFiset](https://www.youtube.com/channel/UCD8yeTczadqdARzQUp29PJw)'s [playlist](https://www.youtube.com/watch?v=wptevk0bshY&list=PLDV1Zeh2NRsCLFSHm1nYb9daYf60lCcag&index=1) explaining how priority queues work, along with sample implementation code

[Priority queue on Wikipedia](https://en.wikipedia.org/wiki/Priority_queue)

[Binary heap on Wikipedia](https://en.wikipedia.org/wiki/Binary_heap)
