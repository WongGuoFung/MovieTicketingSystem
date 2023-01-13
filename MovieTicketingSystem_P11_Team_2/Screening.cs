//============================================================
// Contributor : ONG SZE HWEE ALEC, WONG GUO FUNG
//=============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingSystem
{
    
    class Screening
    {
        public  int ScreeningNo { get; set; }
        public DateTime ScreeningDateTime { get; set; }
        public string ScreeningType { get; set; }
        public int SeatRemaining { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }
        public Screening() { }
        public Screening(int screeningNo,DateTime screeningDateTime, string screeningType, int seatsRemaining,Cinema c, Movie m)
        {
            ScreeningNo = screeningNo;
            ScreeningDateTime = screeningDateTime;
            ScreeningType = screeningType;
            SeatRemaining = seatsRemaining;
            Cinema = c;
            Movie = m;
        }
        public override string ToString()
        {
            return "ScreeningNo: " + ScreeningNo + "\tScreeningDateTime: " + ScreeningDateTime + " \tScreeningType: " + ScreeningType
                + "\tSeatsRemaining: ";
        }
    }
}
