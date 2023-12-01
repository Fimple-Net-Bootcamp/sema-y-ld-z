// See https://aka.ms/new-console-template for more information

public interface IYazdırılabilir
{
    void Yazdır();
}


//Yazılı Eser Üst Sınıfı
public class YazılıEser : IYazdırılabilir
{
    public string Ad { get; set; }
    public string Yazar { get; set; }
    public int YayınYılı { get; set; }
    public int KitapID { get; set; }

    public YazılıEser(string ad, string yazar, int yayınYılı, int kitapID)
    {
        Ad = ad;
        Yazar = yazar;
        YayınYılı = yayınYılı;
        KitapID = kitapID;
    }

    public virtual void Yazdır()
    {
        Console.WriteLine($"Ad: {Ad}, Yazar: {Yazar}, Yayın Yılı: {YayınYılı}, Kitap ID: {KitapID}");
    }
}

// Kitap sınıfı
public class Kitap : YazılıEser
{
    public Kitap(string ad, string yazar, int yayınYılı, int kitapID)
        : base(ad, yazar, yayınYılı, kitapID)
    {
    }
}

// Üye sınıfı
public class Üye : IYazdırılabilir
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public int ÜyelikNumarası { get; set; }
    public List<Kitap> AldığıKitaplar { get; set; }

    public Üye(string ad, string soyad, int üyelikNumarası)
    {
        Ad = ad;
        Soyad = soyad;
        ÜyelikNumarası = üyelikNumarası;
        AldığıKitaplar = new List<Kitap>();
    }

    public void Yazdır()
    {
        Console.WriteLine($"Ad: {Ad}, Soyad: {Soyad}, Üyelik Numarası: {ÜyelikNumarası}");
        Console.WriteLine("Aldığı Kitaplar:");
        foreach (var kitap in AldığıKitaplar)
        {
            kitap.Yazdır();
        }
    }
}

// Kütüphane sınıfı
public class Kütüphane
{
    private List<Kitap> Kitaplar { get; set; }
    private List<Üye> Üyeler { get; set; }

    public Kütüphane()
    {
        Kitaplar = new List<Kitap>();
        Üyeler = new List<Üye>();
    }

    public void KitapEkle(Kitap kitap)
    {
        Kitaplar.Add(kitap);
        Console.WriteLine($"{kitap.Ad} adlı kitap kütüphaneye eklendi.");
    }

    public void KitapSil(Kitap kitap)
    {
        if (Kitaplar.Contains(kitap))
        {
            Kitaplar.Remove(kitap);
            Console.WriteLine($"{kitap.Ad} adlı kitap kütüphaneden silindi.");
        }
        else
        {
            Console.WriteLine($"{kitap.Ad} adlı kitap kütüphanede bulunamadı.");
        }
    }

    public void ÜyeEkle(Üye üye)
    {
        Üyeler.Add(üye);
        Console.WriteLine($"{üye.Ad} {üye.Soyad} adlı üye kütüphaneye eklendi.");
    }

    public void ÜyeSil(Üye üye)
    {
        if (Üyeler.Contains(üye))
        {
            Üyeler.Remove(üye);
            Console.WriteLine($"{üye.Ad} {üye.Soyad} adlı üye kütüphaneden silindi.");
        }
        else
        {
            Console.WriteLine($"{üye.Ad} {üye.Soyad} adlı üye kütüphanede bulunamadı.");
        }
    }

    public void KitapVer(Üye üye, Kitap kitap)
    {
        if (Üyeler.Contains(üye) && Kitaplar.Contains(kitap))
        {
            üye.AldığıKitaplar.Add(kitap);
            Console.WriteLine($"{üye.Ad} {üye.Soyad} adlı üyeye {kitap.Ad} adlı kitap verildi.");
        }
        else
        {
            Console.WriteLine($"Başarısız. Üye veya kitap bulunamadı.");
        }
    }

    public void KitapİadeAl(Üye üye, Kitap kitap)
    {
        if (Üyeler.Contains(üye) && Kitaplar.Contains(kitap) && üye.AldığıKitaplar.Contains(kitap))
        {
            üye.AldığıKitaplar.Remove(kitap);
            Console.WriteLine($"{üye.Ad} {üye.Soyad} adlı üyeden {kitap.Ad} adlı kitap iade alındı.");
        }
        else
        {
            Console.WriteLine($"Başarısız. Üye veya kitap bulunamadı.");
        }
    }
}

class Program
{
    static void Main()
    {
        // Kütüphane oluşturma
        Kütüphane kütüphane = new Kütüphane();

        // Kitap örnekleri
        Kitap kitap1 = new Kitap("Midnight Library", "Matt Haig", 2020, 1);
        Kitap kitap2 = new Kitap("1984", "George Orwell", 1949, 2);
        Kitap kitap3 = new Kitap("To Kill a Mockingbird","Harper Lee", 1960, 3);

        // Üye örneği oluşturma
        Üye üye1 = new Üye("Sema", "Yıldız", 1252);

        // Kitap ve üyeyi kütüphaneye ekleme
        kütüphane.KitapEkle(kitap1);
        kütüphane.KitapEkle(kitap2);
        kütüphane.KitapEkle(kitap3);
        kütüphane.ÜyeEkle(üye1);

        // Kitap ödünç verme ve iade alma işlemleri
        kütüphane.KitapVer(üye1, kitap1);
        kütüphane.KitapVer(üye1, kitap2);
        kütüphane.KitapVer(üye1, kitap3);
        kütüphane.KitapİadeAl(üye1, kitap1);

        // Üye bilgilerini yazdırma
        üye1.Yazdır();
    }
}