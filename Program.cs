using CityLibraryManagementSystem.Exceptions;
using CityLibraryManagementSystem.Services;
using System;

namespace CityLibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            while (true)
            {
              //  Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("     CITY LIBRARY MANAGEMENT SYSTEM");
                Console.WriteLine("======================================");

                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Search Book");
                Console.WriteLine("4. Add Member");
                Console.WriteLine("5. View All Members");
                Console.WriteLine("6. Issue Book");
                Console.WriteLine("7. Return Book");
                Console.WriteLine("8. View Issued Books");
                Console.WriteLine("9. Save Data");
                Console.WriteLine("10. Load Data");
                Console.WriteLine("0. Exit");

                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Console.WriteLine("Thank you for using City Library.");
                    break;
                }

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n===== ADD BOOK =====");

                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter author name: ");
                        string author = Console.ReadLine();

                        decimal price;

                        while (true)
                        {
                            Console.Write("Enter book price: ");

                            if (decimal.TryParse(Console.ReadLine(), out price))
                            {
                                if (price > 0)
                                {
                                    break;
                                }

                                Console.WriteLine(
                                    "Price must be greater than 0."
                                );
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Invalid price. Please enter a number."
                                );
                            }
                        }

                        library.AddBook(title, author, price);

                        break;

                    case "2":
                        library.ViewAllBooks();
                        break;

                    case "3":
                        Console.WriteLine("\n===== SEARCH BOOK =====");
                        Console.WriteLine("1. Search by Book ID");
                        Console.WriteLine("2. Search by Title");

                        Console.Write("Enter your choice: ");
                        string searchChoice = Console.ReadLine();

                        if (searchChoice == "1")
                        {
                            int bookId;

                            while (true)
                            {
                                Console.Write("Enter Book ID: ");

                                if (int.TryParse(Console.ReadLine(), out bookId))
                                {
                                    break;
                                }

                                Console.WriteLine(
                                    "Invalid ID. Please enter a number."
                                );
                            }

                            try
                            {
                                library.SearchBook(bookId);
                            }
                            catch (BookNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else if (searchChoice == "2")
                        {
                            Console.Write("Enter book title: ");
                            string searchTitle = Console.ReadLine();

                            try
                            {
                                library.SearchBook(searchTitle);
                            }
                            catch (BookNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid search choice.");
                        }

                        break;

                    case "4":
                        Console.WriteLine("\n===== ADD MEMBER =====");

                        Console.Write("Enter member name: ");
                        string memberName = Console.ReadLine();

                        Console.Write("Enter phone number: ");
                        string phone = Console.ReadLine();

                        library.AddMember(memberName, phone);

                        break;

                    case "5":
                        library.ViewAllMembers();
                        break;

                    case "6":
                        Console.WriteLine("\n===== ISSUE BOOK =====");

                        int issueBookId;

                        while (true)
                        {
                            Console.Write("Enter Book ID: ");

                            if (int.TryParse(Console.ReadLine(), out issueBookId))
                            {
                                break;
                            }

                            Console.WriteLine(
                                "Invalid Book ID. Please enter a number."
                            );
                        }

                        int issueMemberId;

                        while (true)
                        {
                            Console.Write("Enter Member ID: ");

                            if (int.TryParse(Console.ReadLine(), out issueMemberId))
                            {
                                break;
                            }

                            Console.WriteLine(
                                "Invalid Member ID. Please enter a number."
                            );
                        }

                        library.IssueBook(issueBookId, issueMemberId);

                        break;

                    case "7":
                        Console.WriteLine("\n===== RETURN BOOK =====");

                        int returnBookId;

                        while (true)
                        {
                            Console.Write("Enter Book ID: ");

                            if (int.TryParse(Console.ReadLine(), out returnBookId))
                            {
                                break;
                            }

                            Console.WriteLine(
                                "Invalid Book ID. Please enter a number."
                            );
                        }

                        library.ReturnBook(returnBookId);

                        break;

                    case "8":
                        library.ViewIssuedBooks();
                        break;

                    case "9":
                        library.SaveData();
                        break;

                    case "10":
                        library.LoadData();
                        break;

                    default:
                        Console.WriteLine(
                            "\nInvalid choice. Please try again."
                        );
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadKey();
            }
        }
    }
}
