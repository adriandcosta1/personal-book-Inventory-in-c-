using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace BookInventoryAssignment
{

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public double Price { get; set; }
    }
    class BookInventory
    {
        static int bookIndex = 0;
        bool addInfo(Book aBook, List<Book> books)
        {



            books.Add(aBook);
            books.Sort((lib1, lib2) => lib1.Title.CompareTo(lib2.Title));

            ++BookInventory.bookIndex;

            return true;
        }

        void removeBook(int index, bool isAll, List<Book> books)
        {
            if (bookIndex == 0)
            {
                Console.WriteLine("The book list is empty. Try to add some book then try again.");
            }
            else if (isAll)
            {
                int idx = 0;
                for (int i = 0; i < BookInventory.bookIndex; i++)
                {     
                    books.RemoveAt(idx);
                    
                }
                BookInventory.bookIndex = 0;
                Console.WriteLine("All the books are removed.");
            }
            else if ((index <= 0) || (index > bookIndex))
            {
                Console.WriteLine("Enter a valid index from 1 to {0} of the book to be removed.", bookIndex);
            }
            else if ((bookIndex > 0) && ((index > 0) && (index <= bookIndex)))
            {
                books.RemoveAt(index - 1);
                Console.WriteLine("Removed the book at index number {0}", index);
                --BookInventory.bookIndex;

            }
        }
        void showOptions()
        {
            if (Console.BackgroundColor == ConsoleColor.Black)
            {
                //Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("{0,62}", "This program is licensed under GPLv3");
            Console.WriteLine("{0,55}", "Adrian'sBookInventory");
            Console.WriteLine(new string('=', 100));

            Console.WriteLine("1) Do you want to add a book to the list?");
            Console.WriteLine("2) Do you want to remove a book from the list?");
            Console.WriteLine("3) Do you want to remove all the books from the list?");
            Console.WriteLine("4) Do you want to display the list (by the title's ascending order)?");
            Console.WriteLine("5) Do you want to display the list(by the title's descending order)?");
            Console.WriteLine("6) Do you want to add the book prices and see the total?");
            Console.WriteLine("7) Enter 7 to discontinue. ");
            if (Console.BackgroundColor == ConsoleColor.Black)
            {

                Console.ForegroundColor = ConsoleColor.White;

            }


        }
        void printBookInfo(List<Book> books, bool isDescending)
        {

            int idx = 1;
            if (bookIndex == 0)
            {
                Console.WriteLine("The book list is empty. Try to add some book then try again.");
            }
            else
            {
                if (isDescending)
                {
                    books.Reverse();
                }
                else
                {

                }
                IEnumerator iEnum = books.GetEnumerator();
                //double price = 0.0;
                
                while (iEnum.MoveNext())
                {
                    Book bookInfo = (Book)iEnum.Current;
                    //Double.TryParse(bookInfo.Price, out price);
                    Console.WriteLine("{0,5})  {1,-35}  {2, -25}  {3, -14}  {4, 9:C}", idx, bookInfo.Title, bookInfo.Author, bookInfo.Isbn, bookInfo.Price);
                    ++idx;
                }
                if (isDescending)
                {
                    books.Reverse();
                }
                

            }
            Console.WriteLine();


        }

        void addPrice(List<Book> books)
        {
            if (bookIndex == 0)
            {
                Console.WriteLine("The book list is empty. Try to add some book then try again.");
            }
            else
            {
                Double accumulator = 0.0;
                IEnumerator iEnum = books.GetEnumerator();
                int idx = 1;
                //double price = 0.0;
                while (iEnum.MoveNext())
                {
                    Book bookInfo = (Book)iEnum.Current;
                    //Double.TryParse(bookInfo[3], out price);
                    Console.WriteLine("{0,5})  {1,35}  {2, 9:C}", idx, bookInfo.Title, bookInfo.Price);

                    accumulator += bookInfo.Price;
                    ++idx;
                }
                Console.WriteLine(new String('=', 54));
                Console.WriteLine("{0,6}  {1,46:C}", "Total:", accumulator);
            }
        }


        static void Main(string[] args)
        {
            string inputTitle = "", inputAuthorName = "", inputIsbnNumber = "", inputPrice = "0.0";

            BookInventory lib = new BookInventory();
            List<Book> books = new List<Book>();
            
            Int16 inputNum = 0;
            bool isInputRight = true;
            const int MIN_INPUT = 1, MAX_INPUT = 7;
            string sentence;
            do
            {

                Book aBook = new Book();
                do
                {
                    lib.showOptions();
                    Console.Write("Enter a valid input between 1 and 7 inclusive: ");


                    inputNum = 0;
                    sentence = Console.ReadLine();
                    isInputRight = Int16.TryParse(sentence, out inputNum);
                } while ((!isInputRight) && ((inputNum < MIN_INPUT) && (inputNum > MAX_INPUT)));

                if (inputNum == 1)
                {
                    Console.Write("Enter the book title: ");
                    inputTitle = Console.ReadLine();
                    aBook.Title = inputTitle;

                    Console.Write("Enter author name: ");
                    inputAuthorName = Console.ReadLine();
                    aBook.Author = inputAuthorName;
                    bool allCharactersInStringAreDigits = false;
                    do
                    {
                        Console.Write("Enter ISBN number (10 or 13 digits): ");
                        inputIsbnNumber = Console.ReadLine();
                        allCharactersInStringAreDigits = inputIsbnNumber.All(char.IsDigit);
                    } while (((inputIsbnNumber.Length != 10) && (inputIsbnNumber.Length != 13)) || (!allCharactersInStringAreDigits));
                    aBook.Isbn = inputIsbnNumber;

                    bool isPriceRight = false;
                    double price = 0.0;
                    do
                    {
                        Console.Write("Enter price: ");
                        inputPrice = Console.ReadLine();
                        isPriceRight = Double.TryParse(inputPrice, out price);
                    } while (!isPriceRight);
                    aBook.Price = price;
                    lib.addInfo(aBook, books);
                }
                else if (inputNum == 2)
                {
                    Int32 indexNumber = -1;
                    if (BookInventory.bookIndex > 0)
                    {
                        lib.printBookInfo(books, false);
                        Console.Write("Enter the index number of the book to be removed: ");
                        bool isIndexInputRight = false;

                        do
                        {
                            isIndexInputRight = Int32.TryParse(Console.ReadLine(), out indexNumber);
                        } while (!isIndexInputRight);
                    }
                    lib.removeBook(indexNumber, false, books);

                }
                else if (inputNum == 3)
                {

                    int bookNum = -1;

                    lib.removeBook(bookNum, true, books);
                }
                else if (inputNum == 4)
                {

                    lib.printBookInfo(books, false);
                }
                else if (inputNum == 5)
                {
                    lib.printBookInfo(books, true);
                }
                else if (inputNum == 6)
                {
                    lib.addPrice(books);
                }

                else if (inputNum == 7)
                {

                    Console.WriteLine("This program is exiting...");
                }

            } while (inputNum != 7);
            Console.WriteLine();
        }
    }
}
