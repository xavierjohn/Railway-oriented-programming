using System.Collections.Generic;
using CWiz.DomainDrivenDesign;
using Xunit;

namespace DomainDrivenDesignTests
{
    public class ValueObject_T_Spec
    {
        [Fact]
        [Trait("Category", "ValueObjectWithoutCollection")]
        public void Equal_should_return_true_for_two_different_instances_with_same_values()
        {
            var fname = "Xavier";
            var lname = "John";
            var age = 99;
            var instance1 = new ValueObjectWithoutCollection(fname, lname, age);
            var instance2 = new ValueObjectWithoutCollection(fname, lname, age);
            Assert.Equal(instance1, instance2);
        }

        [Fact]
        [Trait("Category", "ValueObjectWithoutCollection")]
        public void X3DX3D_should_return_true_for_two_different_instances_with_same_value()
        {
            var fname = "Xavier";
            var lname = "John";
            var age = 99;
            var instance1 = new ValueObjectWithoutCollection(fname, lname, age);
            var instance2 = new ValueObjectWithoutCollection(fname, lname, age);
            Assert.True(instance1 == instance2);
        }

        [Fact]
        [Trait("Category", "ValueObjectWithoutCollection")]
        public void NotEqual_should_return_true_when_values_are_different()
        {
            var instance1 = new ValueObjectWithoutCollection("Xavier", "John", 99);
            var instance2 = new ValueObjectWithoutCollection("Space", "Ghost", 99);
            Assert.NotEqual(instance1, instance2);
        }

        [Fact]
        [Trait("Category", "ValueObjectWithoutCollection")]
        public void ne_should_return_true_when_values_are_different()
        {
            var instance1 = new ValueObjectWithoutCollection("Xavier", "John", 99);
            var instance2 = new ValueObjectWithoutCollection("Space", "Ghost", 99);
            Assert.True(instance1 != instance2);
        }


        // Collections
        [Fact]
        [Trait("Category", "ValueObjectWithCollection")]

        public void Equal_should_return_true_for_two_different_collection_instances_with_same_values()
        {
            var phoneNumbers1 = new List<string> {"123-456-7890", "234-567-8901"};
            var phoneNumbers2 = new List<string> {"123-456-7890", "234-567-8901"};
            var instance1 = new ValueObjectWithCollection(phoneNumbers1);
            var instance2 = new ValueObjectWithCollection(phoneNumbers2);
            Assert.Equal(instance1, instance2);
        }

        [Fact]
        [Trait("Category", "ValueObjectWithCollection")]
        public void X3DX3D_should_return_true_for_two_different_collection_instances_with_same_value()
        {
            var phoneNumbers1 = new List<string> {"123-456-7890", "234-567-8901"};
            var phoneNumbers2 = new List<string> {"123-456-7890", "234-567-8901"};
            var instance1 = new ValueObjectWithCollection(phoneNumbers1);
            var instance2 = new ValueObjectWithCollection(phoneNumbers2);
            Assert.True(instance1 == instance2);
        }

        [Fact]
        [Trait("Category", "ValueObjectWithCollection")]
        public void NotEqual_should_return_true_when_collections_are_different()
        {
            var phoneNumbers1 = new List<string> {"123-456-7890", "234-567-8901"};
            var phoneNumbers2 = new List<string> {"123-456-7890"};
            var instance1 = new ValueObjectWithCollection(phoneNumbers1);
            var instance2 = new ValueObjectWithCollection(phoneNumbers2);
            Assert.NotEqual(instance1, instance2);
        }


        [Fact]
        [Trait("Category", "ValueObjectWithCollection")]
        public void ne_should_return_true_when_collections_are_different()
        {
            var phoneNumbers1 = new List<string> {"123-456-7890", "234-567-8901"};
            var phoneNumbers2 = new List<string> {"123-456-7890"};
            var instance1 = new ValueObjectWithCollection(phoneNumbers1);
            var instance2 = new ValueObjectWithCollection(phoneNumbers2);
            Assert.True(instance1 != instance2);
        }
    }

    public class ValueObjectWithoutCollection : ValueObject<ValueObjectWithoutCollection>
    {
        public ValueObjectWithoutCollection(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
    }

    public class ValueObjectWithCollection : ValueObject<ValueObjectWithCollection>
    {
        public ValueObjectWithCollection(List<string> phoneNumbers)
        {
            PhoneNumbers = phoneNumbers;
        }

        public List<string> PhoneNumbers { get; }
    }
}