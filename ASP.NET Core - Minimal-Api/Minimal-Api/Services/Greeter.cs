namespace App.Services
{
    public class Greeter : IGreeter
    {
        public string Greet(DateTimeOffset time)
            => time.Hour switch
            {
                >= 5 and < 12 => "Good morning!",
                >= 12 and < 17 => "Good afternoon!",
                _ => "Good evening"
            };
    }
}
