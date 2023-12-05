using System;

// Prototype arayüzü
public interface ICloneablePrototype
{
    ICloneablePrototype Clone();
    void Display();
}

// ConcretePrototype sınıfları
public class ConcretePrototypeA : ICloneablePrototype
{
    public int Number { get; set; }

    public ICloneablePrototype Clone()
    {
        // MemberwiseClone metodu, nesnenin shallow (yüzeysel) bir kopyasını oluşturur
        return (ICloneablePrototype)MemberwiseClone();
    }

    public void Display()
    {
        Console.WriteLine($"ConcretePrototypeA: {Number}");
    }
}

public class ConcretePrototypeB : ICloneablePrototype
{
    public string Text { get; set; }

    public ICloneablePrototype Clone()
    {
        return (ICloneablePrototype)MemberwiseClone();
    }

    public void Display()
    {
        Console.WriteLine($"ConcretePrototypeB: {Text}");
    }
}

// Client sınıfı
public class Client
{
    public ICloneablePrototype CloneAndDisplay(ICloneablePrototype prototype)
    {
        // Prototipin kopyasını oluştur ve ekrana yazdır
        ICloneablePrototype cloned = prototype.Clone();
        cloned.Display();
        return cloned;
    }
}

class Program
{
    static void Main()
    {
        // Prototype Pattern kullanımı

        // İlk örneği oluştur
        ConcretePrototypeA prototypeA = new ConcretePrototypeA { Number = 42 };

        // Client, prototipin kopyasını oluşturur ve ekrana yazdırır
        Client client = new Client();
        ICloneablePrototype clonedA = client.CloneAndDisplay(prototypeA);

        // İkinci örneği oluştur
        ConcretePrototypeB prototypeB = new ConcretePrototypeB { Text = "Hello, Prototype!" };

        // Client, prototipin kopyasını oluşturur ve ekrana yazdırır
        ICloneablePrototype clonedB = client.CloneAndDisplay(prototypeB);
    }
}


/*Bu örnekte, ICloneablePrototype arayüzü, Clone ve Display metotlarını tanımlar. ConcretePrototypeA ve ConcretePrototypeB
 * sınıfları bu arayüzü uygular ve kendi kopyalama ve gösterme işlemlerini gerçekleştirir. Client sınıfı, prototipin
 * kopyasını oluşturan bir metod içerir.
Main metodunda, ilk olarak bir ConcretePrototypeA ve ardından bir ConcretePrototypeB örneği oluşturulur. Client sınıfı,
bu prototiplerin kopyalarını oluşturarak ekrana yazdırır.
Clone metodu, MemberwiseClone metodunu kullanarak nesnenin shallow (yüzeysel) bir kopyasını oluşturur.


Bu, nesnenin 
referans türü özelliklerinin aynı referansları paylaşmasına rağmen, değer türü özelliklerinin farklı olmasını sağlar.

Prototype Pattern, özellikle nesne oluşturma sürecindeki maliyeti minimize etmek ve performansı artırmak istediğiniz 
durumlarda kullanışlıdır.*/