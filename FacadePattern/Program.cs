
namespace FacadePattern
{
	internal class Program
	{
		//Facade: Karmaşık bir sistemi veya alt sistemleri basitleştirir.

		/*
		 * Façade tasarım deseni, karmaşık bir sistemi basit bir arayüz arkasında gizleyerek:

            Kodun okunabilirliğini ve bakımını kolaylaştırır
            Sistemler arasındaki bağımlılığı azaltır
            İş mantığını organize eder
            Kullanım karmaşıklığını azaltır
		*/

		//Facade (Cephe) tasarım deseni, karmaşık bir alt sistem için basitleştirilmiş bir arayüz sağlayan yapısal bir tasarım desenidir.
		//Bu deseni C# ve .NET kullanarak detaylı bir şekilde açıklayacağım.

		/*
		 Ne Zaman Kullanılır?

		Karmaşık bir alt sisteme basit bir arayüz sağlamak istediğinizde
		Alt sistemler arasındaki bağımlılıkları azaltmak istediğinizde
		Sistemleri katmanlara ayırmak istediğinizde
		Çok sayıda alt sistemi organize etmek istediğinizde
		*/

		/*
		 2. Facade Sınıfı: OrderFacade
		Bu sınıf, facade deseninin temel yapısını oluşturur:

		Tüm alt sistemlerin bir örneğini (instance) içerir
		ProcessOrder() metodu ile alt sistemlerin karmaşıklığını gizler
		Tek bir çağrı ile birden fazla alt sistem işlemini koordine eder

		3. İstemci Kodu (Program.cs)
		İstemci kodu, alt sistemlerin hiçbirini doğrudan kullanmaz, sadece Facade arayüzünü kullanır. Bu sayede istemci, alt sistemlerin karmaşıklığından izole edilmiş olur.*/


		static void Main(string[] args)
		{
			OrderFacade orderFacade = new OrderFacade();
			orderFacade.ProcessOrder("C1001", "Laptop", 2, 10000);
			Console.WriteLine("\n-------------------------------\n");

			orderFacade.ProcessOrder("c1002", "Telefon", 1, 5000);

			Console.ReadLine();

		}
	}
	// FACADE SINIFI: OrderFacade
	public class OrderFacade
	{
		private readonly InventorySystem _inventorySystem;
		private readonly PaymentSystem _paymentSystem;
		private readonly CustomerSystem _customerSystem;
		private readonly ShippingSystem _shippingSystem;
		private readonly NotificationSystem _notificationSystem;

		public OrderFacade()
		{
			_inventorySystem = new InventorySystem();
			_paymentSystem = new PaymentSystem();
			_customerSystem = new CustomerSystem();
			_shippingSystem = new ShippingSystem();
			_notificationSystem = new NotificationSystem();
		}

		// FACADE METODU: Tek bir metot ile tüm karmaşık işlemleri yürütüyor
		public bool ProcessOrder(string customerId, string productName, int quantity, decimal price)
		{
			Console.WriteLine($"\n===== {_customerSystem.GetCustomerName(customerId)} için sipariş işlemi başlatılıyor =====\n");

			// Müşteri doğrulama
			if (!_customerSystem.ValidateCustomer(customerId))
			{
				Console.WriteLine("Sipariş hatası: Müşteri doğrulanamadı");
				return false;
			}

			// Stok kontrolü
			if (!_inventorySystem.CheckStock(productName, quantity))
			{
				Console.WriteLine($"Sipariş hatası: {productName} için yeterli stok yok");
				return false;
			}

			// Ödeme işlemi
			decimal totalAmount = price * quantity;
			if (!_paymentSystem.ProcessPayment(customerId, totalAmount))
			{
				Console.WriteLine("Sipariş hatası: Ödeme işlemi başarısız");
				return false;
			}

			// Stok güncelleme
			_inventorySystem.UpdateStock(productName, quantity);

			// Kargo etiketi oluşturma
			string shippingLabel = _shippingSystem.CreateShippingLabel(customerId, productName, quantity);

			// Sipariş numarası oluşturma
			string orderNumber = $"ORD-{DateTime.Now.ToString("yyyyMMddHHmmss")}-{customerId}";

			// Bildirimler
			_notificationSystem.SendConfirmationEmail(customerId, orderNumber);
			_notificationSystem.SendSMS(customerId, orderNumber);

			Console.WriteLine($"\n===== Sipariş başarıyla tamamlandı: {orderNumber} =====\n");
			return true;
		}
	}


	// ALT SİSTEM SINIFI 1: Ürün Envanteri
	class InventorySystem
	{
		private Dictionary<string, int> productStock = new Dictionary<string, int>
		{
			{"Laptop",10},
			{"Telefon",20 },
			{"Tablet", 15 }
		};

		public bool CheckStock(string productName, int quantity)
		{
			Console.WriteLine($"Envanter sistemi: {productName} ürününün stok kontrolü yapılıyor.");
			if (productStock.ContainsKey(productName) && productStock[productName] >= quantity)
				return true;

			return false;
		}

		public void UpdateStock(string productName, int quantity)
		{
			Console.WriteLine($"Envanter sistemi: {productName} ürün stoku güncelleniyor");

			if (productStock.ContainsKey(productName))
			{
				productStock[productName] -= quantity;
			}
		}

	}

	// ALT SİSTEM SINIFI 2: Ödeme İşlemi
	public class PaymentSystem
	{
		public bool ProcessPayment(string customerId, decimal amount)
		{
			Console.WriteLine($"Ödeme sistemi: {customerId} müşterisi için {amount:C} tutarında ödeme işlemi yapılıyor");

			// Ödeme işlemi simülasyonu
			Random random = new Random();
			bool paymentSuccess = random.Next(0, 10) > 1; // %90 başarı oranı

			if (paymentSuccess)
			{
				Console.WriteLine("Ödeme işlemi başarılı");
				return true;
			}
			else
			{
				Console.WriteLine("Ödeme işlemi başarısız");
				return false;
			}
		}
	}


	// ALT SİSTEM SINIFI 3: Müşteri Yönetimi
	public class CustomerSystem
	{
		private Dictionary<string, string> customers = new Dictionary<string, string>
{
{ "C1001", "Ahmet Yılmaz" },
{ "C1002", "Ayşe Demir" },
{ "C1003", "Mehmet Kara" }
};

		public bool ValidateCustomer(string customerId)
		{
			Console.WriteLine($"Müşteri sistemi: {customerId} müşteri doğrulaması yapılıyor");
			return customers.ContainsKey(customerId);
		}

		public string GetCustomerName(string customerId)
		{
			if (customers.ContainsKey(customerId))
			{
				return customers[customerId];
			}
			return "Bilinmeyen Müşteri";
		}
	}

	// ALT SİSTEM SINIFI 4: Kargo Yönetimi
	public class ShippingSystem
	{
		public string CreateShippingLabel(string customerId, string productName, int quantity)
		{
			Console.WriteLine($"Kargo sistemi: {customerId} müşterisi için kargo etiketi oluşturuluyor");
			return $"KARGO-{customerId}-{DateTime.Now.Ticks}";
		}
	}

	// ALT SİSTEM SINIFI 5: Bildirim Servisi
	public class NotificationSystem
	{
		public void SendConfirmationEmail(string customerId, string orderNumber)
		{
			Console.WriteLine($"Bildirim sistemi: {customerId} müşterisine {orderNumber} sipariş onay e-postası gönderiliyor");
		}

		public void SendSMS(string customerId, string orderNumber)
		{
			Console.WriteLine($"Bildirim sistemi: {customerId} müşterisine {orderNumber} sipariş onay SMS'i gönderiliyor");
		}
	}



}