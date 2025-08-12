namespace BookWebApp.Business.Utilities
{
    public class Result
    {
        // Bu sınıf hata yönetimi içindir. Hata yönetimi Business Katmanının bir parçasıdır.
        public bool Success { get; }
        public string? Message { get; }
        public int? ID { get; }

        protected Result(bool success, string? message = null, int? id = 0)
        {
            Success = success;
            Message = message;
            ID = id;
        }

        public static Result Ok(string message = "İşlem başarılı!", int? id = 0)
        {
            return new Result(true, message, id);
        }

        public static Result Fail(string message)
        {  
            return new Result(false, message); 
        }

        
    }
}

/*
Result kullanım örneği

var result = userService.AddUser(new AppUser { UserName = "testuser", Email = "test@mail.com" });

if (result.Success)
    Console.WriteLine(result.Message);
else
    Console.WriteLine("Hata: " + result.Message);
*/