using System;
using System.Collections.Generic;

namespace PriorityQueues
{
	/// <summary>
	/// Binary heap implementation that uses a Dictionary underneath for storing the values for faster lookup and item removal
	/// </summary>
	public class MappedBinaryPriorityQueue<T> : BinaryPriorityQueue<T>
	{
		#region Private fields
		private Dictionary<T, List<int>> _map;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of <see cref="MappedBinaryPriorityQueue{T}"/> class that is empty and has the default initial capacity
		/// </summary>
		/// <param name="comparer"></param>
		public MappedBinaryPriorityQueue(Comparison<T> comparer) : base(comparer)
		{
			_map = new Dictionary<T, List<int>>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MappedBinaryPriorityQueue{T}"/> class that is empty and has the specified initial capacity
		/// </summary>
		/// <param name="capacity">The number of elements the <see cref="MappedBinaryPriorityQueue{T}"/> can initially store</param>
		/// <param name="comparer"></param>
		public MappedBinaryPriorityQueue(int capacity, Comparison<T> comparer) : base(capacity, comparer)
		{
			_map = new Dictionary<T, List<int>>(capacity);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MappedBinaryPriorityQueue{T}"/> class that contains elements copied from the specified collection, sorted by their priority value
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the <see cref="MappedBinaryPriorityQueue{T}"/></param>
		/// <param name="comparer"></param>
		public MappedBinaryPriorityQueue(IEnumerable<T> collection, Comparison<T> comparer) : base(comparer)
		{
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
		public override void Clear()
		{
			base.Clear();

			_map.Clear();
		}

		/// <inheritdoc/>
		public override bool Contains(T element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}

			// lookup the key in the dictionary
			return _map.ContainsKey(element);
		}

		/// <inheritdoc/>
		public override void Enqueue(T element)
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
		public override bool Remove(T element)
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
		protected override void Swap(int i, int j)
		{
			T i_elem = _heap[i];
			T j_elem = _heap[j];

			_heap[i] = j_elem;
			_heap[j] = i_elem;

			MapSwap(i_elem, j_elem, i, j);
		}

		protected override T RemoveAt(int index)
		{
			if (index < 0 || index > Count - 1)
			{
				throw new ArgumentOutOfRangeException("index", index, string.Empty);
			}

			int indexOfLastElement = Count - 1;
			T removedElem = _heap[index];

			Swap(index, indexOfLastElement);

			_heap.RemoveAt(indexOfLastElement);

			MapRemove(removedElem);

			if (index == indexOfLastElement)
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

		private void MapRemove(T value)
		{
			_map[value].RemoveAt(0);
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
	}
}
