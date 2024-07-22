using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdapterModels;

public class ResultPublicHolidays
{
    public bool success { get; set; }
    public string status { get; set; }
    public string pagecreatedate { get; set; }
    public List<PublicHolidays> resmitatiller { get; set; }
}

public class PublicHolidays
{
    public string gun { get; set; }
    public string en { get; set; }
    public string haftagunu { get; set; }
    public string tarih { get; set; }
    public string uzuntarih { get; set; }
}