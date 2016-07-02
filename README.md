# CsharpFinalProject
Language C#. Final Project

Derek O'Brien
Cis 232 Section 01
6/23/2016

Final Project Description:
Is a car dealership application created for American Motors. The application requires valid user login credentials to gain access to the application. 
The user will be able to search the inventory database for their desired vehicle and then a list of vehicles currently in inventory meeting that criteria will be returned. 
It will allow the user to display a full specification sheet of the vehicle including the picture of the vehicle. The user will be able to print a copy of the specification sheet. 
The user will also be able to calculate the monthly payment of a loan based on price and loan options. The user will be able to add, delete, or modify the inventory database. 
The user will be able to create a vehicle order list and save the order list to a file. 

Login info: USERNAME: admin PASSWORD: password

Database Pathway: D:\C#\FinalProject\FinalProject\App_Data\UserLogins.mdf
		    D:\C#\FinalProject\FinalProject\App_Data\InventoryDatabase.mdf

Forms (7):

LoginForm: user must enter valid login credentials to gain access to application

MenuForm: allows user to access vehicle search, inventory modification, order form, or loan calculator.

SearchForm: allows user to search inventory database for desired vehicle, it then allows user to view vehicle details sheet.

VehicleDetailsForm: gives the user access to vehicle image and specifications, from this form user can print specifications or choose to get loan calculation.

LoanCalculatorForm: allows user to enter details based on the loan they wish to inquiry about, and will calculate a monthly loan payment based on information entered, 
they then can print a quote if they desire.

InventoryForm: allows user to add, remove, or edit the vehicle inventory database.

OrderForm: allows user to special order vehicles, or to order vehicles to replenish inventory. 	
After vehicles are added to a vehicle order list it can then be submitted so that it is save for the proper personal to fufill order.

Classes (5):

FinancingClass: is used to calculate the monthly loan payment.
	Properties: VehiclePrice, DownPayment, TradeInValue, SalesTax, InterestRate, and NumberOfMonths 
	(Set accessors that are executed when properties are assigned the new values entered by the user to be used in the calculation). 			
	MonthlyPayment is read only and is used to display the monthly payment on the LoanCalculatorForm.
	Method: CalcMonthlyPayment() uses the formula 
		EMI = ( P × r × (1+r)^n ) / ((1+r)^n - 1) that is used in the industry to calculate monthly payments of a loan. 
	It uses the values assigned through the properties in the calculation.
		CalcPower() was created so that a decimal value could be raised to the appropriate power.

AutomobileClass: (parent class) Class contains fields that are common to all automobiles.
	Properites: Make, Model, Year, VIN, Color, Mileage, Engine, Transmission, DriveType, Doors, and Price 
	(Set accessors that are executed when properties are assigned the new values entered by user on the vehicle order after selection of child class). 
	Saves on retyping several identical members shared by the car, truck, and suv vehicles.

CarClass: (child of Automobile class) Inheirts fields from automobile class as well as contains specific fields to cars.
	Properties: Convertible, GroundEffects, Electric (Set accessors that are executed when 	properties are assigned the new values entered by user on the vehicle OrderForm after 
	choosing vehicle type (child class).

SUVClass: (child of Automobile class) Inheirts fields from automobile class as well as contains specific fields to SUVs.
	Properties: ThirdRowSeating, LiftGate, RemovableTop(Set accessors that are executed when properties are assigned the new values entered by user on the vehicle OrderForm 	
	after choosing vehicle type (child class).

TruckClass: (child of Automobile class) Inheirts fields from automobile class as well as contains specific fields to trucks.
	Properties: CabType, BedLenght, TowPackage (Set accessors that are executed when properties are assigned the new values entered by user on the vehicle OrderForm after 	
	choosing vehicle type (child class).

Arrays:
Jagged Array used on VehicleSearchForm. The vehicleArray subscripts relate to the vehicle Manufactures in the 'Make' combo box. Chevrolet, Dodge, Ford, and Jeep with values of 	
0-3 respectively. When the user select the make that value is used to load the 'Model' 	combo with the items associated with vehicleArray[subscript] gathered from the user selection. 
A list of string typed name vehicles was used on the OrderForm. The user entered vehicle information based on vehicle type. The vehicle type created a new child class of that 	particular 
vehicle type thus inheriting from the parent automobile class. The properties 	were assigned new values which excuted the set accessors, and then the get accessors 	
allowed the new values to be read into the list and displayed in the listbox, then being saved to a file for the appropriate personal to place order if user submitted the list.

Database: 
The LoginForm uses the userlogin database. The userlogin database consist of a table that contains rows with two columns (userid, password). The LoginForm uses two textboxes to obtain the 
userid and password. When the user clicks enter the values are taken in as paramaters and used in the query to search the database. It uses a count to find where that userid and password 
is in the database if they exist. If both values exist at the same count value then the user entered a value entry and has access to the application, if not invalid entry.

The SearchForm uses the Inventory database. It uses a parameterized query to search the database using the parameter values selected in the combo boxes (Make, Model, Start Year, End Year). 
If a vehicle(s) exist in the database meeting the search criteria the 	selected columns (StockNumber, Year, Make, Model) will be displayed in a listbox, and if no vehicle matches then a 
message will be displayed indicating the vehicle does not exist in inventory. The VehicleDetailsForm upon form loading will query the Inventory database based on the string value passed to 
it from the SearchForm, lstSearchResults listbox, that was obtained by doing a substring method of the selected item. Doing so obtains the stock number of the vehicle which is an identity 
column value. 

The VehicleDetailsForm's labels and picture box will be loaded with the corresponding vehicle data.

The InventoryForm use a detailsview to allow the user to add, delete, or modify the inventory database.

File: 
On the OrderForm the user creates a list of vehicles that need to be ordered. The list is then 	saved to vehicleorderlist.txt using streamwriter and file.appendtext.

Inheritance: 
Parent: AutomobileClass. Child: CarClass, SUVClass, TruckClass. The AutomobileClass contains members that are common amongst all automobile types. 
The CarClass contains members specific to cars(convertible, groundeffects, electric). The SUVClass contains members specific to SUVs(thirdrowseating, lift-gate, removabletop). 
The TruckClass 	contains members specific to trucks(cabtype, bedlength, towpackage). Using inheirtance allowed to for code to reused instead of repeated for every vehicle class. 	
The classes were used on the vehicle OrderForm to take in user entered vehicle data and use the properites write them to a list and displayed in a listbox to further be processed.

Delegates:  
Was used on the LoginForm. The user login form is hidden after the user successfully logs in. If the user were to close out of the other forms it would not close the LoginForm because 	
it was the main form and never was closed which creates a memory problem. To solve this issue a delegate was used to close the LoginForm when the MenuForm is closed. 
Delegate: menu.Closed += (s, args) => this.Close(); The LoginForm listens for the 	closed delegate to use the function to close the LoginForm when the MenuForm is closed.

Learned and challenges:
	
Wanting to keep security as a goal, I researched how to perform safer queries of sql databases. I came a crossing using parameterized queries. 
The benefit of using parameterize queries is it escapes SQL syntax, so that all parameter content is treated like a value and not like a command. 
In the command line I created i was able to add parameters giving the parameters the value of my textboxs or combo boxes, thus help protecting against SQL injections.

I also learned to take the query results that I loaded into a listbox and select an item and use it to load vehicles(rows) information on another form. I used the substring method to obtain
the first 4 characters of the string, which was the unique identity column value. I stored that value in a string and then accessed it from my new form and using that value did another query 
of the database to load that specific vehicles information into the labels and picturebox on the vehicle specification page.

Wanting to be able to hidden my user login form and still be able to close it so it wouldn't continue to run in the background when other forms were closed out I did some research and found a 
delegate that allowed me to close the login form when the menu form was closed. It was interesting and useful because I could not close out the login form while hidden or close it out with out 
also closing the menu form. More details on the delegate can be in the delegate section of this paper.

Printing a form has been a challenging experience for me because there is not a lot of information on printing forms in C#.Net. I found a microsoft turtorial to use printing forms power pack, and got that to work becuase I had experience with it in VB.Net. However, it no longer works at the school so I had to come up with a new approach. I found a method that essentially takes a screen show by creating a new bitmap the width and height of the form. Then creating a new rectangle and using the draw method sizes the image to be printed. It however, isn't perfect becuase it chops some of the right border.
	
Also, found in researching that saving the filepath of an image in a database is better practice than saving the actual image in the database. It saves space on the database and allows the images to be accessed faster.

Regular expression were very useful for data validations particular for the vehicle information. They were challenging at first to figure out the various ways things needed to be typed to give the desired result but afterwards it gave a good amount of control over the validation. I could compare for specific word choices (ie No, Yes, Power, Manuel, etc) with one line of code. I could specify 4 numerical digits for the year, or one 1 digit for engine and doors. I really like how specific it allows you to set your compare values for validation purposes. 
