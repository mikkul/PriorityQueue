using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PriorityQueues
{
	/// <summary>
	/// Priority queue implementation based on a binary heap
	/// </summary>
	public class BinaryPriorityQueue<T> : IPriorityQueue<T>
	{
		#region Private fields
		protected List<T> _heap;
		protected readonly Comparison<T> _comparer;
		#endregion

		#region Public properties
		/// <inheritdoc/>
		public int Count => _heap.Count;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryPriorityQueue{T}"/> class that is empty and has the default initial capacity
		/// </summary>
		/// <param name="comparer"></param>
		public BinaryPriorityQueue(Comparison<T> comparer)
		{
			_heap = new List<T>();
			_comparer = comparer;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryPriorityQueue{T}"/> class that is empty and has the specified initial capacity
		/// </summary>
		/// <param name="capacity">The number of elements the <see cref="BinaryPriorityQueue{T}"/> can initially store</param>
		/// <param name="comparer"></param>
		public BinaryPriorityQueue(int capacity, Comparison<T> comparer)
		{
			_heap = new List<T>(capacity);
			_comparer = comparer;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryPriorityQueue{T}"/> class that contains elements copied from the specified collection, sorted by their priority value
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the <see cref="BinaryPriorityQueue{T}"/></param>
		/// <param name="comparer"></param>
		public BinaryPriorityQueue(IEnumerable<T> collection, Comparison<T> comparer)
		{
			_heap = new List<T>(collection.Count());
			_comparer = comparer;

			int i = 0;
			foreach (var elem in collection)
			{
				_heap.Add(elem);
			}

			// heapify process
			for (int j = Math.Max(0, (_heap.Count / 2) - 1); j >= 0; j--)
			{
				Sink(i);
			}
		}
		#endregion

		#region Public methods
		/// <inheritdoc/>
		public virtual void Clear()
		{
			_heap.Clear();
		}

		/// <inheritdoc/>
		public virtual bool Contains(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}

			// do a linear scan
			foreach (var elem in _heap)
			{
				if (elem.Equals(element))
				{
					return true;
				}
			}
			return false;
		}

		/// <inheritdoc/>
		public virtual T Dequeue()
		{
			return IsEmpty() ? throw new InvalidOperationException("Queue is empty") : RemoveAt(0);
		}

		/// <inheritdoc/>
		public virtual void Enqueue(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}

			_heap.Add(element);

			Swim(_heap.Count - 1);
		}

		/// <inheritdoc/>
		public virtual bool IsEmpty()
		{
			return Count == 0;
		}

		/// <inheritdoc/>
		public virtual T Peek()
		{
			return IsEmpty() ? throw new InvalidOperationException("Queue is empty") : _heap[0];
		}

		/// <inheritdoc/>
		public virtual bool Remove(T element)
		{
			if (element == null)
			{
				return false;
			}

			for (int i = 0; i < Count; i++)
			{
				if (_heap[i].Equals(element))
				{
					RemoveAt(i);
					return true;
				}
			}
			return false;
		}
		#endregion

		#region Private methods
		protected bool IsHeapInvariantMaintained(int index)
		{
			if (index >= Count)
			{
				return true;
			}

			int leftIndex = 2 * index + 1;
			int rightIndex = 2 * index + 2;

			if (leftIndex < Count && !Less(index, leftIndex)) {
				return false;
			}
			if (rightIndex < Count && !Less(index, rightIndex)) {
				return false;
			}

			return IsHeapInvariantMaintained(leftIndex) && IsHeapInvariantMaintained(rightIndex);
		}

		protected bool Less(int i, int j)
		{
			return _comparer(_heap[i], _heap[j]) <= 0;
		}

		protected void Swim(int index)
		{
			int parentIndex = (index - 1) / 2;

			while (index > 0 && Less(index, parentIndex))
			{
				Swap(parentIndex, index);
				index = parentIndex;
				parentIndex = (index - 1) / 2;
			}
		}

		protected void Sink(int index)
		{
			while (true)
			{
				int leftChildIndex = 2 * index + 1;
				int rightChildIndex = 2 * index + 2;
				int smallerNodeIndex = leftChildIndex;
				if (rightChildIndex < Count && Less(rightChildIndex, leftChildIndex))
				{
					smallerNodeIndex = rightChildIndex;
				}

				// stop if we're outside bounds or we cannot sink anymore
				if (leftChildIndex >= Count || Less(index, smallerNodeIndex))
				{
					break;
				}

				Swap(smallerNodeIndex, index);
				index = smallerNodeIndex;
			}
		}

		protected virtual void Swap(int i, int j)
		{
			T i_elem = _heap[i];
			T j_elem = _heap[j];

			_heap[i] = j_elem;
			_heap[j] = i_elem;
		}

		protected virtual T RemoveAt(int index)
		{
			if (index < 0 || index > Count - 1)
			{
				throw new ArgumentOutOfRangeException("index", index, string.Empty);
			}

			int indexOfLastElement = Count - 1;
			T removedElem = _heap[index];

			Swap(index, indexOfLastElement);

			_heap.RemoveAt(indexOfLastElement);

			if (index == indexOfLastElement) {
				return removedElem;
			}

			T elem = _heap[index];
			Sink(index);
			if (_heap[index].Equals(elem))
			{
				Swim(index);
			}

			return removedElem;
		}
		#endregion

		#region IEnumerable interface implementation
		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)_heap).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_heap).GetEnumerator();
		}
		#endregion
	}
}
