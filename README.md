# CEN4010-TeamProject
Team Project for CEN4010-RVC1188 (Software Engineering I) Fall 2018 at Florida International University

## Features
- **Book Browsing and Sorting:** Allows users to browse books by genre, top sellers in our book store, and book rating with pagination based on 10 or 20 results. It also allows sorting by book title, author, price, book rating, and release date.

- **Profile Management:** Users can manage their login credentials (ID, password), personal information (name, email address, home address), nickname for book rating and commenting, credit card information (multiple), and shipping address (multiple). Physical addresses, email addresses, and credit card info is verified as valid and passwords must meet current security standards.

- **Shopping Cart: Users** can easily access their cart from any page, view the same information displayed in the book list, change the quantity, remove it from their cart, or save it for later. A subtotal for all items in their shopping cart is displayed at the bottom. Items saved for later appear below that.

- **Book Details:** Displays book name, book cover (which can be enlarged when clicked), author and bio, book description, genre, publishing info (publisher, release date, etc.), book rating, and comments. It also contains a hyperlink  in the author’s name to a list of other books by the same author.

- **Book Rating and Commenting:** The application uses a five-star rating system. Users can only rate or comment on a book if they’ve purchased it, and may select whether they show their nickname (defined in their profile) or remain anonymous. A single comment is limited to the number of characters, which can fit within half the browser window (so that there are at least two comments which can appear at the same time).

## Architecture
### Frontend
The frontend logic and user interface is written using Angular 6 (JavaScript), as well as Bootstrap (HTML and CSS). User authentication is handled by Google's Firebase.

### Backend
All backend logic is written using ASP.NET Core. The backend services connect to Google's Firebase service to query any data requested by the frontend. The backend is implemented as a RESTful API to be consumed by the frontend.

### Database
The database is a NoSQL database handled by Google's Firebase service.

## Links
- [Angular](https://angular.io/)
- [Bootstrap](https://getbootstrap.com/)
- [ASP.NET Core](https://www.asp.net/)
- [Google Firebase](https://firebase.google.com/)
