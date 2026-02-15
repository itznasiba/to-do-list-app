using System.ComponentModel.DataAnnotations;

namespace basic_authentication.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Required]
        public string Title {  get; set; }
        public bool IsCompleted {  get; set; }
        public string OwnerId {  get; set; }
    }
}
