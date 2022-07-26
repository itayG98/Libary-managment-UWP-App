**Overview**


My application has 2 types of users first Costumer and second is Employee and can handle its tasks using MookData class and Logic class.
Features:

•	Signing up Costumer / Employee

•	Logging in into appropriate page

•	Choose library item to see its Fields

•	Borrow / Buy /Return specific item

•	Change Password

•	Change user’s details

*Employee’s extra features:*

•	Add library items to the system

•	Change person’s discount or details

•	View all Borrowers / costumer /Employees / all Users

•	Delete from the system User except itself

•	Delete Book from the system

**Optional additions which were not included in current version**


*Duo to time shortage

Features:
•	Billing system and implementation of discount

•	Adding more verified codes to publisher and countries

•	Search by name of users or library items

•	Add more profiles of user deriving from costumer’s functionality

•	Add a data track of all transaction which made.



**Summery**
I developed 4 projects for my application :


•	Library model
class library for the objects needed in the library and their behavior.
This class library is dependent and does not work with any special references DLL’s.

•	Logic and mook Data
class library for mook data and its Repository interface and logic object for managing the application .
This class library work with the library model.

•	Library app
The UWP project having variety of UWP pages the user can navigate through
This project familiar with both Model and Logic.

•	Library app Tests
I made test methods to the class libraries projects and due to confusion, I made some methods as integration test (Testing how the Logic class and the Irepository integrate with the mook data class)

**Key fixes from zoom talk:**

I may split the library repository class into 2 classes for Person’s repository and Library item’s repository


![OrderItemPage](https://user-images.githubusercontent.com/91791115/180969287-230fbbc1-6fde-43d2-a907-6f383528ec61.png)
