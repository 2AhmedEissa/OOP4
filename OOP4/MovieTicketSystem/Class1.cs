using System;

namespace OOP2.MovieTicketSystem
{
    public enum TicketType
    {
        Standard,
        VIP,
        IMAX
    }

    public struct Seat
    {
        public char Row { get; }
        public int Number { get; }

        public Seat(char row, int number)
        {
            Row = row;
            Number = number;
        }

        public override string ToString() => $"{Row}{Number}";
    }

    public class Ticket
    {
        private string movieName;

        public string MovieName
        {
            get { return movieName; }
            set
            {
                movieName = !string.IsNullOrEmpty(value) ? value : movieName;
            }
        }

        public TicketType Type { get; }
        public Seat Seat { get; }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value > 0 ? value : price;
            }
        }

        public decimal PriceAfterTax
        {
            get
            {
                return Price * 1.14m;
            }
        }

        private static int ticketCounter = 0;

        public int TicketId { get; }

        public Ticket(
            string movieName,
            TicketType type,
            Seat seat,
            decimal price)
        {
            MovieName = movieName;
            Type = type;
            Seat = seat;
            Price = price;
            TicketId = ++ticketCounter;
        }

        public Ticket(string movieName, decimal price)
            : this(
                movieName,
                TicketType.Standard,
                new Seat('A', 1),
                price)
        {
        }

        public Ticket(string movieName)
            : this(
                movieName,
                TicketType.Standard,
                new Seat('A', 1),
                50m)
        {
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }

        public void SetPrice(decimal basePrice, decimal multiplier)
        {
            Price = basePrice * multiplier;
        }

        public virtual void PrintTicket()
        {
            Console.WriteLine($"Ticket ID: {TicketId}");
            Console.WriteLine($"Movie: {MovieName}");
            Console.WriteLine($"Price: {Price:C}");
            Console.WriteLine($"Price After Tax: {PriceAfterTax:C}");
        }

        public decimal CalcTotal(decimal taxPercent)
        {
            decimal multiplier = 1.0m + (taxPercent / 100.0m);
            return Price * multiplier;
        }

        public decimal ApplyDiscount(decimal discountAmount)
        {
            if (discountAmount > 0 && discountAmount <= Price)
            {
                Price -= discountAmount;
                return Price;
            }

            return Price;
        }

        public override string ToString()
        {
            return $"Movie: {MovieName}, Type: {Type}, Seat: {Seat}, " +
                   $"Price: {Price:C}, Total after tax: {PriceAfterTax:C}";
        }

        public static int GetTotalTickets()
        {
            return ticketCounter;
        }
    }


    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; }

        public StandardTicket(
            string movieName,
            decimal price,
            string seatNumber)
            : base(
                movieName,
                TicketType.Standard,
                new Seat('A', 1),
                price)
        {
            SeatNumber = seatNumber;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();

            Console.WriteLine($"Seat Number: {SeatNumber}");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $", Seat Number: {SeatNumber}";
        }
    }



    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; }

        public decimal ServiceFee { get; } = 50m;

        public VIPTicket(
            string movieName,
            decimal price,
            bool loungeAccess)
            : base(
                movieName,
                TicketType.VIP,
                new Seat('A', 1),
                price)
        {
            LoungeAccess = loungeAccess;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();

            Console.WriteLine($"Lounge Access: {LoungeAccess}");
            Console.WriteLine($"Service Fee: {ServiceFee:C}");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $", Lounge Access: {LoungeAccess}, " +
                   $"Service Fee: {ServiceFee:C}";
        }
    }


    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; }

        public IMAXTicket(
            string movieName,
            decimal price,
            bool is3D)
            : base(
                movieName,
                TicketType.IMAX,
                new Seat('A', 1),
                price)
        {
            Is3D = is3D;

            if (Is3D)
            {
                Price += 30m;
            }
        }

        public override void PrintTicket()
        {
            base.PrintTicket();

            Console.WriteLine($"3D: {Is3D}");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $", 3D: {Is3D}";
        }
    }



    public class Projector
    {
        public void Start()
        {
            Console.WriteLine("Projector started.");
        }

        public void Stop()
        {
            Console.WriteLine("Projector stopped.");
        }
    }



    public class Cinema
    {
        public string CinemaName { get; }

        private Projector projector;

        private Ticket[] ticketHolder = new Ticket[20];

        public Cinema(string cinemaName)
        {
            CinemaName = cinemaName;

            projector = new Projector();
        }

        public bool AddTicket(Ticket t)
        {
            for (int i = 0; i < ticketHolder.Length; i++)
            {
                if (ticketHolder[i] == null)
                {
                    ticketHolder[i] = t;
                    return true;
                }
            }

            return false;
        }

        public void PrintAllTickets()
        {
            for (int i = 0; i < ticketHolder.Length; i++)
            {
                if (ticketHolder[i] != null)
                {
                    ticketHolder[i].PrintTicket();

                    Console.WriteLine();
                }
            }
        }

        public void OpenCinema()
        {
            Console.WriteLine($"{CinemaName} is opening.");

            projector.Start();
        }

        public void CloseCinema()
        {
            Console.WriteLine($"{CinemaName} is closing.");

            projector.Stop();
        }
    }

    public static class BookingHelper
    {
        private static int aCounter = 0;

        public static double CalcGroupDiscount(
            int numberOfTickets,
            double pricePerTicket)
        {
            double totalPrice =
                numberOfTickets * pricePerTicket;

            if (numberOfTickets >= 5)
            {
                return totalPrice * 0.9;
            }

            return totalPrice;
        }

        public static string GenerateBookingReference()
        {
            return "BK-" + (++aCounter);
        }
    }
}