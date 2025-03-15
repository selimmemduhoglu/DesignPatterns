using System;

// Ürün arayüzü
public interface IProduct
{
    void Display();
}

// ConcreteProduct sınıfları
public class ConcreteProductA : IProduct
{
    public void Display()
    {
        Console.WriteLine("Product A");
    }
}

public class ConcreteProductB : IProduct
{
    public void Display()
    {
        Console.WriteLine("Product B");
    }
}

// Creator (Factory) arayüzü
public interface IProductFactory
{
    IProduct CreateProduct();
}

// ConcreteCreator sınıfları
public class ConcreteProductAFactory : IProductFactory
{
    public IProduct CreateProduct()
    {
        return new ConcreteProductA();
    }
}

public class ConcreteProductBFactory : IProductFactory
{
    public IProduct CreateProduct()
    {
        return new ConcreteProductB();
    }
}

class Program
{
    static void Main()
    {
        // Factory Method Pattern kullanımı

        // Birinci ürünü oluştur
        IProductFactory factoryA = new ConcreteProductAFactory();
        IProduct productA = factoryA.CreateProduct();
        productA.Display();

        // İkinci ürünü oluştur
        IProductFactory factoryB = new ConcreteProductBFactory();
        IProduct productB = factoryB.CreateProduct();
        productB.Display();
    }
}

/*Bu örnekte, IProduct arayüzü, ürünleri temsil eder ve Display metoduyla birlikte gelir. ConcreteProductA ve ConcreteProductB sınıfları,
 * bu arayüzü uygular ve kendi özel Display implementasyonlarına sahiptir.
IProductFactory arayüzü, ürünleri oluşturan Factory Method'u tanımlar. ConcreteProductAFactory ve ConcreteProductBFactory sınıfları,
bu arayüzü uygular ve kendi CreateProduct metodlarını sağlarlar, böylece hangi ürünün oluşturulacağına karar verirler.
Main metodu, Factory Method Pattern'ın kullanımını gösterir. İlk olarak ConcreteProductA için bir fabrika oluşturulur, ardından CreateProduct
metodu çağrılarak ConcreteProductA örneği oluşturulur ve ekrana yazdırılır. Aynı şekilde ConcreteProductB için bir fabrika oluşturulur
ve bu sefer de ConcreteProductB örneği oluşturulup ekrana yazdırılır.
Bu tasarım deseni, nesne oluşturma sürecini alt sınıflara bırakarak esnek ve genişletilebilir bir kod yapısı sağlar.*/