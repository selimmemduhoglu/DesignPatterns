using System;

// Target Interface
public interface IMediaPlayer
{
    void Play(string fileType, string fileName);
}

// Adaptee Class
public class AudioPlayer
{
    public void PlayMp3(string fileName)
    {
        Console.WriteLine($"Playing mp3 file: {fileName}");
    }
}

// Adapter Class
public class MediaAdapter : IMediaPlayer
{
    private AudioPlayer _audioPlayer;

    public MediaAdapter(AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
    }

    public void Play(string fileType, string fileName)
    {
        if (fileType.ToLower() == "mp3")
        {
            _audioPlayer.PlayMp3(fileName);
        }
        else
        {
            Console.WriteLine($"Unsupported media type: {fileType}");
        }
    }
}

// Client
public class MusicPlayer
{
    private IMediaPlayer _mediaPlayer;

    public MusicPlayer(IMediaPlayer mediaPlayer)
    {
        _mediaPlayer = mediaPlayer;
    }

    public void PlayMusic(string fileType, string fileName)
    {
        _mediaPlayer.Play(fileType, fileName);
    }
}

class Program
{
    static void Main()
    {
        // Adapter Pattern kullanımı

        // Adaptee olan AudioPlayer sınıfı
        AudioPlayer audioPlayer = new AudioPlayer();

        // Adapter sınıfı ile AudioPlayer'ı IMediaPlayer arayüzüne uydur
        IMediaPlayer mediaAdapter = new MediaAdapter(audioPlayer);

        // Client olan MusicPlayer sınıfı, adapte edilmiş arayüzü kullanır
        MusicPlayer musicPlayer = new MusicPlayer(mediaAdapter);

        // MP3 dosyasını çal
        musicPlayer.PlayMusic("mp3", "song.mp3");

        // Diğer türdeki dosyayı çal (desteklenmiyor)
        musicPlayer.PlayMusic("mp4", "video.mp4");
    }
}

/*
Adapter Pattern, iki farklı arayüzü birleştirmek veya bir arayüzü diğerine dönüştürmek için kullanılan bir tasarım desenidir.
Bu desen, uyumsuz sınıfları bir araya getirmek veya mevcut bir sınıfın arayüzünü değiştirmek amacıyla kullanılır. 
Aşağıda, bir örnek ile bu deseni anlatalım.
Örnek olarak, bir müzik çalma uygulamasının MediaPlayer adında bir sınıfı olduğunu ve bu sınıfın sadece mp3 dosyalarını
çalabildiğini düşünelim. Ancak, uygulama içinde farklı türlerde müzik dosyalarını çalabilen yeni bir sınıf eklemek istiyoruz.
İşte bu durumda Adapter Pattern devreye girebilir.*/


/*Bu örnekte, IMediaPlayer arayüzü bir target (hedef) arayüzü olarak belirlenmiştir. AudioPlayer sınıfı ise Adaptee sınıfını
 * temsil eder ve sadece mp3 dosyalarını çalabilir. Ardından, MediaAdapter sınıfı, IMediaPlayer arayüzünü implemente ederek
 * AudioPlayer'ı bu arayüzle uyumlu hale getirir. Son olarak, MusicPlayer sınıfı, IMediaPlayer arayüzünü kullanarak farklı
 * medya türlerini çalmak için MediaAdapter'ı kullanır.
Adapter Pattern, var olan bir sisteme yeni bir özellik eklemek veya uyumsuz sınıfları birleştirmek amacıyla sıklıkla kullanılır.
Bu desen sayesinde mevcut sınıflar, beklenen arayüzü sağlamayan başka sınıflarla birlikte çalışabilir.*/