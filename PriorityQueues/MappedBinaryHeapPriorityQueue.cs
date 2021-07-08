﻿using System;
using System.Collections.Generic;

namespace PriorityQueues
{
	public class MappedBinaryHeapPriorityQueue<T> : BinaryHeapPriorityQueue<T>
	{
		#region Private fields
		private Dictionary<T, List<int>> _map;
		#endregion

		#region Constructors
		/// <inheritdoc/>
		public MappedBinaryHeapPriorityQueue(Comparison<T> comparer) : base(comparer)
		{
			_map = new Dictionary<T, List<int>>();
		}

		/// <inheritdoc/>
		public MappedBinaryHeapPriorityQueue(int capacity, Comparison<T> comparer) : base(capacity, comparer)
		{
			_map = new Dictionary<T, List<int>>(capacity);
		}

		/// <inheritdoc/>
		public MappedBinaryHeapPriorityQueue(IEnumerable<T> collection, Comparison<T> comparer) : base(comparer)
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
			base.Enqueue(element);

			MapAdd(element, _heap.Count - 1);
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
	}
}
