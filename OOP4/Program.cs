using OOP2.MovieTicketSystem;

namespace OOP4
{
    internal class Program
    {

        // Q1
        /*
        Static binding: The method to call is determined at compile time.
        It happens with method overloading.

        Dynamic binding: The method to call is determined at runtime.
        It happens with method overriding using virtual/override.
        */

        // Q2
        /*
        Method overloading: Same method name but different parameters.
        It is resolved at compile time.

        Method overriding: A child class provides a new implementation
        of a method inherited from the parent class.
        It is resolved at runtime.
        */

        // Q3
        /*
        virtual: Allows a parent method to be overridden by a child class.

        override: Provides a new implementation of a virtual/abstract
        method from the parent class.

        abstract: Defines a method without implementation that must be
        overridden by a non-abstract child class.

        new: Hides the parent method instead of overriding it.
        */


        public static void ProcessTicket(Ticket t)
        {
            t.PrintTicket();
        }


        static void Main(string[] args)
        {
            Cinema cinema = new Cinema("Galaxy Cinema");

            cinema.OpenCinema();

            StandardTicket standardTicket =
                new StandardTicket("Avengers", 100m, "A12");

            VIPTicket vipTicket =
                new VIPTicket("Batman", 200m, true);

            IMAXTicket imaxTicket =
                new IMAXTicket("Avatar", 250m, true);

            standardTicket.SetPrice(150m);

            vipTicket.SetPrice(200m, 1.5m);

            cinema.AddTicket(standardTicket);
            cinema.AddTicket(vipTicket);
            cinema.AddTicket(imaxTicket);

            cinema.PrintAllTickets();

            ProcessTicket(imaxTicket);

            cinema.CloseCinema();
        }
    }
}
