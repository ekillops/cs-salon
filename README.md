# _CS Salon_

#### _C# Salon, 12/9/2016_

#### By _**Erik Killops**_

## Description

_A simple website for a hypothetical Salon. Allows manager to create, read, update, and delete records for their stylists and clients._

#### Specs

##### Stylist Specs

| BEHAVIOR                                 | INPUT                                              | OUTPUT                  |
|------------------------------------------|----------------------------------------------------|-------------------------|
| Allow user to create new stylist         | Name: Jane Doe, Phone: 503-123-4567, Clients: none | Stylist created.        |
| Save stylist when created (or updated)   | Jane Doe created.                                  | Saved to DB.            |
| Allow user to update stylist details     | Name: Jane Doe, Phone: 503-891-0123, Clients: none | Stylist updated.        |
| Allow user to delete stylist             | Jane Doe - Delete                                  | Stylist deleted.        |
| Allow user to search for stylist by name | Search: 'Jane'                                     | Retrieve: Jane Doe etc. |

##### Client Specs

| BEHAVIOR                                 | INPUT                                                      | OUTPUT                  |
|------------------------------------------|------------------------------------------------------------|-------------------------|
| Allow user to create new client         | Name: Sally Smith, Phone: 503-111-2222, Stylist: Jane Doe   | Client created.         |
| Save client when created (or updated)   | Jane Doe created.                                           | Saved to DB.            |
| Allow user to update client details     | Name: Sally Smith Phone: 503-111-2222 Stylist: Sarah Carter | Client updated.         |
| Allow user to delete client             | Sally Smith - Delete                                        | Client deleted.         |
| Allow user to search for client by name | Search: 'Sally'                                             | Retrieve: Sally Smith   |

## Setup/Installation Requirements

_Requires Windows, .Net, SMSS, and SQL SERVER_

1. Clone repository.
2. In SSMS, open hair_salon.sql
3. Add "CREATE DATABASE [hair_salon]" on the first line and "GO" on the following line, and press "Enter".
4. Save the file and click Execute.
5. Repeat steps 2-5 for hair_salon_test.
6. In Powershell,  run ">dnx kestrel" and visit "localhost:5004".


## Known Bugs

The website does not prevent against duplicate entries. If you need to remove duplicates you will have to do it manually via their individual page.


## Technologies Used

HTML, C#, Nancy, Razor, Xunit, SQL.

### License

*GPL*

Copyright (c) 2016 **_Erik Killops, Epicodus_**
