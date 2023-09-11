using System.ComponentModel.DataAnnotations;


namespace serwer.App.DbModels
{
    class Usser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
