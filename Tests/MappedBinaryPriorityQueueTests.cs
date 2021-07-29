using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PriorityQueues.Tests
{
	[TestClass()]
	public class MinMappedBinaryPriorityQueueTests : BasePriorityQueueTests
	{
		[TestMethod()]
		public override void Complex_test_with_multiple_operations()
		{
			var apple = new SampleElement("apple", 50f);
			var banana = new SampleElement("banana", 40f);
			var cherry = new SampleElement("cherry", 30f);
			var date = new SampleElement("date", 20f);
			var elderberry = new SampleElement("elderberry", 10f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(date);
			priorityQueue.Enqueue(elderberry);

			Assert.AreEqual(priorityQueue.Count, 2);
			Assert.AreEqual(priorityQueue.Peek(), elderberry);
			Assert.IsTrue(priorityQueue.Contains(elderberry));
			Assert.AreEqual(priorityQueue.Dequeue(), elderberry);

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(banana);
			priorityQueue.Enqueue(cherry);

			Assert.AreEqual(priorityQueue.Count, 4);

			Assert.IsFalse(priorityQueue.Remove(elderberry));
			Assert.AreEqual(priorityQueue.Count, 4);

			Assert.IsTrue(priorityQueue.Remove(apple));
			Assert.AreEqual(priorityQueue.Count, 3);

			Assert.AreEqual(priorityQueue.Dequeue(), date);
			Assert.AreEqual(priorityQueue.Dequeue(), cherry);
			Assert.AreEqual(priorityQueue.Dequeue(), banana);

			Assert.IsTrue(priorityQueue.IsEmpty());
		}

		[TestMethod()]
		public override void Enqueue_elements_and_dequeue_returns_items_in_priority_order()
		{
			var apple = new SampleElement("apple", 3f);
			var pear = new SampleElement("pear", 1f);
			var banana = new SampleElement("banana", 5f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			Assert.IsTrue(priorityQueue.Dequeue() == pear);
			Assert.IsTrue(priorityQueue.Dequeue() == apple);
			Assert.IsTrue(priorityQueue.Dequeue() == banana);
		}

		[TestMethod()]
		public override void Enqueue_elements_and_peek()
		{
			var apple = new SampleElement("apple", 3f);
			var pear = new SampleElement("pear", 1f);
			var banana = new SampleElement("banana", 5f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			Assert.IsTrue(priorityQueue.Peek() == pear);
		}

		protected override IPriorityQueue<SampleElement> CreatePriorityQueue()
		{
			return new MappedBinaryPriorityQueue<SampleElement>((a, b) => a.Priority.CompareTo(b.Priority));
		}
	}

	[TestClass()]
	public class MaxMappedBinaryPriorityQueueTests : BasePriorityQueueTests
	{
		[TestMethod()]
		public override void Complex_test_with_multiple_operations()
		{
			var apple = new SampleElement("apple", 10f);
			var banana = new SampleElement("banana", 20f);
			var cherry = new SampleElement("cherry", 30f);
			var date = new SampleElement("date", 40f);
			var elderberry = new SampleElement("elderberry", 50f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(date);
			priorityQueue.Enqueue(elderberry);

			Assert.AreEqual(priorityQueue.Count, 2);
			Assert.AreEqual(priorityQueue.Peek(), elderberry);
			Assert.IsTrue(priorityQueue.Contains(elderberry));
			Assert.AreEqual(priorityQueue.Dequeue(), elderberry);

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(banana);
			priorityQueue.Enqueue(cherry);

			Assert.AreEqual(priorityQueue.Count, 4);

			Assert.IsFalse(priorityQueue.Remove(elderberry));
			Assert.AreEqual(priorityQueue.Count, 4);

			Assert.IsTrue(priorityQueue.Remove(apple));
			Assert.AreEqual(priorityQueue.Count, 3);

			Assert.AreEqual(priorityQueue.Dequeue(), date);
			Assert.AreEqual(priorityQueue.Dequeue(), cherry);
			Assert.AreEqual(priorityQueue.Dequeue(), banana);

			Assert.IsTrue(priorityQueue.IsEmpty());
		}

		[TestMethod()]
		public override void Enqueue_elements_and_dequeue_returns_items_in_priority_order()
		{
			var apple = new SampleElement("apple", 1f);
			var pear = new SampleElement("pear", 5f);
			var banana = new SampleElement("banana", 4f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			Assert.IsTrue(priorityQueue.Dequeue() == pear);
			Assert.IsTrue(priorityQueue.Dequeue() == banana);
			Assert.IsTrue(priorityQueue.Dequeue() == apple);
		}

		[TestMethod()]
		public override void Enqueue_elements_and_peek()
		{
			var apple = new SampleElement("apple", 1f);
			var pear = new SampleElement("pear", 5f);
			var banana = new SampleElement("banana", 4f);
			IPriorityQueue<SampleElement> priorityQueue = CreatePriorityQueue();

			priorityQueue.Enqueue(apple);
			priorityQueue.Enqueue(pear);
			priorityQueue.Enqueue(banana);

			Assert.IsTrue(priorityQueue.Peek() == pear);
		}

		protected override IPriorityQueue<SampleElement> CreatePriorityQueue()
		{
			return new MappedBinaryPriorityQueue<SampleElement>((a, b) => b.Priority.CompareTo(a.Priority));
		}
	}
}
