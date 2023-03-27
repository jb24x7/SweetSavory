# Sweet Treats


#### By James Provance

## Technologies Used

* C#
* .Net 6
* HTML
* JavaScript
* JSON
* SQL
* EF core

## Description
This web application is made so that a user may create an account to login. Then they may create foods and flavor to go along with the foods.

## Setup/Installation Requirements

1. Clone this repo.
2. Open your terminal (e.g., Terminal or GitBash) and navigate to this project's production directory named "Factory".
3. Create a file named ['appsettings.json'] in the production directory (Factory) and include a new database connection string. The string should be as follows:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[DATABASE-NAME-HERE];uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];",
  }
}
Create a database name, update username and password to match the username and password of your computer.
4. Enter 'dotnet ef database update' in the terminal inside the production directory (this will create the database schema in MySQL which the appication will access later), enter 'dotnet watch run' in the command line to start the project in development mode with a watcher (Optionally, you can run "dotnet build" to compile the app without running it). 
5. Open the browser to https://localhost:5001.

## Known Bugs

* no known bugs at this time

## License
MIT

Copyright (c) 2023 James Provance

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.