// Component
public interface IGraphic
{
    void Draw();
}

// Leaf
public class Circle : IGraphic
{
    public void Draw()
    {
        Console.WriteLine("Drawing Circle");
    }
}

// Leaf
public class Square : IGraphic
{
    public void Draw()
    {
        Console.WriteLine("Drawing Square");
    }
}

// Composite
public class CompositeGraphic : IGraphic
{
    private List<IGraphic> _graphics = new List<IGraphic>();

    public void Add(IGraphic graphic)
    {
        _graphics.Add(graphic);
    }

    public void Remove(IGraphic graphic)
    {
        _graphics.Remove(graphic);
    }

    public void Draw()
    {
        Console.WriteLine("Drawing Composite Graphic");
        foreach (var graphic in _graphics)
        {
            graphic.Draw();
        }
    }
}

class Program
{
    static void Main()
    {
        // Composite Pattern kullanımı

        // Leaf nesneleri oluştur
        Circle circle = new Circle();
        Square square = new Square();

        // Composite nesnesi oluştur ve leaf nesneleri ekleyerek hiyerarşiyi oluştur
        CompositeGraphic compositeGraphic = new CompositeGraphic();
        compositeGraphic.Add(circle);
        compositeGraphic.Add(square);

        // Draw metodu çağrıldığında, hem leaf nesneler hem de composite nesne içindeki diğer nesneler çizilir
        compositeGraphic.Draw();
    }
}


/*Composite Pattern, bir nesnenin tek bir nesne gibi davranmasını ve içinde hiyerarşik bir yapıdaki alt nesneleri gruplamasını 
 * sağlayan bir tasarım desenidir. Bu desen, bir nesnenin içinde tek bir nesne veya bir grup nesnenin olabileceği durumları
 * modellemek için kullanılır*/

/*Bu örnekte, IGraphic arayüzü, hem yaprak (leaf) nesneleri hem de bileşik (composite) nesneyi temsil eder. Circle ve Square
 * sınıfları, yaprak nesneleridir ve tek bir şekli temsil ederler. CompositeGraphic sınıfı ise bileşik nesneyi temsil eder
 * ve içinde başka IGraphic nesnelerini içerebilir.
 * 
 * 
 * 
CompositeGraphic sınıfının Draw metodu, hem kendi içindeki nesneleri çizer hem de bu nesnelerin içindeki diğer nesneleri
çağırarak bir hiyerarşiyi çizer. Bu sayede, hem tek bir şekli hem de bir grup şekli aynı şekilde çizebilirsiniz.
Composite Pattern, özellikle nesnelerin hiyerarşik bir şekilde örgütlendiği yapıları modellemek ve bu nesneleri tek bir
nesne gibi ele almak istediğiniz durumlarda kullanışlıdır.*/