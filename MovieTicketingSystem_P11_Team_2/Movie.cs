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
    class Movie
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<string> GenreList { get; set; }
        public List<Screening> ScreeningList { get; set; } = new List<Screening>();
        public Movie() { }
        public Movie(string title,int duration, string classfication, DateTime openingDate,List<string> genreList)
        {
            Title = title;
            Duration = duration;
            Classification = classfication;
            OpeningDate = openingDate;
            GenreList = genreList;
        }
        public void addGenre(string newGenre)
        {
            GenreList.Add(newGenre);
        }
        public void AddScreening(Screening screening)
        {
            ScreeningList.Add(screening);
        }
        public override string ToString()
        {
            return "Title: " + Title + "\tDuration: " + Duration + "\tClassification: " + Classification
                + "\tOpeningDate: " + OpeningDate;
        } 
    }
}