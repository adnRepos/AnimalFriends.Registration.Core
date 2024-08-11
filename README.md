AnimalFrieds.Registation API follows simple pattern of Controllers/Services/Data access with Models supporting Dto and Dbo.

Validation:
In Models - > RegistrationInputs => validates and enforces validatoin rules for the POST Endpoint
i.e First/LastName - between 3 & 50
    ReferenceNumber - XX-123456
    Email - test@te.co.uk 
Also, for DateofBirth to validate age is over 18 pr move we have custom attribute **Over18AgeValidation**

Service - controller passes the input to service layers which converts the input to Dbo object
Data -> accepts Dbo object and inserts the Dbo object to database using Dapper

DB - > hold common connection strings along with master connection
On inital we check **InitDb** if the DB and the table donesnt exists then we create them.

![image](https://github.com/user-attachments/assets/0b720de0-5c5a-4955-842b-b08ec5b58bd5)





