using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace StudentsAPI_4_Praxis.Models
{
    public class Students
    {
        [Required]
        public int ID { get; set; }
        public string? Name { get; set; }
        //public DateTime DOB { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Grade { get; set; }
        public string? School_Code { get; set; }
        public string? School_Name { get; set; }
    }
}
