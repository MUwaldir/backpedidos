using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPedidos.Models
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public int DurationInMinutes { get; set; }
    }
}