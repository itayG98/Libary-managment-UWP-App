# Overview

![MainPage](https://user-images.githubusercontent.com/91791115/180998374-829277c7-a4c4-4b2c-ae52-044b099c9832.png)

**please login with ID:000000000 and password: 12345**
My app main purpose is to manage a libary costumers and items add let users order buy and return from the library's stock.
It has 2 types of users first Costumer and second is Employee and can handle its tasks using MookData class Logic class and the model claas.

**Main Features:**

1.	Signing up Costumer / Employee

2. Logging in into appropriate page

3.	Choose library item to see its Fields

4.	Borrow / Buy /Return specific item

5.	Change Password

6.	Change user’s details

**Employee’s extra features:**

7.	Add library items to the system

8.	Change person’s discount or details

9.	View all Borrowers / costumer /Employees / all Users

10.	Delete from the system User except itself

11.	Delete Book from the system




# Optional additions which were not included in current version
*Duo to time shortage


**Features:**

•	Billing system and implementation of discount

•	Adding more verified codes to publisher and countries

•	Search by name of users or library items

•	Add more profiles of user deriving from costumer’s functionality

•	Add a data track of all transaction which made.

•	Add reviews feature on the libary items.



# Summery
I developed 4 projects for my application :


1.Library model

class library for the objects needed in the library and their behavior.
This class library is dependent and does not work with any special references DLL’s.

2.Logic and mook Data

class library for mook data and its Repository interface and logic object for managing the application .
This class library work with the library model.

3.Library app

The UWP project having variety of UWP pages the user can navigate through
This project familiar with both Model and Logic.

4.Library app Tests

I made test methods to the class libraries projects and due to confusion, I made some methods as integration test (Testing how the Logic class and the Irepository integrate with the mook data class)


**Key conclusion from zoom talk:**

I need to split the library repository class into 2 classes for Person’s repository and Library item’s repository



![OrderItemPage](https://user-images.githubusercontent.com/91791115/180969287-230fbbc1-6fde-43d2-a907-6f383528ec61.png)
