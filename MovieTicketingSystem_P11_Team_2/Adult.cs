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
    class Adult : Ticket
    {
        public bool PopCornOffer { get; set; }
        public Adult() : base() { }
        public Adult(Screening screening, bool pco) : base(screening)
        {
            PopCornOffer = pco;
        }
        public override double CalculatePrice()
        {
            double amt;
            List<int> weekEnd = new List<int> { 0, 5, 6 };
            int day = (int)Screening.ScreeningDateTime.DayOfWeek;
            bool isWithin7Days = (Screening.ScreeningDateTime - Screening.Movie.OpeningDate).Days <= 7;
            if (Screening.ScreeningType == "2D")
            {
                if (weekEnd.Contains(day) || isWithin7Days)
                        amt =  12.50;
                else //Weekdays or after 7 days...
                    amt = 8.50;
            }
            else //3D price for adult
            {
                if (weekEnd.Contains(day) || isWithin7Days)
                        amt =  14.00;
                else //Weekdays or after 7 days...
                    amt = 11.00;
            }
            if (PopCornOffer)
            {
                amt += 3;
            }
            return amt;
        }
        public override string ToString()
        {
            return base.ToString() + "PopCornOffer: " + PopCornOffer;
        }
    }
}