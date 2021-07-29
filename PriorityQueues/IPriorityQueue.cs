using System;
using System.Collections.Generic;

namespace PriorityQueues
{
	/// <summary>
	/// Represents a generic priority queue
	/// </summary>
	public interface IPriorityQueue<T> : IEnumerable<T>
	{
		/// <summary>
		/// Gets the number of elements contained in the queue
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Clear all elements from the queue
		/// </summary>
		void Clear();

		/// <summary>
		/// Determines whether the queue contains the specified element
		/// </summary>
		/// <param name="element"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <returns><see langword="true"/> if the queue contains the specified element, <see langword="false"/> otherwise</returns>
		bool Contains(T element);

		/// <summary>
		/// Returns the element with the highest priority value and removes it from the queue
		/// </summary>
		/// <exception cref="InvalidOperationException"></exception>
		/// <returns>The element with the highest priority</returns>
		T Dequeue();

		/// <summary>
		/// Inserts an element to the queue according to its priority value
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <param name="element">The element to be added to the queue</param>
		void Enqueue(T element);

		/// <summary>
		/// Determines if the queue contains no elements
		/// </summary>
		/// <returns><see langword="true"/> if the queue contains no elements, <see langword="false"/> otherwise</returns>
		bool IsEmpty();

		/// <summary>
		/// Returns the element with the highest priority value without removing it from the queue
		/// </summary>
		/// <exception cref="InvalidOperationException"></exception>
		/// <returns>The element with the highest priority</returns>
		T Peek();

		/// <summary>
		/// Removes an element from the queue
		/// </summary>
		/// <param name="element">Element to remove from queue; can be null</param>
		/// <returns><see langword="true"/> if the element is successfuly removed; otherwise, <see langword="false"/>. This method also returns false if the element is not found in the queue</returns>
		bool Remove(T element);
	}
}
