using System;

// Ürün sınıfı
public class Product
{
    public string PartA { get; set; }
    public string PartB { get; set; }
    public string PartC { get; set; }

    public void Display()
    {
        Console.WriteLine($"Parts: {PartA}, {PartB}, {PartC}");
    }
}

// Builder arayüzü
public interface IBuilder
{
    void BuildPartA();
    void BuildPartB();
    void BuildPartC();
    Product GetResult();
}

// ConcreteBuilder sınıfları
public class ConcreteBuilder1 : IBuilder
{
    private Product _product = new Product();

    public void BuildPartA()
    {
        _product.PartA = "Part A1";
    }

    public void BuildPartB()
    {
        _product.PartB = "Part B1";
    }

    public void BuildPartC()
    {
        _product.PartC = "Part C1";
    }

    public Product GetResult()
    {
        return _product;
    }
}

public class ConcreteBuilder2 : IBuilder
{
    private Product _product = new Product();

    public void BuildPartA()
    {
        _product.PartA = "Part A2";
    }

    public void BuildPartB()
    {
        _product.PartB = "Part B2";
    }

    public void BuildPartC()
    {
        _product.PartC = "Part C2";
    }

    public Product GetResult()
    {
        return _product;
    }
}

// Director sınıfı
public class Director
{
    public void Construct(IBuilder builder)
    {
        builder.BuildPartA();
        builder.BuildPartB();
        builder.BuildPartC();
    }
}

class Program
{
    static void Main()
    {
        // Builder Pattern kullanımı

        Director director = new Director();

        // ConcreteBuilder1 ile ürünü oluştur
        IBuilder builder1 = new ConcreteBuilder1();
        director.Construct(builder1);
        Product product1 = builder1.GetResult();
        product1.Display();

        // ConcreteBuilder2 ile ürünü oluştur
        IBuilder builder2 = new ConcreteBuilder2();
        director.Construct(builder2);
        Product product2 = builder2.GetResult();
        product2.Display();
    }
}


/*Bu örnekte, Product sınıfı bir ürünü temsil eder. IBuilder arayüzü, ürünün parçalarını oluşturan metodları tanımlar. 
 * ConcreteBuilder1 ve ConcreteBuilder2 sınıfları, bu arayüzü uygular ve kendi ürünlerini oluşturacak şekilde metodları implemente eder. Director sınıfı, 
 * bir IBuilder nesnesini kullanarak ürünü oluşturan bir metod içerir.
Bu tasarım deseni, farklı temsil biçimlerine sahip nesneleri oluşturmak için kullanılabilir ve aynı Director nesnesi, 
farklı ConcreteBuilder sınıflarıyla birlikte çalışarak farklı türde ürünlerin oluşturulmasını sağlar.*/