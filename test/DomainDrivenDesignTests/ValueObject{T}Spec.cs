using CWiz.DomainDrivenDesign;
using System.Collections.Generic;
using Xunit;

namespace DomainDrivenDesignTests
{
    public class Person : ValueObject<Person>
    {
        public Person(string firstName, string lastName, int age, List<string> phoneNumbers)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            PhoneNumbers = phoneNumbers;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
        public List<string> PhoneNumbers { get; }
    }

    public class ValueObject_T_Spec
    {
        [Fact]
        public void Two_different_instances_of_a_value_object_with_same_values_should_be_equal()
        {
            var fname = "Xavier";
            var lname = "John";
            var age = 99;
            var phoneNumbers = new List<string>() { "123-456-7890", "234-567-8901" };
            var instance1 = new Person(fname, lname, age, phoneNumbers);
            var instance2 = new Person(fname, lname, age, phoneNumbers);
            Assert.Equal(instance1, instance2);
        }

        [Fact]
        public void Two_value_object_with_different_values_should_be_different()
        {
            var phoneNumbers = new List<string>() { "123-456-7890", "234-567-8901" };
            var instance1 = new Person("Xavier", "John", 99, phoneNumbers);
            var instance2 = new Person("Space", "Ghost", 99, phoneNumbers);
            Assert.NotEqual(instance1, instance2);
        }

        [Fact]
        public void Two_value_objects_with_different_only_in_the_collection_should_be_different()
        {
            var fname = "Xavier";
            var lname = "John";
            var age = 99;
            var phoneNumbers1 = new List<string>() { "123-456-7890", "234-567-8901" };
            var phoneNumbers2 = new List<string>() { "123-456-7890" };

            var instance1 = new Person(fname, lname, age, phoneNumbers1);
            var instance2 = new Person(fname, lname, age, phoneNumbers2);
            Assert.NotEqual(instance1, instance2);
        }
    }
}