using System.ComponentModel.DataAnnotations;


namespace SHS.Models.ViewModels
{
    /// <summary>
    /// The ViewModel of agent.
    /// </summary>
    public class AgentViewModel
    {
        /// <summary>
        /// The name of agent.
        /// </summary>
        [Required(ErrorMessage = "請輸入業務員名稱")]
        [MaxLength(10, ErrorMessage = "業務員名稱請不超過10個字")]
        public string Name { get; set; }

        /// <summary>
        /// National identification number.
        /// </summary>
        [Required(ErrorMessage = "請輸入業務員身份證字號")]
        [MaxLength(10, ErrorMessage = "業務員身份證字號請不超過10個字")]
        public string IdNo { get; set; }

        /// <summary>
        /// Agent number.
        /// </summary>
        [Required(ErrorMessage = "請輸入業務員編號")]
        [MaxLength(10, ErrorMessage = "業務員編號請不超過10個字")]
        public string AgentNo { get; set; }

        /// <summary>
        /// Date of birth.
        /// </summary>
        public string Dob { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        [EmailAddress(ErrorMessage = "電子信箱格式錯誤")]
        [MaxLength(320, ErrorMessage = "電子信箱請不超過320個字")]
        public string Email { get; set; }
        
        /// <summary>
        /// Cell phone number.
        /// </summary>
        [MaxLength(11, ErrorMessage = "行動電話請不超過11個字")]
        public string CellPhone { get; set; }
    }
}