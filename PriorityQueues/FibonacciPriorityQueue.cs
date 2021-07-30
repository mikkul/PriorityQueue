using System;
using System.Collections;
using System.Collections.Generic;

namespace PriorityQueues
{
	/// <summary>
	/// Used for comparing priorities
	/// </summary>
	public enum PriorityQueueType
	{
		/// <summary>
		/// Specifies a min heap priority queue
		/// </summary>
		Minimum,
		/// <summary>
		/// Specifies a max heap priority queue
		/// </summary>
		Maximum
	}

	/// <summary>
	/// A Fibonacci heap implementation of the priority queue
	/// </summary>
	public class FibonacciPriorityQueue<T> : IPriorityQueue<T>
	{
		/// <summary>
		/// A wrapper around user specified type
		/// </summary>
		public sealed class QueueElement
		{
			internal QueueElement(T value)
			{
				Value = value;
			}

			internal QueueElement(T value, double priority)
			{
				Value = value;
				Priority = priority;
			}

			/// <summary>
			/// The actual value of the user-specified type
			/// </summary>
			public T Value { get; set; }

			/// <summary>
			/// The priority value
			/// </summary>
			public double Priority { get; set; }
		}

		/// <summary>
		/// Represents a single node in the fibonacci heap
		/// </summary>
		public sealed class Node
		{
			internal Node Parent { get; set; }
			internal Node Child { get; set; }
			internal Node Left { get; set; }
			internal Node Right { get; set; }
			internal int Degree { get; set; }
			internal bool IsMarked { get; set; }
			internal QueueElement Key { get; set; }

			internal Node(QueueElement value)
			{
				Left = this;
				Right = this;
				Key = value;
			}
		}

		#region Private fields
		private int _count;
		private Node _minNode;
		private readonly Comparison<QueueElement> _comparer;
		private readonly bool _isMaxHeap;
		private readonly bool _hasCustomComparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="FibonacciPriorityQueue{T}"/> class that is empty
		/// </summary>
		/// <param name="type">The type of this priority queue used for comparing nodes</param>
		public FibonacciPriorityQueue(PriorityQueueType type)
		{
			Type = type;
			if(type == PriorityQueueType.Maximum)
			{
				_isMaxHeap = true;
			}
			else
			{
				_isMaxHeap = false;
			}

			if(_isMaxHeap)
			{
				_comparer = (a, b) => b.Priority.CompareTo(a.Priority);
			}
			else
			{
				_comparer = (a, b) => a.Priority.CompareTo(b.Priority);
			}
			_hasCustomComparer = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FibonacciPriorityQueue{T}"/> class that is empty
		/// </summary>
		/// <param name="comparer"></param>
		public FibonacciPriorityQueue(Comparison<QueueElement> comparer)
		{
			_comparer = comparer;
			_hasCustomComparer = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FibonacciPriorityQueue{T}"/> class that contains elements copied from the specified collection
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the <see cref="FibonacciPriorityQueue{T}"/></param>
		/// <param name="comparer"></param>
		public FibonacciPriorityQueue(IEnumerable<T> collection, Comparison<QueueElement> comparer) : this(comparer)
		{
			foreach (var element in collection)
			{
				Enqueue(element);
			}
		}
		#endregion

		#region Public properties
		/// <inheritdoc/>
		public int Count => _count;

		/// <summary>
		/// Gets the type of this priority queue
		/// </summary>
		public PriorityQueueType Type { get; }
		#endregion

		#region Public methods
		/// <inheritdoc/>
		public void Clear()
		{
			_minNode = null;
			_count = 0;
		}

		/// <inheritdoc/>
		public bool Contains(T element)
		{
			if(element == null)
			{
				throw new ArgumentNullException("element");
			}

			return Find(element, _minNode) != null;
		}

		/// <summary>
		/// Sets a new priority for the specified node. The new value has to be lower if the <see cref="Type"/> is <see cref="PriorityQueueType.Maximum"/> or higher if the <see cref="Type"/> is <see cref="PriorityQueueType.Minimum"/>
		/// </summary>
		/// <param name="node"><see cref="Node"/> to be modified</param>
		/// <param name="newPriority">A finite value representing the new priority</param>
		public void DecreaseKey(Node node, double newPriority)
		{
			if(double.IsNaN(newPriority))
			{
				throw new ArgumentException("New priority value can't be NaN", "newPriority");
			}

			if(_comparer(new QueueElement(default(T), newPriority), node.Key) > 0)
			{
				throw new ArgumentException("New priority exceeds old", "newPriority");
			}

			DecreaseKeyUnchecked(node, newPriority);
		}

		/// <inheritdoc/>
		public T Dequeue()
		{
			if(IsEmpty())
			{
				throw new InvalidOperationException("Queue is empty");
			}

			_count--;

			var minElem = _minNode;

			if (_minNode.Right == _minNode)
			{
				_minNode = null;
			}
			else
			{
				_minNode.Left.Right = _minNode.Right;
				_minNode.Right.Left = _minNode.Left;
				_minNode = _minNode.Right;
			}

			if (minElem.Child != null)
			{
				var curr = minElem.Child;
				do
				{
					curr.Parent = null;
					curr = curr.Right;
				} 
				while (curr != minElem.Child);
			}

			_minNode = MergeLists(_minNode, minElem.Child);

			if (_minNode == null)
			{
				return minElem.Key.Value;
			}

			List<Node> treeTable = new List<Node>();

			List<Node> toVisit = new List<Node>();

			for (Node curr = _minNode; toVisit.Count == 0 || toVisit[0] != curr; curr = curr.Right)
			{
				toVisit.Add(curr);
			}

			foreach (var item in toVisit)
			{
				var curr = item;
				while (true)
				{
					while (curr.Degree >= treeTable.Count)
					{
						treeTable.Add(null);
					}

					if (treeTable[curr.Degree] == null)
					{
						treeTable[curr.Degree] = curr;
						break;
					}

					Node other = treeTable[curr.Degree];
					treeTable[curr.Degree] = null;

					Node min = _comparer(other.Key, curr.Key) < 0 ? other : curr;
					Node max = _comparer(other.Key, curr.Key) < 0 ? curr : other;

					max.Right.Left = max.Left;
					max.Left.Right = max.Right;

					max.Right = max.Right = max;
					min.Child = MergeLists(min.Child, max);

					max.Parent = min;

					max.IsMarked = false;

					min.Degree++;

					curr = min;
				}

				if(_comparer(curr.Key, _minNode.Key) <= 0)
				{
					_minNode = curr;
				}
			}
			return minElem.Key.Value;
		}

		/// <inheritdoc/>
		public void Enqueue(T element)
		{
			Enqueue(element, 0);
		}

		/// <summary>
		/// Inserts an element to the queue with a specified priority value
		/// </summary>
		/// /// <exception cref="ArgumentNullException"></exception>
		/// <param name="element">The element to be added to the queue</param>
		/// <param name="priority">The priority value</param>
		public Node Enqueue(T element, double priority)
		{
			if(element == null)
			{
				throw new ArgumentNullException("element");
			}

			var newNode = new Node(new QueueElement(element, priority));

			_minNode = MergeLists(_minNode, newNode);

			_count++;

			return newNode;
		}

		/// <inheritdoc/>
		public bool IsEmpty()
		{
			return _minNode == null;
		}

		/// <inheritdoc/>
		public T Peek()
		{
			return IsEmpty() ? throw new InvalidOperationException("Queue is empty") : _minNode.Key.Value;
		}

		/// <inheritdoc/>
		public bool Remove(T element)
		{
			Node node = Find(element, _minNode);
			if(node == null)
			{
				return false;
			}

			Remove(node);
			return true;
		}

		/// <summary>
		/// Removes a node from the queue
		/// </summary>
		/// <param name="node"><see cref="Node"/> to be removed</param>
		public void Remove(Node node)
		{
			if(_hasCustomComparer)
			{
				DecreaseKeyUnchecked(node);
			}
			else
			{
				var lowestValue = _isMaxHeap ? double.PositiveInfinity : double.NegativeInfinity;
				DecreaseKeyUnchecked(node, lowestValue);
			}

			Dequeue();
		}
		#endregion

		#region Private methods
		private Node MergeLists(Node one, Node two)
		{
			if (one == null && two == null)
			{
				return null;
			}
			else if (one != null && two == null)
			{
				return one;
			}
			else if (one == null && two != null)
			{
				return two;
			}
			else
			{ 
				Node oneRight = one.Right;
				one.Right = two.Right;
				one.Right.Left = one;
				two.Right = oneRight;
				two.Right.Left = two;

				return _comparer(one.Key, two.Key) < 0 ? one : two;
			}
		}

		private void CutNode(Node node)
		{
			node.IsMarked = false;

			if (node.Parent == null)
			{
				return;
			}

			if (node.Right != node)
			{
				node.Right.Left = node.Left;
				node.Left.Right = node.Right;
			}


			if (node.Parent.Child == node)
			{
				if (node.Right != node)
				{
					node.Parent.Child = node.Right;
				}
				else
				{
					node.Parent.Child = null;
				}
			}

			node.Parent.Degree--;

			node.Left = node.Right = node;
			_minNode = MergeLists(_minNode, node);

			if (node.Parent.IsMarked)
			{
				CutNode(node.Parent);
			}
			else
			{
				node.Parent.IsMarked = true;
			}

			node.Parent = null;
		}

		private void DecreaseKeyUnchecked(Node node, double priority)
		{
			node.Key.Priority = priority;

			if (node.Parent != null && _comparer(node.Key, node.Parent.Key) <= 0)
			{
				CutNode(node);
			}

			if (_comparer(node.Key, _minNode.Key) <= 0)
			{
				_minNode = node;
			}
		}

		private void DecreaseKeyUnchecked(Node node)
		{
			if (node.Parent != null)
			{
				CutNode(node);
			}

			_minNode = node;
		}

		private Node Find(T element, Node c, Node found = null)
		{
			if (c == null || found != null)
			{
				return found;
			}

			Node temp = c;
			do
			{
				if (temp.Key.Value.Equals(element))
				{
					return temp;
				}
				else
				{
					Node k = temp.Child;
					found = Find(element, k, found);
					temp = temp.Right;
				}
			}
			while (temp != c && found == null);

			return null;
		}
		#endregion

		#region IEnumerable interface implementation
		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator()
		{
			if(IsEmpty())
			{
				yield break;
			}

			foreach (var item in Enumerate())
			{
				yield return item;
			}
		}

		private IEnumerable<T> Enumerate()
		{
			if (IsEmpty())
			{
				yield break;
			}
			var current = _minNode;
			do
			{
				foreach (var node in EnumerateBranch(current))
				{
					yield return node;
				}
				current = current.Right;
			}
			while (current != _minNode);
		}

		private IEnumerable<T> EnumerateBranch(Node root)
		{
			if (root.Child != null)
			{
				var current = root.Child;
				do
				{
					foreach (var node in EnumerateBranch(current))
					{
						yield return node;
					}
					current = current.Right;
				}
				while (current != root.Child);
			}
			yield return root.Key.Value;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}
