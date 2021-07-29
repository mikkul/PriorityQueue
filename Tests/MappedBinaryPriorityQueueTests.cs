using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PriorityQueues.Tests
{
	[TestClass()]
	public class MinMappedBinaryPriorityQueueTests : BasePriorityQueueTests
	{
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
