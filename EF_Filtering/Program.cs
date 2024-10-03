using EF_DataAccess.Data;
using EF_Models.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

//using (ApplicationDbContext context = new()) {
//    context.Database.EnsureCreated();
//    if (context.Database.GetPendingMigrations().Count() > 0) { 
//        context.Database.Migrate();
//    }
//}

//AddBook();
//GetAllBooks();
//GetBook();
//GetBookOnPubId();
GetUsingFind();

void GetUsingFind()
{
    using var context = new ApplicationDbContext();
    //var book = context.Books.Find(5);
    Book? book = context.Books.SingleOrDefault(u=>u.Publisher_Id==10);

    if (book != null)
    {
        Console.WriteLine(book.Title + "-" + book.ISBN);
    }
}

void GetBookOnPubId()
{
    using var context = new ApplicationDbContext();
    var paramISBN = "12345678";
    var books = context.Books.Where(u=>u.Publisher_Id==1 && u.ISBN == paramISBN).FirstOrDefault();
    //foreach (var book in books)
    //{
    Console.WriteLine(books.Title + "-" + books.ISBN);
    //}
}

void GetBook()
{
    using var context = new ApplicationDbContext();
    //Book book = context.Books.First();
    //Fluent_Book book = context.Fluent_Books.First();
    Fluent_Book? book = context.Fluent_Books.FirstOrDefault();
    if (book != null)
    { 
        Console.WriteLine(book.Title + "-" + book.ISBN);
    }
}

void GetAllBooks()
{
    using var context = new ApplicationDbContext();
    var books = context.Books.ToList();
    foreach (var book in books)
    {
        Console.WriteLine(book.Title+"-"+book.ISBN);
    }
}

void AddBook()
{
    Book book = new() {Title="ADO.NET Book", ISBN="2134531", Price=123.88m, Publisher_Id=2 };
    using var context = new ApplicationDbContext();
    var books = context.Books.Add(book); 
    context.SaveChanges();  
}