using System.ComponentModel.DataAnnotations;


namespace SHS.Models.ViewModel
{
    public class AgentViewModel
    {
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請輸入業務員名稱")]
        [MaxLength(10, ErrorMessage = "業務員名稱請不超過10個字")]
        public string Name { get; set; }

        [Display(Name = "身份證字號")]
        [Required(ErrorMessage = "請輸入業務員身份證字號")]
        [MaxLength(10, ErrorMessage = "業務員身份證字號請不超過10個字")]
        public string IdNo { get; set; }

        [Display(Name = "業務員編號")]
        [Required(ErrorMessage = "請輸入業務員編號")]
        [MaxLength(10, ErrorMessage = "業務員編號請不超過10個字")]
        public string AgentNo { get; set; }

        [Display(Name = "生日")]
        public string Dob { get; set; }

        [Display(Name = "電子信箱")]
        [EmailAddress(ErrorMessage = "電子信箱格式錯誤")]
        [MaxLength(320, ErrorMessage = "電子信箱請不超過320個字")]
        public string Email { get; set; }
        
        [Display(Name = "行動電話")]
        [MaxLength(11, ErrorMessage = "行動電話請不超過11個字")]
        public string CellPhone { get; set; }
    }
}