using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PriorityQueues
{
	public class MappedPriorityQueue<T> : IPriorityQueue<T> where T : IPriorityElement
	{
		#region Private fields
		private List<T> _heap;

		private Dictionary<T, List<int>> _map;
		#endregion

		#region Public properties
		/// <inheritdoc/>
		public int Count => _heap.Count;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of <see cref="PriorityQueue{T}"/> class that is empty and has the default initial capacity
		/// </summary>
		public MappedPriorityQueue()
		{
			_heap = new List<T>();

			_map = new Dictionary<T, List<int>>();
		}

		/// <summary>
		/// Initializes a new instance of <see cref="PriorityQueue{T}"/> class that is empty and has the specified initial capacity
		/// </summary>
		/// <param name="capacity">The number of elements the <see cref="PriorityQueue{T}"/> can initially store</param>
		public MappedPriorityQueue(int capacity)
		{
			_heap = new List<T>(capacity);

			_map = new Dictionary<T, List<int>>(capacity);
		}

		/// <summary>
		/// Initializes a new instance of <see cref="PriorityQueue{T}"/> class that contains elements copied from the specified collection, sorted by their priority value
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the <see cref="PriorityQueue{T}"/></param>
		public MappedPriorityQueue(IEnumerable<T> collection)
		{
			_heap = new List<T>(collection.Count());

			_map = new Dictionary<T, List<int>>();

			int i = 0;
			foreach (var elem in collection)
			{
				_heap.Add(elem);

				MapAdd(elem, i++);
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
		public void Clear()
		{
			_heap.Clear();

			_map.Clear();
		}

		/// <inheritdoc/>
		public bool Contains(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}

			// lookup the key in the dictionary
			return _map.ContainsKey(element);
		}

		/// <inheritdoc/>
		public T Dequeue()
		{
			return IsEmpty() ? throw new InvalidOperationException("Queue is empty") : RemoveAt(0);
		}

		/// <inheritdoc/>
		public void Enqueue(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}

			_heap.Add(element);

			MapAdd(element, _heap.Count - 1);

			Swim(_heap.Count - 1);
		}

		/// <inheritdoc/>
		public bool IsEmpty()
		{
			return Count == 0;
		}

		/// <inheritdoc/>
		public T Peek()
		{
			return IsEmpty() ? throw new InvalidOperationException("Queue is empty") : _heap[0];
		}

		/// <inheritdoc/>
		public bool Remove(T element)
		{
			if (element == null)
			{
				return false;
			}

			int index = MapGet(element);
			if (index != -1)
			{
				RemoveAt(index);
			}
			return index != -1;
		}
		#endregion

		#region Private methods
		private bool IsHeapInvariantMaintained(int index)
		{
			if (index >= Count)
			{
				return true;
			}

			int leftIndex = 2 * index + 1;
			int rightIndex = 2 * index + 2;

			if (leftIndex < Count && !Less(index, leftIndex))
			{
				return false;
			}
			if (rightIndex < Count && !Less(index, rightIndex))
			{
				return false;
			}

			return IsHeapInvariantMaintained(leftIndex) && IsHeapInvariantMaintained(rightIndex);
		}
		private bool Less(int i, int j)
		{
			return _heap[i].Priority <= _heap[j].Priority;
		}

		private void Swim(int index)
		{
			int parentIndex = (index - 1) / 2;

			while (index > 0 && Less(index, parentIndex))
			{
				Swap(parentIndex, index);
				index = parentIndex;
				parentIndex = (index - 1) / 2;
			}
		}

		private void Sink(int index)
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

		private void Swap(int i, int j)
		{
			T i_elem = _heap[i];
			T j_elem = _heap[j];

			_heap[i] = j_elem;
			_heap[j] = i_elem;

			MapSwap(i_elem, j_elem, i, j);
		}

		private T RemoveAt(int index)
		{
			if (index < 0 || index > Count - 1)
			{
				throw new ArgumentOutOfRangeException("index", index, string.Empty);
			}

			T removedElem = _heap[index];

			Swap(index, Count - 1);

			_heap.RemoveAt(Count - 1);

			MapRemove(removedElem, Count - 1);

			if (IsEmpty())
			{
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

		private void MapAdd(T value, int index)
		{
			if (!_map.ContainsKey(value))
			{
				_map[value] = new List<int>() { index };
			}
			else
			{
				_map[value].Add(index);
			}
		}

		private void MapRemove(T value, int index)
		{
			_map[value].Remove(index);
			if (_map[value].Count == 0)
			{
				_map.Remove(value);
			}
		}

		private int MapGet(T value)
		{
			if (_map.ContainsKey(value))
			{
				return _map[value][0];
			}
			else
			{
				return -1;
			}
		}

		private void MapSwap(T value1, T value2, int index1, int index2)
		{
			_map[value1].Remove(index1);
			_map[value2].Remove(index2);
			_map[value1].Add(index2);
			_map[value2].Add(index1);
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
