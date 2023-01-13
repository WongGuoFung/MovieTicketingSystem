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
    class Cinema
    {
        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }

        public Cinema(){ }
        public Cinema(string name,int hallno, int capacity)
        {
            Name = name;
            HallNo = hallno;
            Capacity = capacity;
        }

        public override string ToString()
        {
            return "Name: " + Name +"\tHallNo.: " + HallNo + "\tCapacity:" + Capacity;
        }
    }
}
