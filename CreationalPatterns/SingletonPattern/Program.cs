public class Singleton
{
    // Tek bir örneği saklamak için static bir alan
    private static Singleton _instance;

    // Kullanıcıların bu sınıftan bir örneği oluşturmasını önlemek için private bir kurucu metod
    private Singleton()
    {
        // Kurucu metod içinde gerekirse başka ayarlamalar yapılabilir
    }

    // Tek bir örnek sağlamak için public bir metot
    public static Singleton Instance
    {
        get
        {
            // Eğer örnek daha önce oluşturulmamışsa oluştur, aksi takdirde mevcut örneği döndür
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }

    // Singleton sınıfının diğer metodları buraya eklenir.
    public void DoSomething()
    {
        Console.WriteLine("Singleton instance is doing something.");
    }
}

class Program
{
    static void Main()
    {
        // Singleton sınıfından tek bir örneği almak
        Singleton singletonInstance1 = Singleton.Instance;
        Singleton singletonInstance2 = Singleton.Instance;

        // İki örnek aynı örneği referans eder, çünkü Singleton deseni kullanılmıştır
        Console.WriteLine(singletonInstance1 == singletonInstance2);  // true

        // Singleton örneği üzerinde işlem yapma
        singletonInstance1.DoSomething();
    }
}


/*Bu örnekte Singleton sınıfı, yalnızca bir örneğe sahip olmasını sağlamak için kullanılan bir Singleton tasarım deseni örneğidir.
 * Instance adında bir static özellik (property) kullanılarak tek bir örnek sağlanır. Bu özellik, her çağrıldığında örneği oluşturur
 * veya zaten bir örnek varsa mevcut örneği döndürür.
Bu desenin amacı, bir sınıfın yalnızca bir örneğine sahip olmasını sağlamaktır. Bu genellikle uygulamada tek bir noktadan verilere erişmek
veya belirli bir hizmeti paylaşmak gibi durumlar için kullanılır.*/