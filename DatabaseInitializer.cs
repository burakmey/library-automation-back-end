namespace library_automation_back_end
{
    public static class DatabaseInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                try
                {
                    context.Database.EnsureCreated();
                    var roles = context.Roles.FirstOrDefault();
                    if (roles == null)
                    {
                        context.Roles.AddRange(
                            new Role { Name = "Yönetici" },
                            new Role { Name = "Kullanıcı" }
                            );
                        context.Countries.AddRange(
                            new Country { Name = "Türkiye" }, //1
                            new Country { Name = "Azerbaycan" }, //2
                            new Country { Name = "Rusya" }, //3
                            new Country { Name = "İngiltere" }, //4
                            new Country { Name = "Fransa" }, //5
                            new Country { Name = "İtalya" }, //6
                            new Country { Name = "İspanya" }, //7
                            new Country { Name = "Belçika" }, //8
                            new Country { Name = "Yunanistan" }, //9
                            new Country { Name = "Portekiz" }, //10
                            new Country { Name = "Amerika" }, //11
                            new Country { Name = "Brezilya" }, //12
                            new Country { Name = "Arjantin" }, //13
                            new Country { Name = "Kanada" }, //14
                            new Country { Name = "Japonya" }, //15
                            new Country { Name = "Almanya" }, //16
                            new Country { Name = "Çin" } //17
                            );
                        context.Languages.AddRange(
                            new Language { Name = "Türkçe" }, //1
                            new Language { Name = "Rusça" }, //2
                            new Language { Name = "İngilizce" }, //3
                            new Language { Name = "Fransızca" }, //4
                            new Language { Name = "İtalyanca" }, //5
                            new Language { Name = "İspanyolca" }, //6
                            new Language { Name = "Yunanca" }, //7
                            new Language { Name = "Portekizce" }, //8
                            new Language { Name = "Japonca" }, //9
                            new Language { Name = "Almanca" }, //10
                            new Language { Name = "Flemenkçe" }, //11
                            new Language { Name = "Çince" } //12
                            );
                        context.BorrowSituations.AddRange(
                            new BorrowSituation { Situation = "Ödünç Alınmış" },
                            new BorrowSituation { Situation = "İade Edilmiş" },
                            new BorrowSituation { Situation = "Süresi Geçmiş" }
                            );
                        context.ReserveSituations.AddRange(
                            new ReserveSituation { Situation = "Beklemede" },
                            new ReserveSituation { Situation = "Ödünç Alınmış" },
                            new ReserveSituation { Situation = "Süresi Geçmiş" }
                            );
                        context.Categories.AddRange(
                            new Category { Name = "Bilim Kurgu" }, //1
                            new Category { Name = "Tarihsel Kurgu" }, //2
                            new Category { Name = "Edebi Kurgu" }, //3
                            new Category { Name = "Fantastik" }, //4
                            new Category { Name = "Thriller" }, //5
                            new Category { Name = "Gerilim" }, //6
                            new Category { Name = "Romantik" }, //7
                            new Category { Name = "Macera" }, //8
                            new Category { Name = "Çizgi Roman" }, //9
                            new Category { Name = "Gizem" }, //10
                            new Category { Name = "Biyografi" }, //11
                            new Category { Name = "Klasikler" }, //12
                            new Category { Name = "Tarih" }, //13
                            new Category { Name = "Polisiye Kurgu" }, //14
                            new Category { Name = "Anı" }, //15
                            new Category { Name = "Ansiklopedi" } //16
                            );
                        context.Books.AddRange(
                            new Book { Name = "Cesur Yeni Dünya", Year = 1932, Count = 1, PageCount = 272, LanguageId = 1, PublisherId = 1 }, //1
                            new Book { Name = "Üç Cisim Problemi", Year = 2008, Count = 2, PageCount = 416, LanguageId = 1, PublisherId = 1 }, //2
                            new Book { Name = "Kurtuluş Projesi", Year = 2021, Count = 3, PageCount = 536, LanguageId = 3, PublisherId = 1 }, //3
                            new Book { Name = "Tanrı'nın Savaşcıları", Year = 2001, Count = 1, PageCount = 439, LanguageId = 1, PublisherId = 2 }, //4
                            new Book { Name = "Osmancık", Year = 1982, Count = 2, PageCount = 376, LanguageId = 1, PublisherId = 3 }, //5
                            new Book { Name = "Ben Claudius", Year = 1934, Count = 3, PageCount = 528, LanguageId = 3, PublisherId = 4 }, //6
                            new Book { Name = "Harry Potter", Year = 1997, Count = 7, PageCount = 276, LanguageId = 1, PublisherId = 5 }, //7
                            new Book { Name = "Yüzüklerin Efendisi", Year = 1954, Count = 3, PageCount = 416, LanguageId = 1, PublisherId = 6 }, //8
                            new Book { Name = "Güz Alacakaranlığı Ejderhaları", Year = 1984, Count = 3, PageCount = 412, LanguageId = 4, PublisherId = 7 }, //9
                            new Book { Name = "Masumiyet Müzesi", Year = 2008, Count = 1, PageCount = 465, LanguageId = 1, PublisherId = 5 }, //10
                            new Book { Name = "Kırmızı Saçlı Kadın", Year = 2016, Count = 1, PageCount = 220, LanguageId = 1, PublisherId = 5 }, //11
                            new Book { Name = "Notre Dame'ın Kamburu", Year = 1831, Count = 1, PageCount = 560, LanguageId = 1, PublisherId = 8 } //
                            //new Book { Name = "", Year = 0, Count = 0, PageCount = 0, LanguageId = 0, PublisherId = 0 } //
                            );
                        context.Publishers.AddRange(
                            new Publisher { Name = "İthaki Yayınları", CountryId = 1 }, //1
                            new Publisher { Name = "Panama Yayıncılık", CountryId = 1 }, //2
                            new Publisher { Name = "Ötüken Neşriyat", CountryId = 1 }, //3
                            new Publisher { Name = "Profile Books", CountryId = 4 }, //4
                            new Publisher { Name = "Yapı Kredi Yayınları", CountryId = 1 }, //5
                            new Publisher { Name = "Metis Yayınları", CountryId = 1 }, //6
                            new Publisher { Name = "Actes Sud", CountryId = 5 }, //7
                            new Publisher { Name = "İş Bankası Kültür Yayınları", CountryId = 1 } //8
                            //new Publisher { Name = "", CountryId = 0 } //
                            );
                        context.Authors.AddRange(
                            new Author { Name = "Aldous Huxley", CountryId = 4 }, //1
                            new Author { Name = "Cixin Liu", CountryId = 17 }, //2
                            new Author { Name = "Andy Weir", CountryId = 11 }, //3
                            new Author { Name = "James Reston", CountryId = 11 }, //4
                            new Author { Name = "Tağrık Buğra", CountryId = 1 }, //5
                            new Author { Name = "Robert Groves", CountryId = 4 }, //6
                            new Author { Name = "Joanne Kathleen Rowling", CountryId = 4 }, //7
                            new Author { Name = "John Ronald Reuel Tolkien", CountryId = 4 }, //8
                            new Author { Name = "Margaret Weis", CountryId = 11 }, //9
                            new Author { Name = "Tracy Hickman", CountryId = 11 }, //10
                            new Author { Name = "Orhan Pamuk", CountryId = 1 }, //11
                            new Author { Name = "Victor Hugo", CountryId = 5 } //
                            //new Author { Name = "", CountryId = 0 } //
                            );

                        context.BookAuthors.AddRange(
                            new BookAuthor { BookId = 1, AuthorId = 1 },
                            new BookAuthor { BookId = 2, AuthorId = 2 },
                            new BookAuthor { BookId = 3, AuthorId = 3 },
                            new BookAuthor { BookId = 4, AuthorId = 4 },
                            new BookAuthor { BookId = 5, AuthorId = 5 },
                            new BookAuthor { BookId = 6, AuthorId = 6 },
                            new BookAuthor { BookId = 7, AuthorId = 7 },
                            new BookAuthor { BookId = 8, AuthorId = 8 },
                            new BookAuthor { BookId = 9, AuthorId = 9 },
                            new BookAuthor { BookId = 9, AuthorId = 10 },
                            new BookAuthor { BookId = 10, AuthorId = 11 },
                            new BookAuthor { BookId = 11, AuthorId = 11 },
                            new BookAuthor { BookId = 12, AuthorId = 12 }
                            );
                        context.BookCategories.AddRange(
                            new BookCategory { BookId = 1, CategoryId = 1 },
                            new BookCategory { BookId = 1, CategoryId = 12 },
                            new BookCategory { BookId = 2, CategoryId = 1 },
                            new BookCategory { BookId = 3, CategoryId = 1 },
                            new BookCategory { BookId = 4, CategoryId = 2 },
                            new BookCategory { BookId = 4, CategoryId = 11 },
                            new BookCategory { BookId = 5, CategoryId = 2 },
                            new BookCategory { BookId = 6, CategoryId = 2 },
                            new BookCategory { BookId = 7, CategoryId = 4 },
                            new BookCategory { BookId = 7, CategoryId = 8 },
                            new BookCategory { BookId = 7, CategoryId = 10 },
                            new BookCategory { BookId = 8, CategoryId = 4 },
                            new BookCategory { BookId = 8, CategoryId = 8 },
                            new BookCategory { BookId = 9, CategoryId = 4 },
                            new BookCategory { BookId = 9, CategoryId = 10 },
                            new BookCategory { BookId = 10, CategoryId = 3 },
                            new BookCategory { BookId = 11, CategoryId = 3 },
                            new BookCategory { BookId = 12, CategoryId = 3 }
                            );
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Init datas are existent!");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return app;
            }
        }
    }
}
