using Collections;
using NUnit.Framework.Constraints;

namespace Collection.Unit.Tests
{
    public class CollectionTests
    {
        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            var collection = new Collection<int>();
            Assert.AreEqual(collection.ToString(), "[]");
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            var collection = new Collection<int>(5);
            Assert.AreEqual(collection.ToString(), "[5]");
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            var collection = new Collection<int>(5, 6);
            Assert.AreEqual(collection.ToString(), "[5, 6]");
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            var collection = new Collection<int>(5, 6);
            Assert.AreEqual(collection.Count, 2, "Check for Count");
            Assert.That(collection.Capacity, Is.GreaterThan(collection.Count));
        }

        [Test]
        public void Test_Collection_Add()
        {
            var collection = new Collection<string>("Ivan", "Peter");
            collection.Add("Gosho");
            Assert.AreEqual(collection.ToString(), "[Ivan, Peter, Gosho]");
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            var item = collection[1];
            Assert.That(item.ToString(), Is.EqualTo("6"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            collection[1] = 666;
            Assert.That(collection.ToString(), Is.EqualTo("[5, 666, 7]"));
        }
        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            Assert.That(() => { collection[100] = 66; }, Throws.InstanceOf<ArgumentOutOfRangeException>());

        }
        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var collection = new Collection<string>("Ivan", "Peter");
            Assert.That(() => { var item = collection[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            var collection = new Collection<int>(1, 2, 3);
            collection.AddRange(9, 7, 8, 6);
            Assert.AreEqual(7, collection.Count);
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var collection = new Collection<int>();
            int oldCapacity = collection.Capacity;
            var newCollection = Enumerable.Range(1000, 2000).ToArray();
            collection.AddRange(newCollection);
            string expectedCollection = "[" + string.Join(", ", newCollection) + "]";
            Assert.That(collection.ToString(), Is.EqualTo(expectedCollection));
            Assert.That(collection.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(collection.Capacity, Is.GreaterThanOrEqualTo(collection.Count));
        }
        [Test]
        public void Test_Collection_AddeWithGrow()
        {
            var collection = new Collection<int>(1, 2, 3);
            collection.Add(4);
            Assert.AreEqual(4, collection.Count);
            Assert.AreEqual(collection.Capacity, 16);
        }
        [Test]
        public void Test_Collection_InsertAtStart()
        {
            var collection = new Collection<int>(1, 2, 3);
            collection.InsertAt(0, 10);
            Assert.AreEqual(4, collection.Count);
            Assert.AreEqual(collection.Capacity, 16);
        }
        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            var collection = new Collection<int>(1, 2, 3);
            collection.Add(10);
            Assert.AreEqual(4, collection.Count);
            Assert.AreEqual(collection.Capacity, 16);
        }
        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5, 6);
            var middle = (collection.Count / 2);
            collection.InsertAt(middle, 20);
            Assert.AreEqual(20, collection[middle]);
        }
        [Test]
        public void Test_Collection_InsertAtWithGrow()
        {
            var collection = new Collection<int>(1, 2, 3);
            int oldCapacity = collection.Capacity;
            var newCollection = Enumerable.Range(10, 50).ToArray();
            collection.AddRange(newCollection);
            collection.InsertAt(20, 50);
            string expectedCollection = "[" + string.Join(", ", newCollection) + "]";
            Assert.AreNotEqual(collection.ToString(), expectedCollection);
            Assert.That(collection.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(collection.Capacity, Is.GreaterThanOrEqualTo(collection.Count));
        }
        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            var collection = new Collection<string>("Ivan", "Peter");
            Assert.That(() => { collection.InsertAt(3, "George"); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5);
            var middle = (collection.Count / 2);
            var midItem = collection[middle];
            collection.Exchange(middle, 0);
            Assert.AreEqual(collection[0], midItem);
            Assert.AreEqual(collection[0], 3);
            Assert.AreEqual(collection[middle], 1);
        }
        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5);
            var first = collection[0];
            var last = collection[collection.Count - 1];
            collection.Exchange(0, collection.Count - 1);
            Assert.AreEqual(first, collection[collection.Count - 1]);
            Assert.AreEqual(last, collection[0]);
        }
        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            var collection = new Collection<string>("Ivan", "Peter");
            Assert.That(() => { collection.Exchange(3, 2); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5);
            var first = collection[0];
            var initialCount = collection.Count;
            collection.RemoveAt(0);
            Assert.AreNotEqual(first, collection[0]);
            Assert.That(initialCount, Is.GreaterThan(collection.Count));
        }
        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5);
            var last = collection[collection.Count - 1];
            var initialCount = collection.Count;
            collection.RemoveAt(collection.Count - 1);
            Assert.AreNotEqual(last, collection[collection.Count - 1]);
            Assert.That(initialCount, Is.GreaterThan(collection.Count));
        }
        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5);
            var middle = (collection.Count / 2);
            var initialCount = collection.Count;
            var midItem = collection[middle];
            collection.RemoveAt(middle);
            Assert.AreNotEqual(midItem, collection[middle]);
            Assert.That(initialCount, Is.GreaterThan(collection.Count));
        }
        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            var collection = new Collection<string>("Ivan", "Peter");
            Assert.That(() => { collection.RemoveAt(-3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
        [Test]
        public void Test_Collection_RemoveAll()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5);
            while (collection.Count > 0)
            {
                collection.RemoveAt(0);
            }
            Assert.AreEqual(collection.Count, 0);
        }
        [Test]
        public void Test_Collection_Clear()
        {
            var collection = new Collection<int>(1, 2, 3, 4, 5);
            collection.Clear();
            Assert.AreEqual(collection.Count, 0);
        }
        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            var collection = new Collection<string>("Ivan");
            collection[0] = string.Empty;
            Assert.AreEqual(collection.ToString(), "[]");
        }
        [Test]
        public void Test_Collection_ToStringSingle()
        {
            var collection = new Collection<string>("Ivan");
            Assert.AreEqual(collection.ToString(), "[Ivan]");
        }
        [Test]
        public void Test_Collection_ToStringMultiple()
        {
            var collection = new Collection<string>("Ivan", "Peter");
            Assert.AreEqual(collection.ToString(), "[Ivan, Peter]");
        }
        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Teddy", "Gerry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            Assert.That(nestedToString,
              Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
        }

        [Test]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }
    }
}
