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

_Requires Windows and .Net_

Clone repository, run ">dnx kestrel" in Powershell and visit "localhost:5004".

**FILL IN DATABASE INSTRUCTIONS HERE!!!**

## Known Bugs

None.


## Technologies Used

HTML, C#, Nancy, Razor, Xunit, SQL.

### License

*GPL*

Copyright (c) 2016 **_Erik Killops, Epicodus_**
