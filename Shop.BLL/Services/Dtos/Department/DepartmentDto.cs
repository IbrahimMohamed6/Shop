using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services.Dtos.Department
{
    public class DepartmentDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Requierd Ya Hamada")]

        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code Requierd Ya Hamada")]

        public int Code { get; set; }

        public DateTime CreatedOn { get; set; }

      
    }
}
