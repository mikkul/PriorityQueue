using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PriorityQueues.Tests
{
	[TestClass()]
	public abstract class BasePriorityQueueTests
	{
		protected abstract IPriorityQueue<SampleElement> CreatePriorityQueue();

		[TestMethod()]
		public void Empty_queue_has_count_of_zero()
		{
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();
			Assert.IsTrue(priorityQueue.IsEmpty() && priorityQueue.Count == 0);
		}

		[TestMethod()]
		public void Enqueued_elements_are_contained_in_the_queue()
		{
			var apple = new SampleElement("apple", 1f);
			var pear = new SampleElement("pear", 5f);
			var banana = new SampleElement("banana", 3f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			Assert.IsTrue(priorityQueue.Contains(apple));
			Assert.IsTrue(priorityQueue.Contains(pear));
			Assert.IsTrue(priorityQueue.Contains(banana));
		}

		[ExpectedException(typeof(InvalidOperationException))]
		[TestMethod()]
		public void Empty_queue_peek_throws_exception()
		{
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();
			priorityQueue.Peek();
		}

		[ExpectedException(typeof(InvalidOperationException))]
		[TestMethod()]
		public void Empty_queue_dequeue_throws_exception()
		{
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();
			priorityQueue.Dequeue();
		}

		[TestMethod()]
		public void Remove_non_existent_element_returns_false()
		{
			var nonExistentElement = new SampleElement("not in queue", 1f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			Assert.IsFalse(priorityQueue.Remove(nonExistentElement));
		}

		[TestMethod()]
		public void Remove_existing_element_returns_true()
		{
			var apple = new SampleElement("apple", 1f);
			var pear = new SampleElement("pear", 5f);
			var banana = new SampleElement("banana", 3f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			Assert.IsTrue(priorityQueue.Remove(pear));
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod()]
		public void Enqueue_null_element_throws_exception()
		{
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();
			priorityQueue.Enqueue(null);
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod()]
		public void Contains_null_throws_exception()
		{
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();
			priorityQueue.Contains(null);
		}

		[TestMethod()]
		public void Cleared_queue_is_empty()
		{
			var apple = new SampleElement("apple", 1f);
			var pear = new SampleElement("pear", 5f);
			var banana = new SampleElement("banana", 3f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			priorityQueue.Clear();

			Assert.IsTrue(priorityQueue.IsEmpty());
		}

		[TestMethod()]
		public void Queue_with_elements_is_not_empty()
		{
			var apple = new SampleElement("apple", 1f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);

			Assert.IsFalse(priorityQueue.IsEmpty());
		}

		[TestMethod()]
		public void Removed_elements_are_no_longer_contained_in_queue()
		{
			var apple = new SampleElement("apple", 1f);
			var pear = new SampleElement("pear", 5f);
			var banana = new SampleElement("banana", 3f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			priorityQueue.Remove(pear);

			Assert.IsFalse(priorityQueue.Contains(pear));
		}

		[TestMethod()]
		public abstract void Enqueue_elements_and_peek();
	}
}
