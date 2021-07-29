using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PriorityQueues.Tests
{
	[TestClass()]
	public class MinFibonacciPriorityQueueTests : BasePriorityQueueTests
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
		public void Complex_test_with_multiple_operations_2()
		{
			var apple = new SampleElement("apple", 50f);
			var banana = new SampleElement("banana", 40f);
			var cherry = new SampleElement("cherry", 30f);
			var date = new SampleElement("date", 20f);
			var elderberry = new SampleElement("elderberry", 10f);
			FibonacciPriorityQueue<SampleElement> priorityQueue = new FibonacciPriorityQueue<SampleElement>(PriorityQueueType.Minimum);

			priorityQueue.Enqueue(date, date.Priority);
			priorityQueue.Enqueue(elderberry, elderberry.Priority);

			Assert.AreEqual(priorityQueue.Count, 2);
			Assert.AreEqual(priorityQueue.Peek(), elderberry);
			Assert.IsTrue(priorityQueue.Contains(elderberry));
			Assert.AreEqual(priorityQueue.Dequeue(), elderberry);

			priorityQueue.Enqueue(apple, apple.Priority);
			priorityQueue.Enqueue(banana, banana.Priority);
			priorityQueue.Enqueue(cherry, cherry.Priority);

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

			Assert.AreEqual(priorityQueue.Dequeue(), pear);
			Assert.AreEqual(priorityQueue.Dequeue(), apple);
			Assert.AreEqual(priorityQueue.Dequeue(), banana);
		}

		protected override IPriorityQueue<SampleElement> CreatePriorityQueue()
		{
			return new FibonacciPriorityQueue<SampleElement>((a, b) => a.Value.Priority.CompareTo(b.Value.Priority));
		}
	}

	[TestClass()]
	public class MaxFibonacciPriorityQueueTests : BasePriorityQueueTests
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
		public void Complex_test_with_multiple_operations_2()
		{
			var apple = new SampleElement("apple", 10f);
			var banana = new SampleElement("banana", 20f);
			var cherry = new SampleElement("cherry", 30f);
			var date = new SampleElement("date", 40f);
			var elderberry = new SampleElement("elderberry", 50f);
			FibonacciPriorityQueue<SampleElement> priorityQueue = new FibonacciPriorityQueue<SampleElement>(PriorityQueueType.Maximum);

			priorityQueue.Enqueue(date, date.Priority);
			priorityQueue.Enqueue(elderberry, elderberry.Priority);

			Assert.AreEqual(priorityQueue.Count, 2);
			Assert.AreEqual(priorityQueue.Peek(), elderberry);
			Assert.IsTrue(priorityQueue.Contains(elderberry));
			Assert.AreEqual(priorityQueue.Dequeue(), elderberry);

			priorityQueue.Enqueue(apple, apple.Priority);
			priorityQueue.Enqueue(banana, banana.Priority);
			priorityQueue.Enqueue(cherry, cherry.Priority);

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

			Assert.AreEqual(priorityQueue.Dequeue(), pear);
			Assert.AreEqual(priorityQueue.Dequeue(), banana);
			Assert.AreEqual(priorityQueue.Dequeue(), apple);
		}

		protected override IPriorityQueue<SampleElement> CreatePriorityQueue()
		{
			return new FibonacciPriorityQueue<SampleElement>((a, b) => b.Value.Priority.CompareTo(a.Value.Priority));
		}
	}
}
