# MovieTicketingSystem

## Background 
Singa Cineplexes, a leading cinema exhibitor on our island country state, has engaged your 
team to develop a movie ticketing system to computerize the movie ticket sales process. 
For a start, its management has requested for a simple prototype of the system to better 
establish the effectiveness of this solution as a system for ticket counter staff and as a 
self-service kiosk. 
The company has a total of 20 cinema halls located at 5 locations, and is home to the most 
number of movie screenings in the country. Every movie scheduled for screening has been 
approved by the relevant authority and given a classification as follows:

#### Movie classification
![MovieClassification](https://github.com/WongGuoFung/MovieTicketingSystem/blob/master/Pictures/MovieClassification.PNG)

A list of cinema hall and movie information is centrally managed by headquarters. Staff 
can then schedule and manage a movie screening session based on the given details i.e., 
Cinema Hall and Movie. Each screening of a movie can either be in a 2D or 3D format. A 
30 mins cleaning time is allocated after the end of every movie screening session.
During the ticket purchase process, the counter staff will require the purchaser to provide 
the age of the ticket holder should the movie fall under the PG13, NC16, M18, or R21 
categories. The ticket will only be sold if the age requirement is met

To support price discrimination strategies, tickets are sold at different prices depending 
on several factors such as days of the week, type of screening (e.g., 3D, 2D), opening date 
of the movie, and selected concession holders. The ticket prices are given below

#### Information of Ticket Price
![Information of Ticket Price](https://github.com/WongGuoFung/MovieTicketingSystem/blob/master/Pictures/InformationOfTicketPrice.PNG)

If a customer wishes to purchase a student ticket, the level of study must be provided 
(e.g., Primary, Secondary, Tertiary). For a senior citizen ticket, the ticket holder’s age must 
be 55 and above. By default, an adult ticket is to be purchased, even for children who do 
not fall into the student category. For customers buying adult tickets, they will also be 
entitled to purchase a popcorn set at a discounted rate of $3.00. 
Information for use with the Movie, Cinema and Screening classes are given in the
Movie.csv, Cinema.csv and Screening.csv files respectively.

The class diagram for the movie ticketing system is shown below.
The different “status” in the Order class are as follows:
1. Unpaid (When a new order is created)
2. Paid (After payment is made)
3. Cancelled
The order number in the Order class is a running number starting from 1.
The screening number in the Screening class is a running number starting from 1001. 

### Class Diagram for Movie Ticketing System
![Class Diagram For Movie Ticketing System]([Pictures/ClassDiagramForMovieTicketingSystem.PNG](https://github.com/WongGuoFung/MovieTicketingSystem/blob/master/Pictures/ClassDiagramForMovieTicketingSystem.PNG))
