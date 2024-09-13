# Community Library DVD Management System

## Overview

The **Community Library DVD Management System** is a console-based application designed to manage DVD rentals for a community library. The system allows library staff to manage a collection of DVDs, register and remove members, and track the borrowing and returning of DVDs. Library members can browse available movies, borrow and return DVDs, and view their currently borrowed movies.

The system includes several key functionalities for both **Staff** and **Members**, such as:
- Adding and removing DVDs from the system
- Registering and removing members
- Borrowing and returning DVDs
- Displaying top borrowed movies
- Finding contact details of members
- Listing current borrowing activities

## Table of Contents
- [Features](#features)
- [Class Structure](#class-structure)
- [Usage](#usage)
- [Staff Features](#staff-features)
- [Member Features](#member-features)

---

## Features

### For Staff
- **Manage DVDs:** Add new DVDs, remove DVDs, and adjust the number of available copies.
- **Manage Members:** Register new members, remove existing members (provided they do not have any borrowed DVDs).
- **Find Members Renting Specific Movies:** Retrieve a list of members who are currently renting a specific movie.
- **View Top 3 Most Borrowed Movies:** View the top 3 most borrowed movies.

### For Members
- **Browse Movies:** View the list of all available movies sorted alphabetically.
- **Borrow Movies:** Borrow available DVDs.
- **Return Movies:** Return previously borrowed DVDs.
- **View Borrowed Movies:** See a list of movies currently borrowed by the member.
- **Top Borrowed Movies:** View the top 3 most borrowed DVDs by members.

---

## Class Structure

### 1. `Member`
Represents a member of the library. Each member has:
- `FirstName`, `LastName`, `ContactNumber`, and `Password` properties.
- Can borrow up to 5 movies at a time.
- Methods to borrow, return, and get the list of borrowed movies.

### 2. `MemberCollection`
Manages the collection of members in the system:
- Can add new members, remove members, find a specific member, or list members who are renting a particular movie.
  
### 3. `Movie`
Represents a DVD in the library:
- Includes properties like `Title`, `Genre`, `Classification`, `Duration`, `AvailableCopies`, and `BorrowCount`.
- Methods for borrowing, returning, adding, and removing copies.

### 4. `MovieCollection`
A hash table implementation that stores movies:
- Uses a linked list to handle hash collisions.
- Methods to add, remove, and find movies by their title.
- A method to display the top 3 most borrowed movies.

### 5. `Node`
Used in the linked list structure for the `MovieCollection` class to handle collisions in the hash table.

### 6. `Program`
The main class that drives the console-based user interface. It handles:
- **Staff Login & Menu:** Authentication for staff users and subsequent staff actions.
- **Member Login & Menu:** Member authentication and member-specific actions.

---

## Usage

### Main Menu
When the application starts, you will be prompted with the main menu options:
1. **Staff**
2. **Member**
3. **End the program**

### Staff Features

1. **Add DVDs to system:** 
   - Input DVD details such as title, genre, classification, duration, and available copies.
   - Add more copies to existing DVDs.

2. **Remove DVDs from system:**
   - Input the title of the movie to reduce the number of copies or remove the movie entirely if no copies remain.

3. **Register a new member:**
   - Register a member by providing their first name, last name, contact number, and a four-digit password.

4. **Remove a registered member:**
   - Remove a member only if they do not have any DVDs borrowed.

5. **Find a member's contact number:**
   - Retrieve a member’s phone number by providing their first and last names.

6. **Find members currently renting a particular movie:**
   - Input a movie title to list all members who are currently renting the movie.

### Member Features

1. **Browse all movies:**
   - Display all available DVDs with their details such as title, genre, classification, duration, and available copies.

2. **Display movie information:**
   - Get detailed information about a specific movie by entering its title.

3. **Borrow a movie:**
   - Borrow a movie by providing the title. Members can borrow up to 5 movies at a time and cannot borrow multiple copies of the same movie.

4. **Return a movie:**
   - Return a borrowed DVD by providing the title.

5. **List current borrowed movies:**
   - Display the list of movies that the member has borrowed.

6. **Display the top 3 movies rented by the members:**
   - View the three most borrowed DVDs across all members.

---

Community Library DVD Management System is a simple console-based application built for educational purposes, demonstrating basic data structures, OOP principles, and the use of collections and hash tables.
