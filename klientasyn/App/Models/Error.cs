namespace Klient.App.Models
{
    internal class Error
    {
        public static bool IsError { get; set; }
        public static string ExceptionMsg { get; set; } = "";
    }
}
