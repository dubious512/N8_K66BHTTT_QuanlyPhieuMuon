using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWeb_MVC.Models
{
    public class Ticket
    {
        [Key] 
        public int ID {  get; set; }
        [Column(TypeName ="nvarchar(100)")]
        [DisplayName("Mã phiếu")]
        [Required(ErrorMessage = "Không được để trống")]
        public string ticket_id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Mã người dùng")]
        [Required(ErrorMessage = "Không được để trống")]
        public string user_id { get; set;}
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Tên sách")]
        [Required(ErrorMessage = "Không được để trống")]
        public string book_name {  get; set;}
        [DisplayName("Ngày tạo")]
        public DateTime Date {  get; set;}
    }
}
