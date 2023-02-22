using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPosRio.Models.ViewModels
{
    public class FiltroPagedList
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;
    }
}
