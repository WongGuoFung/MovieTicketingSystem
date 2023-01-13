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
    class SeniorCitizen : Ticket
    {
        public int YearOfBirth { get; set; }
        public SeniorCitizen() : base() { }
        public SeniorCitizen(Screening screening, int yob) :base (screening)
        {
            YearOfBirth = yob;
        }
        public override double CalculatePrice()
        {
            List<int> weekEnd = new List<int> { 0, 5, 6 };
            int day = (int)Screening.ScreeningDateTime.DayOfWeek;
            bool isWithin7Days = (Screening.ScreeningDateTime - Screening.Movie.OpeningDate).Days <= 7;
            if (Screening.ScreeningType == "2D")
            {
                if (weekEnd.Contains(day) || isWithin7Days)
                    return 12.50;
                else
                    return 5.00;
            }
            else   //3D Movvie price for Senior Citizen 
            {
                if (weekEnd.Contains(day) || isWithin7Days)
                    return 14.00;
                else
                    return 6.00;
            }
        }
        public override string ToString()
        {
            return base.ToString() + "YearOfBirth: " + YearOfBirth;
        }
    }
}
