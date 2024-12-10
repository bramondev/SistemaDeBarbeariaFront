using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeBarbeariaFront.Models
{
    public class Home
    {
           public Agendamento agendamento  { get; set; }
        public Barbeiro barbeiro { get; set; }
        public Cliente cliente { get; set; }
    }
}