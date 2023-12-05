using System;

// Implementor
public interface IColor
{
    void FillColor();
}

// Concrete Implementors
public class RedColor : IColor
{
    public void FillColor()
    {
        Console.WriteLine("Filling with Red Color");
    }
}

public class GreenColor : IColor
{
    public void FillColor()
    {
        Console.WriteLine("Filling with Green Color");
    }
}

// Abstraction
public abstract class Shape
{
    protected IColor color;

    protected Shape(IColor color)
    {
        this.color = color;
    }

    public abstract void Draw();
}

// Refined Abstraction
public class Circle : Shape
{
    public Circle(IColor color) : base(color)
    {
    }

    public override void Draw()
    {
        Console.Write("Drawing Circle - ");
        color.FillColor();
    }
}

public class Square : Shape
{
    public Square(IColor color) : base(color)
    {
    }

    public override void Draw()
    {
        Console.Write("Drawing Square - ");
        color.FillColor();
    }
}

class Program
{
    static void Main()
    {
        // Bridge Pattern kullanımı

        // Implementor sınıfları oluştur
        IColor redColor = new RedColor();
        IColor greenColor = new GreenColor();

        // Abstraction sınıfları oluştur ve her birini farklı bir Implementor ile bağla
        Shape redCircle = new Circle(redColor);
        Shape greenSquare = new Square(greenColor);

        // Her bir Abstraction'ı çiz
        redCircle.Draw();
        greenSquare.Draw();
    }
}


/*Bridge Pattern, soyutlamayı ve uygulamayı birbirinden ayırarak her iki hiyerarşiyi de bağımsız olarak değiştirmeyi sağlayan
 * bir tasarım desenidir. Bu desen, çoklu kalıtımın getirdiği karmaşıklığı azaltmak ve esneklik sağlamak amacıyla kullanılır.*/