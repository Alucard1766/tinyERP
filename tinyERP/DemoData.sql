EXEC sp_MSForEachTable 'ALTER TABLE dbo.Budgets NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Customers NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Categories NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Transactions NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Orders NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Documents NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Offers NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.OrderConfirmations NOCHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Invoices NOCHECK CONSTRAINT ALL' 
GO

EXEC sp_MSForEachTable 'DELETE FROM dbo.Budgets'
EXEC sp_MSForEachTable 'DELETE FROM dbo.Customers'
EXEC sp_MSForEachTable 'DELETE FROM dbo.Categories'
EXEC sp_MSForEachTable 'DELETE FROM dbo.Transactions'
EXEC sp_MSForEachTable 'DELETE FROM dbo.Orders'
EXEC sp_MSForEachTable 'DELETE FROM dbo.Documents'
EXEC sp_MSForEachTable 'DELETE FROM dbo.Offers'
EXEC sp_MSForEachTable 'DELETE FROM dbo.OrderConfirmations'
EXEC sp_MSForEachTable 'DELETE FROM dbo.Invoices'
GO

EXEC sp_MSForEachTable 'ALTER TABLE dbo.Budgets WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Customers WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Categories WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Transactions WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Orders WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Documents WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Offers WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.OrderConfirmations WITH CHECK CHECK CONSTRAINT ALL' 
EXEC sp_MSForEachTable 'ALTER TABLE dbo.Invoices WITH CHECK CHECK CONSTRAINT ALL' 
GO


SET IDENTITY_INSERT dbo.Budgets ON
insert into dbo.Budgets (Id, Year, Expenses, Revenue) values (1, 2012, 10000, 61300);
insert into dbo.Budgets (Id, Year, Expenses, Revenue) values (2, 2013, 45000, 61000);
insert into dbo.Budgets (Id, Year, Expenses, Revenue) values (3, 2014, 23400, 25700);
insert into dbo.Budgets (Id, Year, Expenses, Revenue) values (4, 2015, 21200, 66400);
insert into dbo.Budgets (Id, Year, Expenses, Revenue) values (5, 2016, 20600, 26700);
insert into dbo.Budgets (Id, Year, Expenses, Revenue) values (6, 2017, 15000, 28000);
SET IDENTITY_INSERT dbo.Budgets OFF
GO

SET IDENTITY_INSERT dbo.Categories ON
insert into dbo.Categories (Id, Name) values (1, 'Büro');
insert into dbo.Categories (Id, Name) values (2, 'Auto');
insert into dbo.Categories (Id, Name) values (3, 'Infrastruktur');
insert into dbo.Categories (Id, Name) values (4, 'Kommunikation');
insert into dbo.Categories (Id, Name) values (5, 'Abgaben');
insert into dbo.Categories (Id, Name) values (6, 'Auftrag');
insert into dbo.Categories (Id, Name) values (7, 'Weiterbildung');
insert into dbo.Categories (Id, Name) values (8, 'Marketing/Werbung');
insert into dbo.Categories (Id, Name) values (9, 'Wareneinkauf');
insert into dbo.Categories (Id, Name, ParentCategoryId) values (10, 'Schreibmaterial', 1);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (11, 'Druckmaterial', 1);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (12, 'Service', 2);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (14, 'Treibstoff', 2);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (15, 'Versicherung', 2);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (16, 'Miete', 3);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (17, 'Versicherung', 3);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (18, 'Einrichtung', 3);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (19, 'Mobile', 4);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (20, 'Festnetz', 4);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (21, 'Internet', 4);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (22, 'Steuern', 5);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (23, 'Sozialabgaben', 5);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (24, 'Pensionskasse', 5);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (25, 'Dienstleistungen', 6);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (26, 'Warenertrag', 6);
insert into dbo.Categories (Id, Name, ParentCategoryId) values (27, 'Lizenzeinkünfte', 6);
SET IDENTITY_INSERT dbo.Categories OFF
GO

SET IDENTITY_INSERT dbo.Transactions ON
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (1, 'Transaction 02703', 837.3, 'true', 0, '04.08.2013', 2, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (2, 'Transaction 29535', 55.6, 'false', 67, '04.01.2015', 4, 1);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (3, 'Transaction 49378', 412.6, 'true', 0, '07.08.2012', 1, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (4, 'Transaction 27188', 358.8, 'false', 58, '09.08.2015', 4, 2);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (5, 'Transaction 64890', 102.8, 'true', 0, '03.18.2014', 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (6, 'Transaction 52874', 15.9, 'false', 99, '08.24.2012', 1, 3);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (7, 'Transaction 48503', 516.2, 'true', 0, '01.17.2014', 3, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (8, 'Transaction 35898', 788.7, 'false', 31, '03.27.2013', 2, 4);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (9, 'Transaction 57883', 872.1, 'true', 0, '02.14.2015', 4, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (10, 'Transaction 35993', 636.1, 'false', 77, '07.06.2014', 3, 5);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (11, 'Transaction 75301', 70.4, 'true', 0, '01.05.2012', 1, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (12, 'Transaction 90491', 83.3, 'true', 0, '10.19.2013', 2, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (13, 'Transaction 16702', 397.9, 'false', 24, '04.04.2013', 2, 7);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (14, 'Transaction 67972', 537.3, 'false', 32, '04.04.2017', 6, 8);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (15, 'Transaction 72118', 116.6, 'true', 0, '12.26.2012', 1, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (16, 'Transaction 99712', 428.9, 'true', 0, '03.10.2013', 2, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (17, 'Transaction 86717', 853.5, 'true', 0, '01.17.2013', 2, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (18, 'Transaction 44061', 576.1, 'false', 47, '11.26.2013', 2, 9);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (19, 'Transaction 47723', 591.8, 'true', 0, '03.08.2014', 3, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (20, 'Transaction 36586', 286.8, 'true', 0, '11.16.2013', 2, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (21, 'Transaction 24199', 465.3, 'true', 0, '02.27.2014', 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (22, 'Transaction 08839', 843.2, 'false', 57, '03.01.2014', 3, 10);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (23, 'Transaction 98509', 761.1, 'true', 0, '12.30.2014', 3, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (24, 'Transaction 84874', 311.8, 'false', 23, '06.28.2015', 4, 11);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (25, 'Transaction 48097', 135.6, 'false', 71, '11.07.2013', 2, 12);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (26, 'Transaction 19122', 139.8, 'false', 56, '07.28.2012', 1, 16);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (27, 'Transaction 25133', 221.4, 'true', 0, '12.23.2014', 3, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (28, 'Transaction 81939', 392.7, 'false', 68, '06.20.2014', 3, 14);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (29, 'Transaction 06843', 866.0, 'true', 0, '07.14.2012', 1, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (30, 'Transaction 06826', 164.3, 'false', 78, '03.07.2016', 5, 15);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (31, 'Transaction 83420', 477.1, 'true', 0, '12.02.2012', 1, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (32, 'Transaction 04394', 842.6, 'true', 0, '07.08.2014', 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (33, 'Transaction 36937', 801.5, 'true', 0, '05.13.2014', 3, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (34, 'Transaction 96309', 180.3, 'false', 16, '07.29.2015', 4, 1);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (35, 'Transaction 80631', 14.7, 'true', 0, '05.08.2013', 2, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (36, 'Transaction 76212', 277.3, 'true', 0, '02.22.2012', 1, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (37, 'Transaction 27865', 202.0, 'true', 0, '09.11.2014', 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (38, 'Transaction 14931', 516.7, 'false', 55, '09.07.2012', 1, 2);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (39, 'Transaction 43671', 847.9, 'false', 30, '05.20.2012', 1, 3);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (40, 'Transaction 29605', 275.5, 'true', 0, '04.23.2013', 2, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (41, 'Transaction 48947', 251.2, 'false', 53, '02.14.2012', 1, 4);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (42, 'Transaction 00678', 811.6, 'true', 0, '03.10.2014', 3, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (43, 'Transaction 59941', 418.7, 'false', 10, '04.22.2016', 5, 5);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (44, 'Transaction 55433', 735.1, 'true', 0, '11.16.2012', 1, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (45, 'Transaction 04546', 808.7, 'true', 0, '02.12.2016', 5, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (46, 'Transaction 98131', 786.5, 'true', 0, '02.22.2015', 4, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (47, 'Transaction 25069', 280.4, 'true', 0, '11.30.2016', 5, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (48, 'Transaction 08976', 313.0, 'false', 60, '02.03.2014', 3, 9);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (49, 'Transaction 94492', 273.1, 'true', 0, '01.29.2014', 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (50, 'Transaction 55378', 425.1, 'true', 0, '02.26.2015', 4, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (51, 'Transaction 48721', 565.8, 'true', 0, '02.14.2014', 3, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (52, 'Transaction 26688', 240.9, 'true', 0, '10.10.2015', 4, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (53, 'Transaction 54640', 721.2, 'false', 88, '09.15.2016', 5, 7);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (54, 'Transaction 93930', 268.8, 'false', 91, '09.29.2016', 5, 8);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (55, 'Transaction 77970', 247.4, 'true', 0, '12.20.2014', 3, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (56, 'Transaction 30733', 509.2, 'false', 35, '08.20.2016', 5, 9);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (57, 'Transaction 17941', 769.3, 'true', 0, '07.16.2016', 5, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (58, 'Transaction 01887', 64.2, 'true', 0, '06.08.2016', 5, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (59, 'Transaction 86472', 243.6, 'true', 0, '09.15.2015', 4, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (60, 'Transaction 60225', 232.9, 'false', 82, '04.23.2013', 2, 10);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (61, 'Transaction 92402', 275.3, 'true', 0, '10.10.2012', 1, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (62, 'Transaction 66470', 657.9, 'false', 93, '08.08.2016', 5, 11);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (63, 'Transaction 40910', 379.7, 'true', 0, '01.19.2016', 5, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (64, 'Transaction 53578', 380.1, 'true', 0, '06.19.2016', 5, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (65, 'Transaction 54362', 698.7, 'false', 32, '01.29.2012', 1, 12);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (66, 'Transaction 58863', 552.5, 'false', 92, '04.13.2016', 5, 16);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (67, 'Transaction 63006', 623.1, 'false', 92, '06.22.2014', 3, 14);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (68, 'Transaction 91516', 612.4, 'true', 0, '10.01.2016', 5, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (69, 'Transaction 65493', 426.3, 'true', 0, '10.15.2013', 2, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (70, 'Transaction 74366', 664.2, 'false', 3, '10.20.2013', 2, 15);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (71, 'Transaction 94488', 665.1, 'false', 13, '04.22.2017', 6, 1);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (72, 'Transaction 48920', 840.1, 'false', 69, '08.15.2013', 2, 2);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (73, 'Transaction 69589', 713.1, 'true', 0, '03.09.2014', 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (74, 'Transaction 97958', 578.2, 'true', 0, '10.04.2014', 3, 27);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (75, 'Transaction 27607', 732.6, 'false', 33, '06.17.2015', 4, 3);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (76, 'Transaction 09444', 895.0, 'false', 32, '08.25.2013', 2, 4);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (77, 'Transaction 53764', 222.1, 'false', 11, '07.13.2012', 1, 5);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (78, 'Transaction 91300', 585.2, 'false', 20, '06.30.2012', 1, 9);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (79, 'Transaction 00145', 381.5, 'true', 0, '04.07.2016', 5, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, BudgetId, CategoryId) values (80, 'Transaction 80356', 445.4, 'true', 0, '07.28.2015', 4, 26);
SET IDENTITY_INSERT dbo.Transactions OFF
GO

SET IDENTITY_INSERT dbo.Customers ON
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (1, 'Elcom', 'Sarge', 'Hermann Group', '0584 Quincy Center', 4090, 'Krajan Timur Suger Kidul', 'selcom0@who.int');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (2, 'Connikie', 'Damien', 'Mohr, Stiedemann and Orn', '75 Reindahl Center', 9576, 'Gandusari', 'dconnikie1@gmpg.org');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (3, 'Jorez', 'Murray', 'Fisher, Wyman and Sanford', '43 Michigan Circle', 8482, 'Kratovo', 'mjorez2@facebook.com');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (4, 'Nortcliffe', 'Bartie', 'Bernhard-Blick', '49 7th Drive', 5800, 'Lingcheng', 'bnortcliffe3@cpanel.net');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (5, 'McGurk', 'Maggi', 'Kunze, Gleichner and Cremin', '411 Crownhardt Drive', 6696, 'Xinjie', 'mmcgurk4@abc.net.au');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (6, 'Jindrak', 'Archy', 'Kuphal-Monahan', '50 Roth Place', 6624, 'Ngurensiti', 'ajindrak5@princeton.edu');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (7, 'Melody', 'Cosetta', 'Skiles Inc', '540 Pleasure Street', 7259, 'Bang Nam Priao', 'cmelody6@icq.com');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (8, 'Critten', 'Ezequiel', 'Stark-Baumbach', '73 Goodland Terrace', 2420, 'Orita Eruwa', 'ecritten7@senate.gov');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (9, 'Scalia', 'Conway', 'Kihn Group', '209 Rutledge Lane', 4029, 'Gualán', 'cscalia8@comcast.net');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (10, 'Clears', 'Jerrie', 'Muller and Sons', '292 Burrows Plaza', 5976, 'Uryupinsk', 'jclears9@deviantart.com');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (11, 'Malinowski', 'Christin', 'Reynolds, Ritchie and Durgan', '54 Victoria Park', 3399, 'Caraga', 'cmalinowskia@mlb.com');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (12, 'Faircley', 'Granthem', 'Schuster and Sons', '650 Mandrake Parkway', 8519, 'Asembagus', 'gfaircleyb@purevolume.com');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (13, 'Scintsbury', 'Renato', 'Trantow, Hoeger and Wyman', '0953 Gateway Way', 4084, 'Paris 07', 'rscintsburyc@google.de');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (14, 'Jarville', 'Abe', 'Lowe, Nikolaus and Kreiger', '5571 La Follette Junction', 4452, 'Cereté', 'ajarvilled@google.co.jp');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (15, 'MacLardie', 'Malissia', 'Smith and Sons', '00 Russell Junction', 5625, 'As Sab‘ Biyār', 'mmaclardiee@fema.gov');
insert into dbo.Customers (Id, LastName, FirstName, Company, Street, Zip, City, Email) values (16, 'Meier', 'Samuel', 'Meier Engineering AG', 'Holzweg 4', 1234, 'Musterort', 'smeier@meier.ch');
SET IDENTITY_INSERT dbo.Customers OFF
GO

SET IDENTITY_INSERT dbo.Orders ON
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (1, 'Order 21050', 0, '05/13/2016', '11/20/2016', 1);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (2, 'Order 99176', 1, '07/12/2013', '11/07/2014', 1);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (3, 'Order 23487', 1, '03/16/2015', '11/17/2016', 2);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (4, 'Order 12674', 2, '09/20/2015', '04/16/2016', 2);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (5, 'Order 88515', 2, '05/23/2013', '11/25/2016', 3);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (6, 'Order 29931', 1, '05/03/2012', '11/18/2013', 3);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (7, 'Order 37783', 1, '07/11/2014', '06/27/2016', 4);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (8, 'Order 75626', 0, '04/26/2015', '01/30/2017', 4);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (9, 'Order 84900', 1, '06/09/2014', '08/05/2014', 4);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (10, 'Order 98716', 2, '04/17/2014', '07/20/2016', 5);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (11, 'Order 26388', 2, '02/19/2015', '03/26/2016', 6);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (12, 'Order 79476', 2, '11/30/2016', '01/24/2017', 7);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (13, 'Order 01048', 2, '12/16/2014', '03/21/2016', 8);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (14, 'Order 05143', 0, '06/05/2015', '07/19/2015', 9);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (15, 'Order 16592', 0, '05/05/2014', '10/22/2016', 10);
insert into dbo.Orders (Id, Title, State, CreationDate, StateModificationDate, CustomerId) values (16, 'Serverbestellung', 1, '05/05/2017', '05/22/2017', 16);
SET IDENTITY_INSERT dbo.Orders OFF
GO

SET IDENTITY_INSERT dbo.Documents ON
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (1, 'Document 77367', 'Nicht physisch vorhanden', '09/25/2014', 'Hac.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (2, 'Document 58461', null, '12/31/2016', 'Sit.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (3, 'Document 40894', null, '08/07/2014', 'LaciniaSapien.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (4, 'Document 75359', 'In Ablage', '06/11/2015', 'Sem.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (5, 'Document 92740', null, '03/31/2013', 'Mus.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (6, 'Document 34633', 'In Ablage', '08/07/2013', 'Consequat.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (7, 'Document 25680', null, '08/19/2013', 'Bibendum.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (8, 'Document 81750', 'Unterschrieben', '05/26/2013', 'UtAt.pdf');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (9, 'Document 19333', 'In Ablage', '05/03/2015', 'SedMagnaAt.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (10, 'Document 57468', 'Nicht physisch vorhanden', '05/30/2014', 'Quisque.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (11, 'Document 44241', 'In Ablage', '11/22/2015', 'At.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (12, 'Document 10103', 'In Ablage', '05/26/2013', 'Aliquam.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (13, 'Document 50865', 'Nicht physisch vorhanden', '08/16/2013', 'Lectus.pdf');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (14, 'Document 32543', 'Nicht physisch vorhanden', '08/07/2013', 'CongueEgetSemper.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (15, 'Document 20070', null, '04/09/2017', 'PlateaDictumst.doc');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (16, 'Document 32762', 'Unterschrieben', '06/29/2014', 'EuOrciMauris.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (17, 'Document 87857', 'Nicht physisch vorhanden', '09/06/2014', 'DuisAtVelit.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (18, 'Document 04377', 'Nicht physisch vorhanden', '11/02/2013', 'AFeugiat.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (19, 'Document 60706', 'Nicht physisch vorhanden', '09/28/2016', 'UltricesPosuere.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (20, 'Document 96952', null, '10/30/2014', 'SedLacus.pdf');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (21, 'Document 86365', null, '11/12/2014', 'A.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (22, 'Document 62019', 'Beim Kunden zur Unterschrift', '01/27/2013', 'OdioConsequatVarius.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (23, 'Document 54530', null, '05/05/2013', 'Luctus.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (24, 'Document 69569', 'Unterschrieben', '03/08/2015', 'HacHabitassePlatea.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (25, 'Document 72586', null, '08/14/2016', 'AmetDiam.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (26, 'Document 59394', 'Beim Kunden zur Unterschrift', '03/24/2016', 'VestibulumQuamSapien.doc');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (27, 'Document 78476', null, '09/21/2016', 'BlanditUltricesEnim.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (28, 'Document 23529', 'Nicht physisch vorhanden', '02/04/2017', 'VestibulumEget.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (29, 'Document 36427', null, '06/08/2016', 'Adipiscing.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (30, 'Document 72162', null, '08/24/2016', 'UtEratId.doc');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (31, 'Document 97801', 'In Ablage', '07/13/2016', 'Velit.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (32, 'Document 90567', 'In Ablage', '05/07/2017', 'NonummyIntegerNon.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (33, 'Document 52033', 'In Ablage', '01/07/2014', 'InAnteVestibulum.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (34, 'Document 23594', null, '05/08/2015', 'Neque.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (35, 'Document 20119', null, '11/10/2015', 'EgetSemper.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (36, 'Document 64440', 'Unterschrieben', '03/19/2015', 'Tincidunt.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (37, 'Document 12823', 'In Ablage', '03/27/2014', 'AuctorGravidaSem.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (38, 'Document 39315', 'In Ablage', '07/15/2016', 'EgetSemperRutrum.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (39, 'Document 90542', 'Unterschrieben', '09/09/2013', 'AtVelitVivamus.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (40, 'Document 50464', 'In Ablage', '02/23/2016', 'DignissimVestibulumVestibulum.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (41, 'Document 21852', 'Unterschrieben', '05/26/2013', 'SapienCursusVestibulum.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (42, 'Document 93944', 'Unterschrieben', '03/30/2017', 'VelNulla.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (43, 'Document 28773', 'In Ablage', '05/05/2016', 'MorbiNon.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (44, 'Document 05236', null, '03/05/2016', 'DuiProin.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (45, 'Document 14714', 'In Ablage', '01/05/2014', 'PellentesqueUltrices.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (46, 'Document 41417', 'Nicht physisch vorhanden', '08/01/2015', 'NonInterdumIn.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (47, 'Document 46118', null, '04/22/2017', 'AdipiscingLorem.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (48, 'Document 55218', 'Nicht physisch vorhanden', '05/22/2016', 'FaucibusOrciLuctus.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (49, 'Document 42006', 'Unterschrieben', '10/12/2014', 'Ut.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (50, 'Document 15961', null, '04/23/2014', 'Nulla.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (51, 'Document 99106', 'In Ablage', '05/07/2014', 'LuctusEt.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (52, 'Document 49837', 'Unterschrieben', '10/08/2015', 'LaciniaEratVestibulum.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (53, 'Document 70386', 'Nicht physisch vorhanden', '01/21/2016', 'VulputateElementumNullam.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (54, 'Document 34705', null, '07/03/2014', 'Mauris.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (55, 'Document 66095', 'In Ablage', '02/16/2015', 'PhasellusSit.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (56, 'Document 60735', 'Beim Kunden zur Unterschrift', '07/25/2014', 'InHacHabitasse.doc');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (57, 'Document 63583', null, '07/13/2015', 'Turpis.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (58, 'Document 51882', null, '11/05/2016', 'Rhoncus.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (59, 'Document 76936', 'In Ablage', '01/07/2015', 'AugueLuctus.doc');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (60, 'Document 49926', null, '04/19/2013', 'UrnaUtTellus.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (61, 'Document 48166', 'In Ablage', '12/01/2013', 'EgetVulputateUt.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (62, 'Document 27296', 'In Ablage', '08/05/2015', 'InFaucibusOrci.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (63, 'Document 27408', 'Nicht physisch vorhanden', '06/17/2016', 'BibendumMorbiNon.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (64, 'Document 57540', 'In Ablage', '07/05/2015', 'Purus.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (65, 'Document 65529', null, '07/07/2015', 'NullaTempusVivamus.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (66, 'Document 83674', 'In Ablage', '03/28/2014', 'BlanditUltricesEnim.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (67, 'Document 72084', 'In Ablage', '07/30/2015', 'EnimBlandit.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (68, 'Document 63806', 'In Ablage', '05/31/2013', 'Viverra.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (69, 'Document 82288', 'In Ablage', '03/13/2016', 'ConsequatLectus.xls');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (70, 'Document 79361', 'Nicht physisch vorhanden', '09/18/2015', 'JustoEtiamPretium.doc');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (71, 'Document 91339', 'In Ablage', '02/14/2015', 'Metus.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (72, 'Document 66190', 'In Ablage', '11/02/2014', 'Pretium.doc');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (73, 'Document 49506', 'Nicht physisch vorhanden', '07/18/2016', 'EstRisus.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (74, 'Document 55176', null, '07/14/2015', 'Porttitor.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (75, 'Document 45667', null, '04/18/2014', 'APede.ppt');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (76, 'Offerte Server', null, '05/20/2017', 'Offer_AZ0.docx');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (77, 'Offerte Server 2', null, '05/22/2017', 'Offer_XCV.docx');
insert into dbo.Documents (Id, Name, Tag, IssueDate, RelativePath) values (78, 'Auftragsbestätigung S2', null, '05/22/2017', 'OrderConfirmation_EFS.docx');
SET IDENTITY_INSERT dbo.Documents OFF
GO

SET IDENTITY_INSERT dbo.Offers ON
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (1, 'ON-94008', 15, 2);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (2, 'ON-40081', 15, 56);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (3, 'ON-86911', 15, 23);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (4, 'ON-91055', 4, 39);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (5, 'ON-30878', 5, 50);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (6, 'ON-66088', 14, 31);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (7, 'ON-70335', 10, 59);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (8, 'ON-15261', 8, 53);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (9, 'ON-63866', 13, 4);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (10, 'ON-75978', 7, 20);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (11, 'ON-41314', 5, 28);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (12, 'ON-12343', 8, 38);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (13, 'ON-63666', 11, 59);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (14, 'ON-90818', 13, 57);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (15, 'ON-83785', 15, 39);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (16, 'ON-29523', 10, 39);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (17, 'ON-81074', 2, 21);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (18, 'ON-14298', 14, 16);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (19, 'ON-42880', 2, 20);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (20, 'ON-93907', 12, 58);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (21, 'ON-13776', 12, 20);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (22, 'ON-11087', 13, 3);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (23, 'ON-04555', 3, 43);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (24, 'ON-31584', 8, 9);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (25, 'ON-45336', 7, 18);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (26, 'ON-81372', 7, 58);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (27, 'ON-80874', 3, 35);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (28, 'ON-13030', 13, 17);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (29, 'ON-07176', 3, 25);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (30, 'ON-38186', 5, 21);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (31, 'ON-38157', 16, 76);
insert into dbo.Offers (Id, OfferNumber, OrderId, DocumentId) values (32, 'ON-84757', 16, 77);
SET IDENTITY_INSERT dbo.Offers OFF
GO

insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (1, 'OCN-51057', 39);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (2, 'OCN-06947', 17);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (3, 'OCN-84303', 16);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (4, 'OCN-11518', 18);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (5, 'OCN-22875', 5);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (6, 'OCN-67970', 3);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (7, 'OCN-66527', 47);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (8, 'OCN-30051', 6);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (9, 'OCN-61544', 42);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (10, 'OCN-09924', 38);
insert into dbo.OrderConfirmations (OrderId, OrderConfNumber, DocumentId) values (16, 'OCN-07234', 78);
GO

SET IDENTITY_INSERT dbo.Invoices ON
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (1, 'IN-23415', 'true', 449.2, 3, 61);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (2, 'IN-39617', 'false', 612.0, 13, 62);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (3, 'IN-30460', 'false', 460.7, 11, 63);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (4, 'IN-86719', 'true', 611.8, 2, 64);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (5, 'IN-31315', 'true', 653.7, 10, 65);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (6, 'IN-86570', 'true', 873.5, 15, 66);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (7, 'IN-52153', 'false', 824.6, 2, 67);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (8, 'IN-49576', 'true', 256.9, 3, 68);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (9, 'IN-06643', 'false', 854.6, 7, 69);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (10, 'IN-17174', 'false', 724.5, 4, 70);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (11, 'IN-27457', 'true', 445.6, 10, 71);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (12, 'IN-55195', 'true', 753.5, 6, 72);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (13, 'IN-08986', 'false', 662.6, 11, 73);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (14, 'IN-95305', 'true', 583.2, 11, 74);
insert into dbo.Invoices (Id, InvoiceNumber, IsPaid, Amount, OrderId, DocumentId) values (15, 'IN-37064', 'true', 662.6, 12, 75);
SET IDENTITY_INSERT dbo.Invoices OFF
GO

SET IDENTITY_INSERT dbo.Transactions ON
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (81, 'Rechnung zu IN-23415', 449.2, 'true', 0, '12/01/2013', 61, 2, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (82, 'Rechnung zu IN-86719', 611.8, 'true', 0, '07/05/2015', 64, 4, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (83, 'Rechnung zu IN-31315', 653.7, 'true', 0, '07/07/2015', 65, 4, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (84, 'Rechnung zu IN-86570', 873.5, 'true', 0, '03/28/2014', 66, 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (85, 'Rechnung zu IN-49576', 256.9, 'true', 0, '05/31/2013', 68, 2, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (86, 'Rechnung zu IN-27457', 445.6, 'true', 0, '02/14/2015', 71, 4, 6);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (87, 'Rechnung zu IN-55195', 753.5, 'true', 0, '11/02/2014', 72, 3, 25);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (88, 'Rechnung zu IN-95305', 583.2, 'true', 0, '07/14/2015', 74, 4, 26);
insert into dbo.Transactions (Id, Name, Amount, IsRevenue, PrivatePart, Date, DocumentId, BudgetId, CategoryId) values (89, 'Rechnung zu IN-37064', 662.6, 'true', 0, '04/18/2014', 75, 3, 6);
SET IDENTITY_INSERT dbo.Transactions OFF
GO
