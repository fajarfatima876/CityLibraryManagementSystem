using System;
using System.Collections.Generic;
using System.Text;

namespace CityLibraryManagementSystem.Models
{
    public class IssueRecord
    {
        private int recordId;
        private int bookId;
        private int memberId;
        private DateTime issueDate;
        private DateTime? returnDate;

        private static int nextRecordId = 1;

        public int RecordId
        {
            get
            {
                return recordId;
            }
        }

        public int BookId
        {
            get
            {
                return bookId;
            }
        }

        public int MemberId
        {
            get { return memberId; }
        }

        public DateTime IssueDate
        {
            get { return issueDate; }
        }

        public DateTime? ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }

        // Constructor for creating a new issue
        public IssueRecord(int bookId, int memberId)
        {
            recordId = nextRecordId++;
            bookId = bookId;
            memberId = memberId;
            issueDate = DateTime.Now;
            returnDate = null;
        }

        // Constructor for loading saved data
        public IssueRecord(
            int recordId,
            int bookId,
            int memberId,
            DateTime issueDate,
            DateTime? returnDate)
        {
            recordId = recordId;
            bookId = bookId;
            memberId = memberId;
            issueDate = issueDate;
            returnDate = returnDate;

            if (recordId >= nextRecordId)
            {
                nextRecordId = recordId + 1;
            }
        }
    }
}
    
