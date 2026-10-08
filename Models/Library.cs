using System;
using System.Collections.Generic;
using System.IO;
using CityLibraryManagementSystem.Models;
using CityLibraryManagementSystem.Exceptions;

namespace CityLibraryManagementSystem.Services
{
    public class Library
    {
        private List<Book> books;
        private List<Member> members;
        private List<IssueRecord> issueRecords;

        public Library()
        {
            books = new List<Book>();
            members = new List<Member>();
            issueRecords = new List<IssueRecord>();
        }

        // =====================================================
        // 1. ADD BOOK
        // =====================================================

        public void AddBook(string title, string author, decimal price)
        {
            try
            {
                Book book = new Book();

                book.Title = title;
                book.Author = author;
                book.Price = price;

                books.Add(book);

                Console.WriteLine(
                    $"Book added successfully. Book ID: {book.BookId}"
                );

                LogOperation(
                    $"Book added: ID={book.BookId}, Title={book.Title}"
                );
            }
            catch (LibraryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Failed to add book: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine("Add Book operation completed.");
            }
        }

        // =====================================================
        // 2. VIEW ALL BOOKS
        // =====================================================

        public void ViewAllBooks()
        {
            try
            {
                if (books.Count == 0)
                {
                    Console.WriteLine("No books available.");
                    return;
                }

                Console.WriteLine("\n===== ALL BOOKS =====");

                foreach (Book book in books)
                {
                    Console.WriteLine(
                        $"ID: {book.BookId} | " +
                        $"Title: {book.Title} | " +
                        $"Author: {book.Author} | " +
                        $"Price: {book.Price:F2} | " +
                        $"Issued: {book.IsIssued}"
                    );
                }

                LogOperation("Viewed all books.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Failed to view books: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine("View Books operation completed.");
            }
        }

        // =====================================================
        // 3. SEARCH BOOK BY ID
        // =====================================================

        public Book SearchBook(int id)
        {
            try
            {
                foreach (Book book in books)
                {
                    if (book.BookId == id)
                    {
                        Console.WriteLine(
                            $"ID: {book.BookId} | " +
                            $"Title: {book.Title} | " +
                            $"Author: {book.Author} | " +
                            $"Price: {book.Price:F2} | " +
                            $"Issued: {book.IsIssued}"
                        );

                        LogOperation(
                            $"Book searched by ID: {id}"
                        );

                        return book;
                    }
                }

                throw new BookNotFoundException(
                    $"Book with ID {id} was not found."
                );
            }
            catch (BookNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Book search failed: {ex.Message}"
                );

                throw;
            }
            finally
            {
                Console.WriteLine(
                    "Search Book by ID operation completed."
                );
            }
        }

        // =====================================================
        // 4. SEARCH BOOK BY TITLE
        // =====================================================

        public List<Book> SearchBook(string title)
        {
            try
            {
                List<Book> results = new List<Book>();

                foreach (Book book in books)
                {
                    if (book.Title.Contains(
                        title,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(book);
                    }
                }

                if (results.Count == 0)
                {
                    throw new BookNotFoundException(
                        $"No book found containing '{title}'."
                    );
                }

                Console.WriteLine("\n===== SEARCH RESULTS =====");

                foreach (Book book in results)
                {
                    Console.WriteLine(
                        $"ID: {book.BookId} | " +
                        $"Title: {book.Title} | " +
                        $"Author: {book.Author} | " +
                        $"Price: {book.Price:F2} | " +
                        $"Issued: {book.IsIssued}"
                    );
                }

                LogOperation(
                    $"Book searched by title: {title}"
                );

                return results;
            }
            catch (BookNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Book title search failed: {ex.Message}"
                );

                throw;
            }
            finally
            {
                Console.WriteLine(
                    "Search Book by Title operation completed."
                );
            }
        }

        // =====================================================
        // 5. ADD MEMBER
        // =====================================================

        public void AddMember(string name, string phone)
        {
            try
            {
                Member member = new Member(name, phone);

                members.Add(member);

                Console.WriteLine(
                    $"Member added successfully. Member ID: {member.Id}"
                );

                LogOperation(
                    $"Member added: ID={member.Id}, Name={member.Name}"
                );
            }
            catch (LibraryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Failed to add member: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine("Add Member operation completed.");
            }
        }

        // =====================================================
        // 6. VIEW ALL MEMBERS
        // =====================================================

        public void ViewAllMembers()
        {
            try
            {
                if (members.Count == 0)
                {
                    Console.WriteLine("No members available.");
                    return;
                }

                Console.WriteLine("\n===== ALL MEMBERS =====");

                foreach (Member member in members)
                {
                    member.DisplayInfo();
                }

                LogOperation("Viewed all members.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Failed to view members: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine(
                    "View Members operation completed."
                );
            }
        }

        // =====================================================
        // 7. ISSUE BOOK
        // =====================================================

        public void IssueBook(int bookId, int memberId)
        {
            try
            {
                Book book = null;

                foreach (Book b in books)
                {
                    if (b.BookId == bookId)
                    {
                        book = b;
                        break;
                    }
                }

                if (book == null)
                {
                    throw new BookNotFoundException(
                        $"Book with ID {bookId} was not found."
                    );
                }

                Member member = null;

                foreach (Member m in members)
                {
                    if (m.Id == memberId)
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    throw new MemberNotFoundException(
                        $"Member with ID {memberId} was not found."
                    );
                }

                if (book.IsIssued)
                {
                    throw new BookNotAvailableException(
                        $"Book '{book.Title}' is already issued."
                    );
                }

                book.IsIssued = true;

                IssueRecord record =
                    new IssueRecord(bookId, memberId);

                issueRecords.Add(record);

                Console.WriteLine(
                    $"Book '{book.Title}' issued to {member.Name}."
                );

                Console.WriteLine(
                    $"Issue Record ID: {record.RecordId}"
                );

                LogOperation(
                    $"Book issued: BookID={bookId}, MemberID={memberId}"
                );
            }
            catch (LibraryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Issue book failed: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine("Issue Book operation completed.");
            }
        }

        // =====================================================
        // 8. RETURN BOOK
        // =====================================================

        public void ReturnBook(int bookId)
        {
            try
            {
                Book book = null;

                foreach (Book b in books)
                {
                    if (b.BookId == bookId)
                    {
                        book = b;
                        break;
                    }
                }

                if (book == null)
                {
                    throw new BookNotFoundException(
                        $"Book with ID {bookId} was not found."
                    );
                }

                if (!book.IsIssued)
                {
                    throw new BookNotAvailableException(
                        $"Book '{book.Title}' is not currently issued."
                    );
                }

                IssueRecord activeRecord = null;

                foreach (IssueRecord record in issueRecords)
                {
                    if (record.BookId == bookId &&
                        record.ReturnDate == null)
                    {
                        activeRecord = record;
                        break;
                    }
                }

                if (activeRecord == null)
                {
                    throw new BookNotAvailableException(
                        "No active issue record was found for this book."
                    );
                }

                activeRecord.ReturnDate = DateTime.Now;

                book.IsIssued = false;

                Console.WriteLine(
                    $"Book '{book.Title}' returned successfully."
                );

                Console.WriteLine(
                    $"Return Date: {activeRecord.ReturnDate}"
                );

                LogOperation(
                    $"Book returned: BookID={bookId}"
                );
            }
            catch (LibraryException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Return book failed: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine(
                    "Return Book operation completed."
                );
            }
        }

        // =====================================================
        // 9. VIEW ISSUED BOOKS
        // =====================================================

        public void ViewIssuedBooks()
        {
            try
            {
                bool found = false;

                Console.WriteLine("\n===== ISSUED BOOKS =====");

                foreach (IssueRecord record in issueRecords)
                {
                    if (record.ReturnDate == null)
                    {
                        Book book = null;
                        Member member = null;

                        foreach (Book b in books)
                        {
                            if (b.BookId == record.BookId)
                            {
                                book = b;
                                break;
                            }
                        }

                        foreach (Member m in members)
                        {
                            if (m.Id == record.MemberId)
                            {
                                member = m;
                                break;
                            }
                        }

                        if (book != null && member != null)
                        {
                            Console.WriteLine(
                                $"Record ID: {record.RecordId} | " +
                                $"Book: {book.Title} | " +
                                $"Member: {member.Name} | " +
                                $"Issue Date: {record.IssueDate}"
                            );

                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No books are currently issued.");
                }

                LogOperation("Viewed issued books.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                LogOperation(
                    $"Failed to view issued books: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine(
                    "View Issued Books operation completed."
                );
            }
        }

        // =====================================================
        // 10. SAVE DATA
        // =====================================================

        public void SaveData()
        {
            try
            {
                using (StreamWriter writer =
                       new StreamWriter("books.txt"))
                {
                    foreach (Book book in books)
                    {
                        writer.WriteLine(
                            $"{book.BookId}|{book.Title}|{book.Author}|" +
                            $"{book.Price:F2}|{book.IsIssued}"
                        );
                    }
                }

                using (StreamWriter writer =
                       new StreamWriter("members.txt"))
                {
                    foreach (Member member in members)
                    {
                        writer.WriteLine(
                            $"{member.Id}|{member.Name}|{member.Phone}"
                        );
                    }
                }

                using (StreamWriter writer =
                       new StreamWriter("issues.txt"))
                {
                    foreach (IssueRecord record in issueRecords)
                    {
                        string returnDate =
                            record.ReturnDate.HasValue
                            ? record.ReturnDate.Value.ToString("o")
                            : "";

                        writer.WriteLine(
                            $"{record.RecordId}|{record.BookId}|" +
                            $"{record.MemberId}|" +
                            $"{record.IssueDate:o}|" +
                            $"{returnDate}"
                        );
                    }
                }

                Console.WriteLine(
                    "Data saved successfully."
                );

                LogOperation("All data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error while saving data: {ex.Message}"
                );

                LogOperation(
                    $"Save data failed: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine("Save Data operation completed.");
            }
        }

        // =====================================================
        // 11. LOAD DATA
        // =====================================================

        public void LoadData()
        {
            try
            {
                books.Clear();
                members.Clear();
                issueRecords.Clear();

                // -------------------------
                // Load Books
                // -------------------------

                if (File.Exists("books.txt"))
                {
                    using (StreamReader reader =
                           new StreamReader("books.txt"))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            try
                            {
                                string[] parts = line.Split('|');

                                if (parts.Length != 5)
                                {
                                    LogOperation(
                                        $"Skipped corrupted book line: {line}"
                                    );

                                    continue;
                                }

                                if (!int.TryParse(
                                    parts[0],
                                    out int id))
                                {
                                    continue;
                                }

                                if (!decimal.TryParse(
                                    parts[3],
                                    out decimal price))
                                {
                                    continue;
                                }

                                if (!bool.TryParse(
                                    parts[4],
                                    out bool isIssued))
                                {
                                    continue;
                                }

                                Book book = new Book(
                                    id,
                                    parts[1],
                                    parts[2],
                                    price
                                );

                                book.IsIssued = isIssued;

                                books.Add(book);
                            }
                            catch
                            {
                                LogOperation(
                                    $"Skipped corrupted book line: {line}"
                                );
                            }
                        }
                    }
                }

                // -------------------------
                // Load Members
                // -------------------------

                if (File.Exists("members.txt"))
                {
                    using (StreamReader reader =
                           new StreamReader("members.txt"))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            try
                            {
                                string[] parts = line.Split('|');

                                if (parts.Length != 3)
                                {
                                    LogOperation(
                                        $"Skipped corrupted member line: {line}"
                                    );

                                    continue;
                                }

                                if (!int.TryParse(
                                    parts[0],
                                    out int id))
                                {
                                    continue;
                                }

                                Member member = new Member(
                                    id,
                                    parts[1],
                                    parts[2]
                                );

                                members.Add(member);
                            }
                            catch
                            {
                                LogOperation(
                                    $"Skipped corrupted member line: {line}"
                                );
                            }
                        }
                    }
                }

                // -------------------------
                // Load Issue Records
                // -------------------------

                if (File.Exists("issues.txt"))
                {
                    using (StreamReader reader =
                           new StreamReader("issues.txt"))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            try
                            {
                                string[] parts = line.Split('|');

                                if (parts.Length != 5)
                                {
                                    LogOperation(
                                        $"Skipped corrupted issue line: {line}"
                                    );

                                    continue;
                                }

                                if (!int.TryParse(
                                    parts[0],
                                    out int recordId))
                                {
                                    continue;
                                }

                                if (!int.TryParse(
                                    parts[1],
                                    out int bookId))
                                {
                                    continue;
                                }

                                if (!int.TryParse(
                                    parts[2],
                                    out int memberId))
                                {
                                    continue;
                                }

                                if (!DateTime.TryParse(
                                    parts[3],
                                    out DateTime issueDate))
                                {
                                    continue;
                                }

                                DateTime? returnDate = null;

                                if (!string.IsNullOrWhiteSpace(parts[4]))
                                {
                                    if (!DateTime.TryParse(
                                        parts[4],
                                        out DateTime parsedReturnDate))
                                    {
                                        continue;
                                    }

                                    returnDate = parsedReturnDate;
                                }

                                IssueRecord record =
                                    new IssueRecord(
                                        recordId,
                                        bookId,
                                        memberId,
                                        issueDate,
                                        returnDate
                                    );

                                issueRecords.Add(record);
                            }
                            catch
                            {
                                LogOperation(
                                    $"Skipped corrupted issue line: {line}"
                                );
                            }
                        }
                    }
                }

                Console.WriteLine(
                    "Data loaded successfully."
                );

                LogOperation("All data loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error while loading data: {ex.Message}"
                );

                LogOperation(
                    $"Load data failed: {ex.Message}"
                );
            }
            finally
            {
                Console.WriteLine("Load Data operation completed.");
            }
        }

        // =====================================================
        // 12. LOG OPERATION
        // =====================================================

        private void LogOperation(string message)
        {
            try
            {
                using (StreamWriter writer =
                       new StreamWriter("library.log", true))
                {
                    writer.WriteLine(
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}"
                    );
                }
            }
            catch
            {
                // Logging failure should not crash the program.
            }
        }
    }
}