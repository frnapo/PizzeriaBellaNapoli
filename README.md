# BellaNapoli Pizzeria Management System

Welcome to the BellaNapoli Pizzeria Management System, a comprehensive web application designed to streamline the operations of the BellaNapoli Pizzeria. This project utilizes the Model-View-Controller (MVC) architectural pattern, not MVC Core, and leverages Entity Framework for data management.

## Features

- **Order Management**: Easily manage customer orders, from placement through preparation to delivery.
- **Menu Customization**: Update and modify the pizzeria menu, including prices, ingredients, pics and delivery time.
- **Customer Database**: Keep track of customer information, preferences, and order history.
- **Reporting**: Generate reports on sales.
- **Dynamic Navigation**: Utilize the MVC pattern for seamless navigation across different sections of the application.

## Technologies & Tools

![MVC](https://img.shields.io/badge/MVC-Pattern-blue.svg)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-ORM-blue.svg)
![.NET Framework](https://img.shields.io/badge/.NET_Framework-Compatible-blue.svg)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-blue.svg)

- ASP.NET MVC (Model-View-Controller architecture, not MVC Core)
- Entity Framework
- SQL Server Management Studio (SSMS) for database management

### Prerequisites

Ensure you have the following installed on your machine:

- Visual Studio, with support for ASP.NET MVC projects
- SQL Server and SQL Server Management Studio (SSMS)
- .NET Framework compatible with the project

### Installation

1. **Clone the repository** to your local machine: git clone https://github.com/frnapo/PizzeriaBellaNapoli.git
2. **Navigate to the project directory**: cd BellaNapoliPizzeriaManagement
3. **Restore the Database**:

- Open SQL Server Management Studio (SSMS).
- Connect to your SQL Server instance.
- Right-click on `Databases` and select `Restore Database`.
- Choose the `Device` option and select the `BellaNapoli.bak` file provided in the project directory.
- Follow the prompts to restore the database for use with the application.

4. **Open the project** in Visual Studio.

5. **Build the project** to install necessary packages and dependencies.

6. **Run the application** by pressing `F5` or clicking on the `Run` button in Visual Studio.

Your web browser should open automatically to the homepage of the BellaNapoli Pizzeria Management System, where you can start exploring its features.

## Acknowledgments

Special thanks to EPICODE for the project idea.
