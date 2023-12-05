using System;

// Component Interface
public interface ICoffee
{
    string GetDescription();
    double Cost();
}

// ConcreteComponent
public class SimpleCoffee : ICoffee
{
    public string GetDescription()
    {
        return "Simple Coffee";
    }

    public double Cost()
    {
        return 5.0;
    }
}

// Decorator
public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual string GetDescription()
    {
        return _coffee.GetDescription();
    }

    public virtual double Cost()
    {
        return _coffee.Cost();
    }
}

// ConcreteDecorator
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee)
    {
    }

    public override string GetDescription()
    {
        return $"{_coffee.GetDescription()}, Milk";
    }

    public override double Cost()
    {
        return _coffee.Cost() + 2.0;
    }
}

// ConcreteDecorator
public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee)
    {
    }

    public override string GetDescription()
    {
        return $"{_coffee.GetDescription()}, Sugar";
    }

    public override double Cost()
    {
        return _coffee.Cost() + 1.0;
    }
}

class Program
{
    static void Main()
    {
        // Decorator Pattern kullanımı

        // Basit bir kahve siparişi
        ICoffee simpleCoffee = new SimpleCoffee();
        Console.WriteLine($"Description: {simpleCoffee.GetDescription()}, Cost: {simpleCoffee.Cost()}");

        // Süt eklenmiş kahve siparişi
        ICoffee milkCoffee = new MilkDecorator(simpleCoffee);
        Console.WriteLine($"Description: {milkCoffee.GetDescription()}, Cost: {milkCoffee.Cost()}");

        // Şeker eklenmiş süt eklenmiş kahve siparişi
        ICoffee sugarMilkCoffee = new SugarDecorator(milkCoffee);
        Console.WriteLine($"Description: {sugarMilkCoffee.GetDescription()}, Cost: {sugarMilkCoffee.Cost()}");
    }
}


/*Bu örnekte, ICoffee arayüzü, kahve nesnelerinin temel yapısını tanımlar. SimpleCoffee sınıfı bu arayüzü uygular ve basit bir kahve temsil eder.
 * 
CoffeeDecorator sınıfı, dekoratörlerin temelini oluşturur ve ICoffee arayüzünü uygular. Bu sınıfın alt sınıfları, ek işlevselliği
ekler ve temel kahve nesnesini sarmalar.
MilkDecorator ve SugarDecorator sınıfları, bu dekoratörlerden bazılarıdır. Her biri, temel kahve nesnesine ek özellikler 
(süt veya şeker) ekler.
Program sınıfında ise bu dekoratörleri kullanarak kahve siparişleri oluşturulur ve her birinin açıklaması ve maliyeti ekrana
yazdırılır.
Decorator Pattern, özellikle bir nesneye dinamik olarak davranış eklemek veya kaldırmak istediğiniz durumlarda kullanışlıdır.
Yeni özellikler eklemek istediğinizde, mevcut sınıf yapısını değiştirmek yerine dekoratörleri kullanabilirsiniz.*/