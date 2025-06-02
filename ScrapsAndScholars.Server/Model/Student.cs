using System;

// Model for a student to save their information.
namespace ScrapsAndScholars.Server.Model
{
  public class Student
  {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Credits { get; set; }
        public string Address { get; set; }

        public List<FoodPost> Posts { get; set; } = new();
    }
}
