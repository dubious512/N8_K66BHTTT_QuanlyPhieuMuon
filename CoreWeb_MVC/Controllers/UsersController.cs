using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreWeb_MVC.Models;

namespace CoreWeb_MVC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> ds_user = new List<User>()
        {
            
            new User 
            {
                user_id="217480102234",
                user_name="Trần Văn Bằng",
                user_email="bangd2kz@gmail.com",
                user_phonenumber="098737242842"
            },
            new User 
            {
                user_id="217480105746",
                user_name="Tý Thì Niệm",
                user_email="niemngu@gmail.com",
                user_phonenumber="09873724543"
            },
            new User 
            {
                user_id="217480105832",
                user_name="Đặng Văn Nghệ",
                user_email="nghevl201@gmail.com",
                user_phonenumber="03813724457"
            },
            new User {
                user_id="217480106442",
                user_name="Mã Cao Cung",
                user_email="cungdepzai@gmail.com",
                user_phonenumber="0347853478"
            },
            new User 
            {
                user_id="217480107950",
                user_name="Tào Văn Tháo",
                user_email="thao3q@gmail.com",
                user_phonenumber="043723810095"
            },
            new User 
            {
                user_id="217480100193",
                user_name="Thái Xuân Đông",
                user_email="dongnhoem@gmail.com",
                user_phonenumber="03381922332"
            },
            new User 
            {
                user_id="217480109781",
                user_name="Bực Cả Mình",
                user_email="deolamnua@gmail.com",
                user_phonenumber="012397439001"
            },
        };
        
        
		[HttpGet]
        public IEnumerable<User> GetUsers ()
        {
		// Giả sử bạn đã có một danh sách các sách từ nguồn dữ liệu (database hoặc dữ liệu tĩnh)

		 return ds_user.OrderByDescending(i => i.user_id).AsEnumerable();
	    }
        
		[HttpGet("{user_id}")]
		
		public User GetUsers (string user_id)
		{
			
			return ds_user.FirstOrDefault(u => u.user_id == user_id);
		}

	}
}

