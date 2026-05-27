// =============================================================================
// TASK 02 — Covariance & Contravariance
// =============================================================================
//
// DIFFICULTY: L4 (advanced)            TARGET TIME: < 10 minutes
//
// THIS PROGRAM DOES NOT COMPILE. Your job is to make it compile by applying
// generic variance correctly — WITHOUT changing the concrete classes or the
// intent of Main.
//
// THINGS TO SCRUTINISE:
//   1. IProducer<T> only ever RETURNS a T. What variance modifier does that
//      allow, and why does `IProducer<Animal> p = new DogProducer()` fail?
//   2. IConsumer<T> only ever ACCEPTS a T. What variance modifier does that
//      allow, and why does `IConsumer<Dog> c = new AnimalConsumer()` fail?
//   3. There is ALSO a runtime bug that compiles fine even after you add the
//      variance modifiers: array covariance. Find the line that will throw
//      ArrayTypeMismatchException at run time and fix it.
//
// Reminder: `out` = covariant (output positions), `in` = contravariant
// (input positions). A type used only in output can vary one way; a type used
// only as input can vary the other way.
// =============================================================================

using System;

interface IProducer<T>          // <-- only produces T
{
    T Produce();
}

interface IConsumer<T>          // <-- only consumes T
{
    void Consume(T item);
}

class Animal { public string Name = "animal"; }
class Dog : Animal { public Dog() { Name = "dog"; } }
class Cat : Animal { public Cat() { Name = "cat"; } }

class DogProducer : IProducer<Dog>
{
    public Dog Produce() => new Dog();
}

class AnimalConsumer : IConsumer<Animal>
{
    public void Consume(Animal a) => Console.WriteLine($"consumed: {a.Name}");
}

public class Program
{
    public static void Main()
    {
        // Covariance required: IProducer<Dog> should be usable as IProducer<Animal>.
        IProducer<Animal> producer = new DogProducer();
        Animal produced = producer.Produce();
        Console.WriteLine($"produced: {produced.Name}");

        // Contravariance required: IConsumer<Animal> should be usable as IConsumer<Dog>.
        IConsumer<Dog> consumer = new AnimalConsumer();
        consumer.Consume(new Dog());

        // Array covariance: this compiles, but one of these assignments throws
        // ArrayTypeMismatchException at run time. Make the storage safe.
        Animal[] animals = new Dog[2];
        animals[0] = new Cat();
        Console.WriteLine($"stored: {animals[0].Name}");
    }
}
