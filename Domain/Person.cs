using System;

namespace EfCoreValueObjects;

public abstract class Person
{
    protected Person()
    {
    }

    public Person(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
    public PersonTypes Type { get; protected set; }
}

public enum PersonTypes
{
    Employee = 1,
    Student = 2,
}

public class Employee : Person
{
    public Employee(Guid id)
        : base(id)
    {
    }
}

public class Student : Person
{
    public Student(Guid id)
        : base(id)
    {
    }
}