//============================================================
// Contributor : ONG SZE HWEE ALEC, WONG GUO FUNG
//============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MovieTicketingSystem
{
    class Program
    {
        static int orderNum = 0;
        static int screeningNum = 1000;
        static void Main(string[] args)
        {
            List<Movie> movieList = new List<Movie>();
            List<Cinema> cinemaList = new List<Cinema>();
            List<Screening> screeningList = new List<Screening>();
            List<Order> orderList = new List<Order>();
            InitCinema(cinemaList);
            InitMovie(movieList);
            InitScreening(screeningList, movieList, cinemaList);

            while (true)
            {
                try
                {
                    List<int> acceptedOptions = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8,9 };
                    Menu();
                    Console.Write("Enter an option: ");
                    int option = Convert.ToInt32(Console.ReadLine());
                    if (acceptedOptions.Contains(option))
                    {
                        if (option == 1)
                        {
                            ListMovie(movieList);
                        }
                        else if (option == 2)
                            UserInputMovie(movieList);
                        else if (option == 3)
                            AddMovieScreeningSession(movieList, screeningList, cinemaList);
                        else if (option == 4)
                            DeleteMovieScreening(movieList, screeningList);
                        else if (option == 5)
                            OrderMovieTicket(orderList, movieList, screeningList);
                        else if (option == 6)
                            CancelOrderOfTicket(orderList, movieList);
                        else if (option == 7)
                            HighestMovie(orderList);
                        else if (option == 8)
                            DisplaySeatsRemainingInDescending(screeningList);
                        else if (option == 9)
                            SearchMovieByGenre(movieList);
                        else if (option == 0)
                        {
                            Console.WriteLine("Thank you and have a nice day!");
                            break;
                        }
                    }
                    else
                        Console.WriteLine("Please only select options from the menu[0-{0}].", acceptedOptions.Count - 1);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please only enter an integer.");
                }
            }
        }
        public static void Menu()
        {
            Console.WriteLine("------------- MENU --------------");
            Console.WriteLine("[1] List all movies");
            Console.WriteLine("[2] List movies screenings");
            Console.WriteLine("[3] Add a movie screening session");
            Console.WriteLine("[4] Delete a movie screening session");
            Console.WriteLine("[5] Order Movie Ticket(s)");
            Console.WriteLine("[6] Cancel order of ticket");
            Console.WriteLine("[7] Recommend movie based on sale of tickets sold");
            Console.WriteLine("[8] Displaying screening session by seat remaining in descending order");
            Console.WriteLine("[9] Search movie by genre");
            Console.WriteLine("[0] Exit");
            Console.WriteLine("--------------------------------");
        }
        public static void InitMovie(List<Movie> mList)
        {
            if (File.Exists("Movie.csv"))
            {
                string[] mArray = File.ReadAllLines("Movie.csv");
                for (int i = 1; i < mArray.Length; i++)
                {
                    List<string> genreList = new List<string>();
                    string[] dataArray = mArray[i].Split(",");
                    string title = dataArray[0];
                    string duration = dataArray[1];
                    string[] gArray = dataArray[2].Split("/");
                    for (int j = 0; j < gArray.Length; j++)
                    {
                        genreList.Add(gArray[j]);
                    }
                    string classification = dataArray[3];
                    DateTime opDateTime = Convert.ToDateTime(dataArray[4]);
                    mList.Add(new Movie(title, Convert.ToInt32(duration), classification, opDateTime, genreList));
                }
            }
        }
        public static void InitCinema(List<Cinema> cList)
        {
            if (File.Exists("Cinema.csv"))
            {
                string[] cArray = File.ReadAllLines("Cinema.csv");
                for (int i = 1; i < cArray.Length; i++)
                {
                    string[] dataArray = cArray[i].Split(",");
                    string cName = dataArray[0];
                    int cHallNo = Convert.ToInt32(dataArray[1]);
                    int cCapacity = Convert.ToInt32(dataArray[2]);
                    Cinema c = new Cinema(cName, cHallNo, cCapacity);
                    cList.Add(c);
                }
            }
        }
        public static void InitScreening(List<Screening> sList, List<Movie> mList, List<Cinema> cList)
        {
            if (File.Exists("Screening.csv"))
            {
                string[] sArray = File.ReadAllLines("Screening.csv");
                for (int i = 1; i < sArray.Length; i++)
                {
                    string[] dataArray = sArray[i].Split(",");
                    DateTime screeningDT = Convert.ToDateTime(dataArray[0]);
                    string screeningType = dataArray[1];
                    foreach (Cinema c in cList)
                    {
                        if (dataArray[2] == c.Name && Convert.ToInt32(dataArray[3]) == c.HallNo)
                        {
                            //dataArray[2] is cinema name, dataArray[3] is cinema hall no 
                            //We only want the cinema object if it matches with the screening csv
                            foreach (Movie m in mList)
                            {
                                if (dataArray[4] == m.Title)
                                {
                                    screeningNum++;
                                    int seatR = c.Capacity;
                                    Screening newScreenTime = new Screening(screeningNum, screeningDT, screeningType, seatR, c, m);
                                    m.AddScreening(newScreenTime);
                                    sList.Add(newScreenTime);
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void ListCinema(List<Cinema> cList)
        {
            Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-20}", "S/No", "Name", "HallNo.", "Capacity");
            Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-20}", "----", "----", "-------", "--------");
            for (int i = 0; i < cList.Count; i++)
            {
                Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-20}", i + 1, cList[i].Name, cList[i].HallNo, cList[i].Capacity);
            }
        }
        public static void UserInputMovie(List<Movie> mList)
        {
            //Listing all movie and prompt user input for displaying screening time for that movie
            ListMovie(mList);
            while (true)
            {
                try
                {
                    Console.Write("Enter a S/No to select a movie: ");
                    int sNo = Convert.ToInt32(Console.ReadLine());
                    if (sNo > 0 && sNo <= mList.Count)
                    {
                        for (int i = 0; i < mList.Count; i++)
                        {
                            if (i == Convert.ToInt32(sNo) - 1) //When movie is found Listthe screening session for that movie
                            {
                                ListScreening(mList[i].ScreeningList);//Listscreening for that movie 
                            }
                        }
                        break;
                    }
                    else { Console.WriteLine("You can only enter [1 - {0}]", mList.Count); }
                }
                catch (FormatException) { Console.WriteLine("Please only enter an integer."); }
            }
        }
        public static void ListMovie(List<Movie> mList)
        {
            Console.WriteLine("{0,-10}{1,-30}{2,-20}{3,-20}{4,-25}{5,-20}", "S/No", "Title", "Duration(mins)", "Classification", "Genre", "Opening Date ");
            Console.WriteLine("{0,-10}{1,-30}{2,-20}{3,-20}{4,-25}{5,-20}", "----", "-----", "--------------", "--------------", "------", "------------");
            for (int i = 0; i < mList.Count; i++)
            {
                List<string> gList = mList[i].GenreList;
                string genre = string.Join("/", gList.ToArray());

                Console.WriteLine("{0,-10}{1,-30}{2,-20}{3,-20}{4,-25}{5,-20}", i + 1, mList[i].Title, mList[i].Duration, mList[i].Classification, genre, mList[i].OpeningDate);
            }
        }
        public static void ListScreening(List<Screening> sList)
        {
            Console.WriteLine("{0,-10}{1,-30}{2,-20}{3,-20}", "S/No", "ScreeningDateTime", "ScreeningType", "SeatsRemaining");
            Console.WriteLine("{0,-10}{1,-30}{2,-20}{3,-20}", "----", "-----------------", "-------------", "------------");
            for (int i = 0; i < sList.Count; i++)
            {
                Console.WriteLine("{0,-10}{1,-30}{2,-20}{3,-20}", i + 1, sList[i].ScreeningDateTime,
                    sList[i].ScreeningType, sList[i].SeatRemaining);
            }
        }
        public static void AddMovieScreeningSession(List<Movie> mList, List<Screening> sList, List<Cinema> cList)
        {
            ListMovie(mList);
            while (true)
            {
                try
                {
                    bool screenAdded = false;
                    Console.Write("Enter a S/No to select a movie [ or -1 to exit]: ");
                    int movieSNo = Convert.ToInt32(Console.ReadLine());
                    if (movieSNo > 0 && movieSNo <= mList.Count)
                    {
                        for (int i = 0; i < mList.Count; i++) // mList[i] is an object
                        {
                            if (i == movieSNo - 1)
                            {
                                try
                                {
                                    while (true)
                                    {
                                        Console.Write("Enter a screening type[2D/3D]: ");
                                        string mScreenType = Console.ReadLine().ToUpper();
                                        if (mScreenType == "2D" || mScreenType == "3D")
                                        {
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.Write("Enter a screening date and time: ");
                                                    DateTime mScreenDTime = Convert.ToDateTime(Console.ReadLine());
                                                    // check user datetime entered is later than opening date
                                                    if (mScreenDTime >= mList[i].OpeningDate && mScreenDTime.TimeOfDay.TotalMinutes != 0)
                                                    {//check that time entered is later than opening date of movie and time is entered
                                                        ListCinema(cList);
                                                        while (true)
                                                        {
                                                            try
                                                            {
                                                                Console.Write("Enter a S/No to select a cinema hall: ");
                                                                int cinemaSNo = Convert.ToInt32(Console.ReadLine());
                                                                if (cinemaSNo >= 1 && cinemaSNo <= cList.Count)
                                                                {
                                                                    for (int j = 0; j < cList.Count; j++) // select c from cList
                                                                    {
                                                                        if (j == cinemaSNo - 1)//if same object, obtain the object 
                                                                        {
                                                                            //create a list to store screening for that particular cinema 
                                                                            List<Screening> allScreeningForUserCinemaList = new List<Screening>();
                                                                            foreach (Screening s in sList) //each screening object (s) is object for the list 
                                                                            {
                                                                                if (s.Cinema.Equals(cList[j]))
                                                                                    allScreeningForUserCinemaList.Add(s);

                                                                            }
                                                                            if (allScreeningForUserCinemaList.Count == 0)
                                                                            {
                                                                                screeningNum++;
                                                                                Cinema cinemaSelected = cList[j];
                                                                                Screening newScreeningDateTime = new Screening(screeningNum, mScreenDTime, mScreenType, cinemaSelected.Capacity, cinemaSelected, mList[i]);
                                                                                mList[i].AddScreening(newScreeningDateTime);//add for sList in movie class
                                                                                sList.Add(newScreeningDateTime);//add for sList in screening class
                                                                                screenAdded = true;
                                                                            }
                                                                            //calculate the ending time of the screentime including of cleaning time 
                                                                            DateTime endScreenDTime = mScreenDTime.AddMinutes(mList[i].Duration + 30);
                                                                            int counter = 0; // make sure all existing screenings do not clash with the new screeening 
                                                                            foreach (Screening s in allScreeningForUserCinemaList)
                                                                            {
                                                                                //CHECKING if cinema is available 
                                                                                //calculating ending time of screening time for each screening for selected movie including the time considered  
                                                                                DateTime startingDateTime = s.ScreeningDateTime; // starting time of each screening
                                                                                DateTime endingDateTime = s.ScreeningDateTime.AddMinutes(mList[i].Duration + 30); // real ending time
                                                                                if ((startingDateTime.AddMinutes(-(mList[i].Duration + 30)) <= mScreenDTime) && (mScreenDTime <= endingDateTime))
                                                                                {
                                                                                    screenAdded = false;
                                                                                }
                                                                                else
                                                                                {
                                                                                    counter++;
                                                                                    if (counter == allScreeningForUserCinemaList.Count) //if all fulfil the condidtion 
                                                                                    {
                                                                                        screeningNum++;
                                                                                        Screening newScreeningDateTime = new Screening(screeningNum, mScreenDTime, mScreenType, s.Cinema.Capacity, s.Cinema, mList[i]);
                                                                                        mList[i].AddScreening(newScreeningDateTime);//add for sList in movie class
                                                                                        sList.Add(newScreeningDateTime);//add for sList in screening class
                                                                                        screenAdded = true;
                                                                                        break;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    break; //for 4th loop
                                                                }
                                                                else { Console.WriteLine("Please only enter 1 - {0}", cList.Count); }
                                                            }
                                                            catch (FormatException) { Console.WriteLine("Please only enter an integer"); } // exception for cinema SNo
                                                        }
                                                        break; //for third loop
                                                    }
                                                    else { Console.WriteLine("You have to enter a date time after the opening date of the movie selected OR enter a time."); }
                                                }
                                                catch (FormatException) { Console.WriteLine("Please enter in this format - [dd/MM/yyyy hh:mm:ss] "); }
                                            }
                                            break;// for second while loop
                                        }
                                        else
                                        { Console.WriteLine("Please enter 2D or 3D only."); } // only allow 2d or 3d 
                                    }
                                }
                                catch (Exception) { Console.WriteLine("Please enter 2D or 3D only."); }// exception for movie screening type 
                            }
                        }
                        if (screenAdded) // displaying of result
                        {
                            Console.WriteLine("New screening has been added.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Screening has not been added.");
                            break;
                        }
                    }
                    else if (movieSNo == -1) { break; }
                    else { Console.WriteLine("Please only enter 1 - {0}", mList.Count); }
                }
                catch (FormatException) { Console.WriteLine("Please only enter an integer"); } // exception for movie SNo
            }
        }
        public static void ListTicketType()
        {
            Console.WriteLine("======================");
            Console.WriteLine("[1] Student");
            Console.WriteLine("[2] Senior Citizen [55 years and above]");
            Console.WriteLine("[3] Adult");
            Console.WriteLine("======================");
        }
        public static void DeleteMovieScreening(List<Movie> mList, List<Screening> sList)
        {
            List<Screening> screeningsToBeRemove = new List<Screening>();
            foreach (Screening s in sList)
            {
                //When cinema capacity and seat remaining are the same, 0 tickets are sold
                if (s.SeatRemaining == s.Cinema.Capacity)
                {
                    screeningsToBeRemove.Add(s);
                }
            }
            Console.WriteLine("Movie Screenings with 0 tickets sold.\n");
            ListScreening(screeningsToBeRemove);
            //Prompt to select a session
            while (true)
            {
                try
                {
                    Console.Write("Select S/No to delete the movie screening [or enter -1 to exit]: ");
                    int sNo = Convert.ToInt32(Console.ReadLine());
                    if (sNo > 1 && sNo <= screeningsToBeRemove.Count)
                    {
                        Screening sToBeDelete = new Screening();
                        for (int i = 0; i < screeningsToBeRemove.Count(); i++)
                        {
                            if (i == sNo - 1)
                            {
                                sToBeDelete = screeningsToBeRemove[i];
                                break;
                            }
                        }
                        //Remove screening obj from  Movie.ScreeningList 
                        bool removeFromMovie = false;
                        foreach (Movie m in mList)
                        {
                            for (int i = 0; i < m.ScreeningList.Count; i++)
                            {
                                if (m.ScreeningList[i] == sToBeDelete)
                                {
                                    m.ScreeningList.RemoveAt(i);
                                    removeFromMovie = true;
                                }
                            }
                        }
                        // Remove screening obj from Screening List
                        bool removeFromScreening = false;
                        for (int i = 0; i < sList.Count; i++)
                        {
                            if (sList[i] == sToBeDelete)
                            {
                                sList.RemoveAt(i);
                                removeFromScreening = true;
                            }
                        }
                        if (removeFromMovie && removeFromScreening)
                        {
                            Console.WriteLine("Removal successful.");
                        }
                        break;
                    }
                    else if (sNo == -1) { break; }
                    else { Console.WriteLine("Please only enter 1 - {0}", screeningsToBeRemove.Count); }
                }
                catch (FormatException) { Console.WriteLine("Please only enter an integer"); }
            }
        }
        public static void DisplaySeatsRemainingInDescending(List<Screening> sList)
        {
            var newList = sList.OrderByDescending(x => x.SeatRemaining).ToList(); //new list that will order seat remaining in desc order
            ListScreening(newList);
        }
        public static void ListGenre()
        {
            Console.WriteLine("======================");
            Console.WriteLine("[1] Musical");
            Console.WriteLine("[2] Action");
            Console.WriteLine("[3] Horror");
            Console.WriteLine("[4] Crime");
            Console.WriteLine("[5] Drama");
            Console.WriteLine("[6] Mystery");
            Console.WriteLine("[7] Thriller");
            Console.WriteLine("[8] Comedy");
            Console.WriteLine("[9] Romance");
            Console.WriteLine("[10] Animation");
            Console.WriteLine("======================");
        }
        public static void SearchMovieByGenre(List<Movie> mList)
        {
            ListGenre();
            //Making of list for validation
            List<Movie> movieSortByGenre = new List<Movie>();
            List<string> genreList = new List<string> { "Musical", "Action", "Horror", "Crime", "Drama", "Mystery", "Thriller", "Comedy", "Romance", "Animation" };
            List<string> genreChosen = new List<string>();
            List<int> genreSNo = new List<int>();
            bool genreFound = false;
            while (true)
            {
                try
                {
                    Console.Write("How many genre do you wish to choose [or -1 to exit]: ");
                    int numGenre = Convert.ToInt32(Console.ReadLine());
                    int counter = 0;
                    if (numGenre > 0 && numGenre <= genreList.Count)
                    {
                        while (true)
                        {
                            while (counter < numGenre)
                            {
                                try
                                {
                                    Console.Write("Enter a S/No to select a genre: ");
                                    int SNo = Convert.ToInt32(Console.ReadLine());
                                    if (SNo > 0 && SNo <= genreList.Count)
                                    {
                                        bool sNoDuplicate = genreSNo.Contains(SNo);
                                        if (sNoDuplicate) // meaning genre is duplicated
                                        {
                                            Console.WriteLine("You cannot select the same genre again.");
                                            continue;
                                        }
                                        else // new genre detected
                                        {
                                            genreSNo.Add(SNo);
                                            counter++;
                                            for (int i = 0; i < genreList.Count; i++)
                                            {
                                                if (i == SNo - 1) // get genre from list
                                                {
                                                    foreach (Movie m in mList)
                                                    {
                                                        if (m.GenreList.Contains(genreList[i]))
                                                        {
                                                            genreFound = true;
                                                            if (movieSortByGenre.Contains(m))
                                                                continue;
                                                            else
                                                            {
                                                                movieSortByGenre.Add(m);
                                                                if (genreChosen.Contains(genreList[i]))
                                                                    continue;
                                                                else
                                                                    genreChosen.Add(genreList[i]);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else { Console.WriteLine("Please only enter an integer [1 - {0}]", genreList.Count); }
                                }
                                catch (FormatException) { Console.WriteLine("Please only enter an integer."); }
                            }
                            break;//Second while true after finish executing
                        }
                        break;
                    }
                    else if (numGenre == -1) { break; }
                    else
                        Console.WriteLine("You need to choose at least 1 genre and less than {0} genre(s).", genreList.Count);
                }
                catch (FormatException) { Console.WriteLine("Please only enter an integer."); }
            }
            if (genreFound)
            {
                string genre = string.Join(",", genreChosen.ToArray());
                Console.WriteLine("You have chosen: " + genre + "\n");
                ListMovie(movieSortByGenre);
            }
            else
            {
                //Unable to execute unless new genere added and no such movie fulfil that genre 
                //unless exception not done for the select SNo prompt 
                Console.WriteLine("No movie found with genre(s) selected");
            }
        }
        public static void OrderMovieTicket(List<Order> oList, List<Movie> mList, List<Screening> sList)
        {
            int chkcounter = 0;
            ListMovie(mList); //7.1

            while (true)
            {
                try
                {
                    Console.Write("Enter a S/No to select a movie: "); //7.2
                    int movieSNo = Convert.ToInt32(Console.ReadLine());
                    if (movieSNo > 0 && movieSNo <= mList.Count)
                    {
                        for (int i = 0; i < mList.Count; i++) //7.3
                        {
                            if (i == movieSNo - 1)
                            {
                                ListScreening(mList[i].ScreeningList);//Listing screenings for the movie selected
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write("Enter a S/No to select a movie screening or [0] to exit: "); //7.4
                                        int movieScreeningSNo = Convert.ToInt32(Console.ReadLine());
                                        //Screening List for movie selected
                                        List<Screening> movieScreening = mList[i].ScreeningList; //7.5
                                        if (movieScreeningSNo > 0 && movieScreeningSNo <= (movieScreening).Count)
                                        {
                                            for (int j = 0; j < movieScreening.Count; j++)
                                            {

                                                if (j == movieScreeningSNo - 1)
                                                {
                                                    Screening screen = movieScreening[j];
                                                    foreach (Screening mScreening in movieScreening)
                                                    {
                                                        foreach (Screening s in sList) // to access object in screeningList
                                                        {
                                                            if (s == screen)  // if both object are the same, then start ordering 
                                                            {
                                                                if (chkcounter == 0)
                                                                {
                                                                    while (true)
                                                                    {
                                                                        try
                                                                        {
                                                                            Console.Write("Enter the no. of tickets you wish to order: "); //7.6
                                                                            int totalTicket = Convert.ToInt32(Console.ReadLine());
                                                                            if (totalTicket > 0)
                                                                            {
                                                                                if (totalTicket <= screen.SeatRemaining) // check if no.of seat is available 
                                                                                {
                                                                                    if (mScreening.Movie.Classification != "G") // not "G" then ask 
                                                                                    {
                                                                                        Console.WriteLine(screen.Movie.Classification);
                                                                                        while (true)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                Console.Write("Have all ticket holders met the movie classification requirements?[Y/N]"); //7.7
                                                                                                string requireMet = Console.ReadLine().ToUpper();
                                                                                                if (requireMet == "Y")
                                                                                                {
                                                                                                    OrderFunction(totalTicket, screen, oList);
                                                                                                    chkcounter++;

                                                                                                    s.SeatRemaining = s.SeatRemaining - totalTicket;//9.d update remainingseats.
                                                                                                    break;
                                                                                                }
                                                                                                else if (requireMet == "N")
                                                                                                {
                                                                                                    Console.WriteLine("All ticket holders must fulfil movie classfication requirements.");
                                                                                                    break;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Console.WriteLine("Please enter only Y or N");

                                                                                                }
                                                                                            }
                                                                                            catch (FormatException)
                                                                                            {
                                                                                                Console.WriteLine("Please enter only Y or N");
                                                                                            }
                                                                                        }

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        OrderFunction(totalTicket, screen, oList);
                                                                                        chkcounter++;

                                                                                        s.SeatRemaining = s.SeatRemaining - totalTicket;//9.d update remainingseats

                                                                                    }
                                                                                    break;

                                                                                }
                                                                                else //if total ticket excees
                                                                                {
                                                                                    Console.WriteLine("There are insufficient remaining seats available, Re-enter amount");
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine("Ticket ordered must be more than 0");
                                                                                break;
                                                                            }

                                                                        }
                                                                        catch (FormatException)
                                                                        {
                                                                            Console.WriteLine("Please only enter an integer.");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                        else if (movieScreeningSNo == 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please only enter an integer [1 - {0}]", movieScreening.Count);
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Please only enter an integer.");
                                    }

                                }
   
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please only enter an integer [1 - {0}]", mList.Count);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please only enter an integer.");
                }
                
            }
            
        }



        public static void OrderFunction(int totalTicket, Screening mScreening, List<Order> oList)
        {

            int counter = 0;
            orderNum++; //global varibale, new number for every ticket 
            Order order = new Order(orderNum, DateTime.Now);
            order.Status = "Unpaid"; //7.8
            order.Amount = 0;
            while (counter < totalTicket)
            {
                counter++;
                ListTicketType();
                while (true)
                {
                    try
                    {
                        Console.Write("Ticket {0} | Enter the type of ticket you wish to buy: ", counter);
                        int ticketType = Convert.ToInt32(Console.ReadLine());
                        
                        if (ticketType == 1) //7.9ai
                        {
                            while (true)
                            {
                                try
                                {
                                    Console.Write("Enter your level of study [Primary,Secondary,Tertiary]: ");
                                    string levelOfStudy = Console.ReadLine().ToUpper();
                                    if (levelOfStudy == "PRIMARY" || levelOfStudy == "SECONDARY" || levelOfStudy == "TERTIARY")
                                    {
                                        Ticket studentTicket = new Student(mScreening, levelOfStudy); //9.b
                                        order.Amount += studentTicket.CalculatePrice(); //7.12 //Adding ticket to tList of order
                                        order.AddTicket(studentTicket);// adding ticket obj to tList of order 9.c
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please re-enter your level of study");
                                    }

                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Please re-enter your level of study");
                                }
                            }
                            break;
                        }
                        else if (ticketType == 2) //7.9aii
                        {
                            while(true)

                            {
                                try
                                {
                                    Console.Write("Enter year of birth: ");
                                    int yearOfBirth = Convert.ToInt32(Console.ReadLine());
                                    //Check if more than 55 years old 
                                    bool over55Years = (DateTime.Now.Year - yearOfBirth) >= 55;
                                    if (over55Years)
                                    {
                                        Console.WriteLine("You're eligible for senior citizen ticket");
                                        Ticket scTicket = new SeniorCitizen(mScreening, yearOfBirth); //9.b
                                        order.Amount += scTicket.CalculatePrice();//7.12
                                        order.AddTicket(scTicket);// adding ticket obj to tList of order 9.c
                                        break;
                                    }
                                    else
                                    { Console.WriteLine("Not eligible yet");
                                        break;
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Please enter only integers");
                                   
                                }
                                
                            }
                            break;
                             
                            
                        }
                        else if (ticketType == 3) //9.aiii
                        {
                            while (true)
                            {
                                try
                                {
                                    Console.Write("Popcorn offer for $3?[Y/N]");
                                    string popCornOffer = Console.ReadLine().ToUpper();
                                    bool pco = false;
                                    if (popCornOffer == "Y") 
                                    { 
                                        pco = true;
                                        Ticket adultTicket = new Adult(mScreening, pco); //9.b
                                        order.Amount += adultTicket.CalculatePrice(); //7.12
                                        order.AddTicket(adultTicket); // adding ticket obj to tList of order 9.c
                                        break;
                                    }
                                    else if (popCornOffer == "N") 
                                    {
                                        pco = false;
                                        Ticket adultTicket = new Adult(mScreening, pco); //9.b
                                        order.Amount += adultTicket.CalculatePrice(); //7.12
                                        order.AddTicket(adultTicket); // adding ticket obj to tList of order 9.c
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter only Y or N");
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Please enter only Y or N");
                                }
                                
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter only integers 1 to 3");
                        }
                        

                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter only integers 1 to 3");
                    }
                    
                }

            }
            //Update of seat number for both screening & movie class screeeningList
            Console.WriteLine("You have bought {0} ticket, total amount payable: {1}", order.TicketList.Count, order.Amount.ToString("0.00")); //7.10
            Console.WriteLine("Press any key to make payment..."); //7.11
            Console.ReadKey();
            order.Status = "Paid"; //7.13
            oList.Add(order); //7.12
        }
        
        public static void ListOrderTicket(List<Order> oList)
        {
            Console.WriteLine("{0,-20}{1,-40}{2,-20}", "Order No.", "Date and Time of Order", "Status");
            Console.WriteLine("{0,-20}{1,-40}{2,-20}", "---------", "--------------------------------", "------");
            foreach (Order order in oList)
            {
                Console.WriteLine("{0,-20}{1,-40}{2,-20}", order.OrderNo, order.OrderDateTime.ToString("f"), order.Status);
            }
        }
        public static void CancelOrderOfTicket(List<Order> oList, List<Movie> mList)
        {
            if (oList.Count > 0)
            {
                while (true)
                {
                    try
                    {
                        ListOrderTicket(oList);//To display the order list
                        Console.Write("Enter your order number or [0] to exit: "); //8.1


                        int orderNumber = Convert.ToInt32(Console.ReadLine());

                        if (orderNumber >= 1 && orderNumber <= oList.Count)
                        {
                            bool cancelSuccess = false;
                            Screening s = new Screening();

                            for (int i = 0; i < oList.Count; i++)// each row in Order No.
                            {

                                if (oList[i].OrderNo == orderNumber)

                                {
                                    s = oList[i].TicketList[0].Screening;
                                    if (DateTime.Now < s.ScreeningDateTime && oList[i].Status != "Cancelled")// if not screened can be deleted
                                    {
                                        foreach (Movie m in mList)
                                        {
                                            foreach (Screening screening in m.ScreeningList)

                                            {
                                                if (s == screening) // if same screening then change seat remaining 8.3
                                                {
                                                    screening.SeatRemaining++; //8.4
                                                    cancelSuccess = true;
                                                    break;
                                                }

                                            }
                                            break; //dont waste resources, if found
                                        }
                                        oList[i].Status = "Cancelled"; //8.5
                                    }
                                    if (cancelSuccess)
                                    {
                                        Console.WriteLine("${0} was refunded", oList[i].Amount); //8.6
                                        Console.WriteLine("Cancelation was successful."); //8.7
                                        break;

                                    }
                                    else
                                    {
                                        Console.WriteLine("Cancelation was unsuccesful"); //8.7
                                        break;
                                    }
                                }
                            }
                        }
                        else if (orderNumber == 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid order number entered");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter only integers 1 to {0}", oList.Count);
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("Order List is empty, there are no order(s) to be cancelled");
            }

        }
        public static void HighestMovie(List<Order> oList)
        {
            
            int ticketcount = 0;
            string movieName = "";
            if (oList.Count > 0)
            {
                foreach (Order o in oList)
                {
                    if (o.TicketList[0].Screening.Movie.Title == movieName)
                    {
                        ticketcount += o.TicketList.Count;
                    }
                    else // if ticket is more, it will override it
                    {
                        if (o.TicketList.Count > ticketcount)
                        {
                            movieName = o.TicketList[0].Screening.Movie.Title;
                            ticketcount = o.TicketList.Count;
                        }
                    }
                }
                Console.WriteLine("Reccomended Movie: " + movieName);
            }
            else
            {
                Console.WriteLine("There are no movie to recommend");
                  
            }
 

        }
        public static void Highest3Movie(List<Order> oList)
        {

            int ticketcount = 0;
            string movieName = "";
            foreach (Order o in oList)
            {
                if (o.TicketList[0].Screening.Movie.Title == movieName)
                {
                    ticketcount += o.TicketList.Count;
                }
                else // if ticket is more, it will override it
                {
                    if (o.TicketList.Count > ticketcount)
                    {
                        movieName = o.TicketList[0].Screening.Movie.Title;
                        ticketcount = o.TicketList.Count;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {

            }
            Console.WriteLine("Reccomended Movie: " + movieName);
        }



    }

}



