using System.ComponentModel.DataAnnotations;


namespace SHS.Models.ViewModels
{
    public class AgentViewModel
    {
        [Required(ErrorMessage = "請輸入業務員名稱")]
        [MaxLength(10, ErrorMessage = "業務員名稱請不超過10個字")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入業務員身份證字號")]
        [MaxLength(10, ErrorMessage = "業務員身份證字號請不超過10個字")]
        public string IdNo { get; set; }

        [Required(ErrorMessage = "請輸入業務員編號")]
        [MaxLength(10, ErrorMessage = "業務員編號請不超過10個字")]
        public string AgentNo { get; set; }

        public string Dob { get; set; }

        [EmailAddress(ErrorMessage = "電子信箱格式錯誤")]
        [MaxLength(320, ErrorMessage = "電子信箱請不超過320個字")]
        public string Email { get; set; }
        
        [MaxLength(11, ErrorMessage = "行動電話請不超過11個字")]
        public string CellPhone { get; set; }
    }
}