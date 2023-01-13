//============================================================
// Contributor : ONG SZE HWEE ALEC, WONG GUO FUNG
//============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieTicketingSystem
{
    abstract class Ticket
    {
        public Screening Screening { get; set; }
        public Ticket() { }
        public Ticket(Screening screening)
        {
            Screening = screening;
        }
        public abstract double CalculatePrice();
        public override string ToString()
        {
            return "Screening: " + Screening;
        }
    }
}
