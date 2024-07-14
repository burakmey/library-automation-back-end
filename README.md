# Library Automation Back-End

## Overview

This project is a back-end system used to manage the operations of a library. It supports modules for searching and borrowing books, logging in, registering, and more. Some actions, such as borrowing and returning, are considered user desires that require approval from library administrators.

## Features

- JWT for authentication

- Authorization ("User" and "Admin" roles)
- Web API
- SQL Server for database
- Entity Framework Core for queries
- Fine calculation and management (currently inactive)

### Modules for Anonymous Users

- Searching book from the library
- Reviewing a specific book that in the library
- Logging in and registering

### Modules for Registered Users

- Sending borrow and return desire for admin
- Canceling any request before admin approval
- Making reservations for books
- Listing borrowed books, reservated books and pending desires

### Modules for Admins

- Accepting or rejecting users desires
- Tracking borrowed and reserved books
- Adding new books to the library
- Updating existing books in the library

## ER Diagram
![ER drawio](https://github.com/user-attachments/assets/c517b562-f514-49a2-9abc-c668550927ba)

## Modules
![Modules drawio](https://github.com/user-attachments/assets/8fe91ce1-7426-483b-9928-72122d243a3a)
