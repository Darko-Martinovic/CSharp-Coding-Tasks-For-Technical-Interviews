// =============================================================================
// SOLUTION 02 — Covariance & Contravariance
// =============================================================================

using System;

// FIX #1: `out T` makes IProducer<T> COVARIANT. T appears only in output
//         (return) position, so IProducer<Dog> is assignable to IProducer<Animal>.
interface IProducer<out T>
{
    T Produce();
}

// FIX #2: `in T` makes IConsumer<T> CONTRAVARIANT. T appears only in input
//         (parameter) position, so IConsumer<Animal> is assignable to IConsumer<Dog>.
interface IConsumer<in T>
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
        // Now legal thanks to covariance.
        IProducer<Animal> producer = new DogProducer();
        Animal produced = producer.Produce();
        Console.WriteLine($"produced: {produced.Name}");

        // Now legal thanks to contravariance.
        IConsumer<Dog> consumer = new AnimalConsumer();
        consumer.Consume(new Dog());

        // FIX #3: Array covariance lets `Dog[]` be referenced as `Animal[]`, but
        //         storing a Cat into a backing Dog[] throws ArrayTypeMismatchException
        //         at run time. If you truly need to store any Animal, allocate an
        //         Animal[] (or use List<Animal>); arrays are covariant but NOT safe.
        Animal[] animals = new Animal[2];
        animals[0] = new Cat();
        Console.WriteLine($"stored: {animals[0].Name}");
    }
}
