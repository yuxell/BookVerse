async function getBooks() {
    try {
        let response = await fetch("/Home/JSBooks");

        if (!response.ok) {
            throw new Error(`Hata: ${response.status}`);
        }
        
        let books = await response.json();
        updateBookList(books);
    } catch (error) {
        console.error("Kitapları çekerken hata oluştu:", error);
    }
}

// Kitapları sayfaya ekleyen fonksiyon
function updateBookList(books) {
    let bookList = document.getElementById("bookList");
    bookList.innerHTML = "";

    books.forEach(book => {
        bookList.innerHTML += `
                <div>
                    <img src="img/books/${book.CoverImage}" alt="${book.Title}" width="80">
                    <form method="post">
                    <h3><a href="Book/BookDetail/${book.BookID}">${book.Title}</a></h3>
                    </form>
                    <p>Price: ${book.Price}</p>
                    <p>Author: <a href="Author/${book.AuthorID}">${book.AuthorFullName}</a></p>
                </div>
            `;
    });
}
