using Store.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WebMVC.Models
{
    public class MenuViewModel
    {
        public List<CategoryDTO> Categories { get; set; }
    }
}