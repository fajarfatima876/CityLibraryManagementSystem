using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Models
{
    public class Book
    {
        private int bookId;
        private string title;
        private string author;
        private decimal price;
        private bool isIssued;

        private static int nextBookId = 1;
        public int BookId
        {
            get { return bookId; }
        }
            

        public string Title
        {
            get { return title; }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.InvalidInputException("Book title cannot be empty");
                }
                title = value;
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.InvalidInputException(
                        "Book author cannot be empty."
                    );
                }

                author = value;
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                {
                    throw new Exceptions.InvalidInputException(
                        "Book price must be greater than 0."
                    );
                }

                price = value;
            }
        }

        public bool IsIssued
        {
            get { return isIssued; }
            set { isIssued = value; }
        }

        public Book()
        {
            bookId = nextBookId++;
            isIssued = false;
        }

        public Book(
    int id,
    string title,
    string author,
    decimal price)
        {
            bookId = id;
            Title = title;
            Author = author;
            Price = price;
            isIssued = false;

            if(id>=nextBookId)
            {
                nextBookId = id + 1;
            }
        }



    }
}
