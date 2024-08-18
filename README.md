# web-scraper

This project is a Worker Service designed to scrape and store stock market data 
from the Dhaka Stock Exchange (DSE) website. The service continuously monitors the market status 
and collects stock prices when the market is open.
Data is stored in a SQL Server database, with tables for Company and StockPrice information.

# Features:

# Market Status Monitoring: 
Automatically checks if the market is open or closed.
Data collection is only performed when the market is open.

# Web Scraping: 
Utilizes the HTML Agility Pack to extract stock data from DSE Latest Share Price.

# Data Storage: 
Captures stock prices for each company every minute and 
stores them in a SQL Server database using the provided connection string.

# Entity Framework: 
Includes migrations for setting up the Company and StockPrice tables.

# Worker Service: 
The service is configured to run independently of Visual Studio, providing continuous data collection for at least two days.

# Technology Stack:

C#
.NET Worker Service
HTML Agility Pack
Entity Framework
SQL Server
