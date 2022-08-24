## Rate Limiter AspNet 7

Microsoft.AspNetCore.RateLimiting ' i projemize dahil edelim.


### Concurrency Limit

Uygulamamıza eş zamanlı gelen istekleri sınırlandırmak istiyorsak bu yapılandırmayı kullanırız.

Concurrency limiter Options nesnesi önemli parametreler içerir.

1. Permit Limit : Aynı anda işlenebilecek request sayısıdır.
2. Queue Processing Order : Aynı anda işlenen requestlerin kuyruk yapısının çalışma mantığını temsil eder. 
   - Oldest First (LIFO = Last in first out)
   - Newest First (FIFO = First in first out)
3. Queu Limit : Kuyruğun kapasitesini temsil eder.

Queu dolduğunda Reject senaryosunda Defaul olarak Status 503 Service Unavailable yani 503 işlenir. Customize edilebilir.

429 Status Code Too Many Requests anlamına gelirken 503 Status Code ise daha genel bir anlam içerir.

<p style="color:red">Rate Limiter Middleware aktif edildiğinde belirlenen kurallar tüm endpointler için geçerli olur muaf tutmak istediğimiz endpointleri NoLimiter Extension methodu ile işaretleriz.</p>

### Token Bucket Limit

Bir bucket yapısı vardır, İstek geldiğinde bir token harcarız.

 Parametreler

1. Token Limit : Bucket içerisinde olabilecek maksimum token sayısıdır.
2. Queue Processing Order : Token yönetimi yapılırken kuyruğun çalışma mantığı
3. Queue Limit : İşlenmek için bekleyen maksimum token sayısıdır.
4. Replenishment Period : Bucket içerisindeki tokenların tekrar doldurulması için gerekli süredir.
5. Token Per Period : Resplenishment Period geldiğinde Bucket'a kaç Token koyulacağını belirler.
6. Auto Replenishment : Bool değer alır; False ise manuel bir şekilde Bucket a token ikmali yapılır. True ise Otomatik Replenishment Period geldiğinde bucket doldurulur.


