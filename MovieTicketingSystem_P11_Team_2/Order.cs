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
    class Order
    {
        public int OrderNo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public List<Ticket> TicketList { get; set; } = new List<Ticket>();
        public Order() { }
        public Order(int orderno,DateTime orderdatetime)
        {
            OrderNo = orderno;
            OrderDateTime = orderdatetime;
        }
        public void AddTicket(Ticket ticket)
        {
            TicketList.Add(ticket);
        }
        public override string ToString()
        {
            return "OrderNo: " + OrderNo + "\tOrderDateTime: " + OrderDateTime + "\tAmount: " + Amount
                + "\tStatus: " + Status;
        }
    }
}
