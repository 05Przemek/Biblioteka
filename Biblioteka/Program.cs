using System;
using System.Collections.Generic;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
    }
}

interface ILibrary
{
    void AddBook(Book book);
    void RemoveBook(string isbn);
    List<Book> SearchBook(string keyword);
    void DisplayAvailableBooks();
    void DisplayAllBooks();
}

class Library : ILibrary
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(string isbn)
    {
        Book bookToRemove = books.Find(b => b.ISBN == isbn);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Książka o ISBN {isbn} została usunięta z biblioteki.");
        }
        else
        {
            Console.WriteLine("Książka o podanym ISBN nie istnieje w bibliotece.");
        }
    }

    public List<Book> SearchBook(string keyword)
    {
        return books.FindAll(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public void DisplayAvailableBooks()
    {
        Console.WriteLine("Dostępne książki w bibliotece:");
        foreach (Book book in books)
        {
            if (book.IsAvailable)
            {
                Console.WriteLine($"- {book.Title} by {book.Author} (ISBN: {book.ISBN})");
            }
        }
    }

    public void DisplayAllBooks()
    {
        Console.WriteLine("Wszystkie książki w bibliotece:");
        foreach (Book book in books)
        {
            Console.WriteLine($"- {book.Title} by {book.Author} (ISBN: {book.ISBN}), Dostępna: {book.IsAvailable}");
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        library.AddBook(new Book("Five Night's at Freddy's The Silver Eyes", "Scott Cawton", "9788416867356"));
        library.AddBook(new Book("Harry Potter and the order of Phoenixr", "J.K Rowling", "9780439358064"));
        library.AddBook(new Book("Mein Kampf", "Adolf Hitler", "9780395925034"));
        library.AddBook(new Book("F.A.R.M. System", "Rich Koslowski", "9781603095150"));

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nWybierz jedną z opcji:");
            Console.WriteLine("1. Dodaj nową książkę");
            Console.WriteLine("2. Usuń książkę");
            Console.WriteLine("3. Wyszukaj książkę");
            Console.WriteLine("4. Wyświetl dostępne książki");
            Console.WriteLine("5. Wyświetl wszystkie książki");
            Console.WriteLine("0. Wyjdź z programu");

            char option = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (option)
            {
                case '1':
                    Console.Write("Podaj tytuł książki: ");
                    string title = Console.ReadLine();
                    Console.Write("Podaj autora książki: ");
                    string author = Console.ReadLine();
                    Console.Write("Podaj ISBN książki: ");
                    string isbn = Console.ReadLine();
                    library.AddBook(new Book(title, author, isbn));
                    Console.WriteLine("Książka została dodana do biblioteki.");
                    break;

                case '2':
                    Console.Write("Podaj ISBN książki do usunięcia: ");
                    string isbnToDelete = Console.ReadLine();
                    library.RemoveBook(isbnToDelete);
                    break;

                case '3':
                    Console.Write("Wyszukaj książkę po tytule lub autorze: ");
                    string searchKeyword = Console.ReadLine();
                    List<Book> searchResults = library.SearchBook(searchKeyword);
                    Console.WriteLine("Wyniki wyszukiwania:");
                    foreach (Book book in searchResults)
                    {
                        Console.WriteLine($"- {book.Title} by {book.Author} (ISBN: {book.ISBN})");
                    }
                    break;

                case '4':
                    library.DisplayAvailableBooks();
                    break;

                case '5':
                    library.DisplayAllBooks();
                    break;

                case '0':
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
}