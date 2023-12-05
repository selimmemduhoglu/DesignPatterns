using System;

// Subject Interface
public interface IRealSubject
{
    void Request();
}

// RealSubject Class
public class RealSubject : IRealSubject
{
    public void Request()
    {
        Console.WriteLine("RealSubject: Handling Request");
    }
}

// Proxy Class
public class Proxy : IRealSubject
{
    private RealSubject _realSubject;
    public void Request()
    {
        // Proxy, gerçek nesneye erişim sağlamadan önce gerekirse kontrol veya ek işlemleri yapabilir
        if (_realSubject == null)
        {
            Console.WriteLine("Proxy: Creating RealSubject");
            _realSubject = new RealSubject();
        }
        Console.WriteLine("Proxy: Forwarding Request to RealSubject");
        _realSubject.Request();
    }
}
class Program
{
    static void Main()
    {    // Proxy üzerinden gerçek nesneye erişim
        IRealSubject proxy = new Proxy();
        proxy.Request();
    }
}


/*
Proxy Pattern, bir nesne üzerindeki erişimi kontrol etmek, sınırlamak veya optimize etmek için kullanılan bir tasarım desenidir.
Bu desen, gerçek nesneye erişim sağlamadan önce bir aracı görevi gören bir proxy nesnesi ekler. Bu sayede, gerçek nesnenin
yaratılması veya özellikle bir işlem yapılması gerektiğinde proxy nesnesi devreye girebilir.*/

/*Erişim Kontrolü: Gerçek nesneye erişim sağlamadan önce belirli izinleri kontrol etmek istiyorsanız.
Optimizasyon: Gerçek nesnenin yaratılması veya yüklenmesi maliyetliyse, proxy nesnesi bu maliyeti geciktirerek sadece gerektiğinde

gerçekleştirebilir.
Sinyal Gönderme: Gerçek nesnenin bir olayı tetiklemesi gerekirse, proxy nesnesi bu sinyali algılayarak belirli işlemleri 
gerçekleştirebilir.*/

/*lazy loading yaklaşımı benimseniyor yani nesne kullanılması gereken zamnada üretiliyor veya veriliyor.*/