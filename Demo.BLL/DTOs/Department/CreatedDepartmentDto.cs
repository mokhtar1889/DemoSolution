﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.Department
{
    public class CreatedDepartmentDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name ="Date of Creation")]
        public DateTime DateOfCreation { get; set; }

    }
}
