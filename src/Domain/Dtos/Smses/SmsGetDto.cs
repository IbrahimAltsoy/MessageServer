using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Smses
{
    public class SmsGetDto
    {
        public string NameSurname { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }

        public string Content { get; set; }
    }
}
