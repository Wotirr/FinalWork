-- Книжный мир. Схема и данные базы данных для MS SQL Server.
-- Скрипт создаёт все таблицы и наполняет их данными в текущей (уже существующей) базе данных.
-- Перед запуском выберите нужную базу данных в SSMS.

IF OBJECT_ID(N'OrderItem', N'U') IS NOT NULL DROP TABLE OrderItem;
IF OBJECT_ID(N'Orders', N'U') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID(N'Product', N'U') IS NOT NULL DROP TABLE Product;
IF OBJECT_ID(N'Users', N'U') IS NOT NULL DROP TABLE Users;
IF OBJECT_ID(N'OrderStatus', N'U') IS NOT NULL DROP TABLE OrderStatus;
IF OBJECT_ID(N'Category', N'U') IS NOT NULL DROP TABLE Category;
IF OBJECT_ID(N'Manufacturer', N'U') IS NOT NULL DROP TABLE Manufacturer;
IF OBJECT_ID(N'Unit', N'U') IS NOT NULL DROP TABLE Unit;
IF OBJECT_ID(N'Role', N'U') IS NOT NULL DROP TABLE Role;
GO

CREATE TABLE Role (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
CREATE TABLE Unit (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);
CREATE TABLE Manufacturer (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL
);
CREATE TABLE Category (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL
);
CREATE TABLE OrderStatus (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);
CREATE TABLE Product (
    Article NVARCHAR(20) PRIMARY KEY,
    Name NVARCHAR(300) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Author NVARCHAR(200) NULL,
    Price DECIMAL(10,2) NOT NULL,
    Discount INT NOT NULL DEFAULT 0,
    StockQuantity INT NOT NULL DEFAULT 0,
    Photo NVARCHAR(100) NULL,
    UnitId INT NOT NULL REFERENCES Unit(Id),
    CategoryId INT NOT NULL REFERENCES Category(Id),
    ManufacturerId INT NOT NULL REFERENCES Manufacturer(Id)
);
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    Login NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    RoleId INT NOT NULL REFERENCES Role(Id)
);
CREATE TABLE Orders (
    Number INT PRIMARY KEY,
    OrderDate DATE NOT NULL,
    DeliveryDate DATE NULL,
    PickupCode NVARCHAR(3) NOT NULL,
    StatusId INT NOT NULL REFERENCES OrderStatus(Id),
    UserId INT NULL REFERENCES Users(Id)
);
CREATE TABLE OrderItem (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderNumber INT NOT NULL REFERENCES Orders(Number),
    ProductArticle NVARCHAR(20) NOT NULL REFERENCES Product(Article),
    Quantity INT NOT NULL
);
GO

-- Role
SET IDENTITY_INSERT Role ON;
INSERT INTO Role (Id, Name) VALUES (1, N'Администратор');
INSERT INTO Role (Id, Name) VALUES (2, N'Менеджер');
INSERT INTO Role (Id, Name) VALUES (3, N'Авторизированный клиент');
SET IDENTITY_INSERT Role OFF;
GO

-- Unit
SET IDENTITY_INSERT Unit ON;
INSERT INTO Unit (Id, Name) VALUES (1, N'шт.');
SET IDENTITY_INSERT Unit OFF;
GO

-- Manufacturer
SET IDENTITY_INSERT Manufacturer ON;
INSERT INTO Manufacturer (Id, Name) VALUES (1, N'Яуза');
INSERT INTO Manufacturer (Id, Name) VALUES (2, N'Т8 Издательские технологии');
INSERT INTO Manufacturer (Id, Name) VALUES (3, N'Прогресс книга');
INSERT INTO Manufacturer (Id, Name) VALUES (4, N'Время');
INSERT INTO Manufacturer (Id, Name) VALUES (5, N'Лениздат');
INSERT INTO Manufacturer (Id, Name) VALUES (6, N'Неолит');
INSERT INTO Manufacturer (Id, Name) VALUES (7, N'Амрита-Русь');
INSERT INTO Manufacturer (Id, Name) VALUES (8, N'Златоуст');
INSERT INTO Manufacturer (Id, Name) VALUES (9, N'Аспект Пресс');
INSERT INTO Manufacturer (Id, Name) VALUES (10, N'ВКН');
SET IDENTITY_INSERT Manufacturer OFF;
GO

-- Category
SET IDENTITY_INSERT Category ON;
INSERT INTO Category (Id, Name) VALUES (1, N'Художественная литература');
INSERT INTO Category (Id, Name) VALUES (2, N'Учебник для вузов');
INSERT INTO Category (Id, Name) VALUES (3, N'Хрестоматия');
INSERT INTO Category (Id, Name) VALUES (4, N'Учебное пособие');
SET IDENTITY_INSERT Category OFF;
GO

-- OrderStatus
SET IDENTITY_INSERT OrderStatus ON;
INSERT INTO OrderStatus (Id, Name) VALUES (1, N'Новый');
INSERT INTO OrderStatus (Id, Name) VALUES (2, N'В обработке');
INSERT INTO OrderStatus (Id, Name) VALUES (3, N'Завершен');
SET IDENTITY_INSERT OrderStatus OFF;
GO

-- Товары
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'А112Т4', N'Прокляты и убиты', N'Роман-эпопею "Прокляты и убиты" Виктора Астафьева по праву считают одним из самых сильных и пронзительных произведений отечественной военной прозы.', N'Виктор Астафьев', 585.00, 25, 6, N'1.jpg', 1, 1, 1);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'G843H5', N'Тайны и загадки отца БраунаТайны и загадки отца Брауна', N'Гилберт Кит Честертон — признанный классик английской литературы, один из самых ярких писателей первой половины XX века. Классикой стали его романы и многочисленные эссе, однако любовь массового читателя принесли ему рассказы об отце Брауне, тихом, застенчивом священнике, мастерски раскрывающем наиболее запутанные загадки и преступления.', N'Гилберт Кит Честертон', 193.00, 30, 9, N'2.jpg', 1, 1, 1);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'D325D4', N'Девайс', N'Молодой фрилансер Захар Скаро устраивается на очередную подработку. Задача, казалось бы, тривиальная: тестирование нового устройства. Вот только вопрос в том, тестированием какой реальности занимается этот новый Девайс?', N'Кирилл Каланджи', 1599.00, 5, 12, N'3.jpg', 1, 1, 2);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'S432T5', N'Необыкновенное обыкновенное чудо. Школьные истории', N'', N'Людмила Улицкая', 549.00, 15, 15, N'4.jpg', 1, 1, 2);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'F325D4', N'Чук и Гек', N'В книгу вошли повести и рассказы Аркадия Петровича Гайдара: "Чук и Гек", "Горячий камень" и "Сказка о военной тайне, о Мальчише-Кибальчише и его твердом слове"', N'Аркадий Гайдар', 209.00, 18, 3, N'5.jpg', 1, 1, 2);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'G432G6', N'Информационная безопасность. Национальные стандарты Российской Федерации. 3-е издание. Учебное пособие', N'В учебном пособии рассмотрено более 300 действующих открытых документов национальной системы стандартизации Российской Федерации, включая международные и межгосударственные стандарты в области информационной безопасности по состоянию на начало 2023 года.', N'Юрий Родичев', 3899.00, 22, 3, N'6.jpg', 1, 2, 3);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'H542F5', N'Linux. Командная строка. Лучшие практики', N'Перейдите на новый уровень работы в Linux! Если вы системный администратор, разработчик программного обеспечения, SRE-инженер или пользователь Linux, книга поможет вам работать быстрее, элегантнее и эффективнее.', N'Дэниел Джей Барретт', 1799.00, 4, 5, N'7.jpg', 1, 2, 3);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'C346F5', N'Квантовые миры и возникновение пространства-времени', N'Шон Кэрролл — физик-теоретик и один из самых известных в мире популяризаторов науки — заставляет нас по-новому взглянуть на физику. Столкновение с главной загадкой квантовой механики полностью поменяет наши представления о пространстве и времени.', N'Шон Кэрролл', 1349.00, 5, 4, N'8.jpg', 1, 2, 3);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'F256G6', N'Вселенная. Происхождение жизни, смысл нашего существования и огромный космос', N'Знаменитый физик Шон Кэрролл в свойственной ему увлекательной манере объясняет принципы, которые лежат в основах научных революций от Дарвина до Эйнштейна, и показывает как невероятные научные открытия последнего столетия изменили наш мир.', N'Шон Кэрролл', 1799.00, 6, 2, N'', 1, 2, 3);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'J532V5', N'Пушкин. Бродский. Империя и судьба. В 2-х томах (комплект из 2-х книг)', N'Первая книга двухтомника «Пушкин. Бродский. Империя и судьба» пронизана пушкинской темой. Пушкин — «певец империи и свободы» — присутствует даже там, где он впрямую не упоминается, ибо его судьба, как и судьба других героев книги, органично связана с трагедией великой империи.', N'Яков Гордин', 529.00, 8, 6, N'10.jpg', 1, 3, 4);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'G643F4', N'Иосиф Бродский. Избранные эссе (комплект из 6-ти книг)', N'Шесть сборников избранных эссе Иосифа Бродского (1940-1996), великого поэта, драматурга, мыслителя, лауреата Нобелевской премии по литературе (1987): «Будущее или далекое прошлое», «Верь своей боли», «Как читать книгу», «О русской литературе», «О тирании», «Путеводитель по переименованному городу». Все тексты представлены на английском языке и в переводе на русский и открывают автора не только как поэта, но как историка, критика, и глубокого и ироничного мыслителя.', N'Иосиф Бродский', 4925.00, 2, 24, N'11.jpg', 1, 3, 5);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'J326V5', N'Тысячелетие императорской керамикиv', N'Фарфор стал величайшим символом китайской культуры. Это одно из выдающихся изобретений, внесших неоценимый вклад в мировую цивилизацию.', N'Янь Чуннянь Янь Чуннянь', 2599.00, 5, 4, N'12.jpg', 1, 3, 5);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'J632F6', N'Вечные спутники: Портреты из всемирной литературы', N'Книга "Вечные спутники" - это цикл критических очерков о культуре и великих литераторах, сопровождавших жизнь и творчество русского писателя, поэта, литературного критика и общественного деятеля Дмитрия Мережковского (1865–1941).', N'Дмитрий Мережковский', 1599.00, 0, 6, N'13.jpg', 1, 3, 5);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'G632H6', N'Формирование литературной репутации Н.Г.Чернышевского в ХIX-XXI веках', N'Монография Д. А. Щербакова - новаторская. Поэтапно рассмотрены не только многочисленные суждения известных отечественных и зарубежных критиков, литературоведов, философов и политиков, различным образом характеризовавших Н. Г. Чернышевского в связи и вне связи со знаменитым романом "Что делать?', N'Дмитрий Щербаков', 1349.00, 2, 8, N'14.jpg', 1, 3, 6);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'M642E5', N'Теория искусства. Краткий путеводитель', N'', N'Роджер Осборн, Дэн Стерджис', 879.00, 3, 2, N'15.jpg', 1, 3, 6);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'G543F5', N'Религиозные верования с древнейших времен до наших дней', N'Настоящее издание представляет собой сборник переводов лекций по истории дохристианских и нехристианских религий, прочитанных в Лондоне в период с 1888 по 1891 гг. авторитетными исследователями данного раздела религиоведения.', N'Дмитрий Щербаков', 879.00, 4, 6, N'16.jpg', 1, 3, 7);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'B653G6', N'Русский язык: Первые шаги. Часть 3. Учебное пособие', N'Пособие является завершающей частью учебного комплекса. Третья часть содержит 10 уроков (21-30, последний-повторительный). Усвоение лексико-грамматического материала рассчитано примерно на 200-240 часов аудиторных занятий.', N'Любовь Беликова, Инна Ерофеева, Татьяна Шутова', 2699.00, 8, 9, N'17.jpg', 1, 4, 8);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'J735J7', N'Синтетический образ индивидуального психического мира', N'Психика подобна определенным объектам, это фиксируют сами люди в языке и искусстве. В данном исследовании рассматриваются в этом плане образы сосуда, воронки, дерева и крепости.', N'Сергей Моргачев', 1099.00, 9, 4, N'18.jpg', 1, 3, 8);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'H436H7', N'Английский язык в спорте: Учебное пособие', N'Учебное пособие подготовлено для слушателей, изу чающих английский язык как язык специальности', N'Екатерина Габарта, Ирина Игнатьева', 1999.00, 2, 0, N'19.jpg', 1, 4, 9);
INSERT INTO Product (Article, Name, Description, Author, Price, Discount, StockQuantity, Photo, UnitId, CategoryId, ManufacturerId) VALUES (N'H475R5', N'Лексика и грамматика современного китайского языка (к тому II учебника «Новый практический курс китайского языка» под редакцией Лю Сюня): учебное пособие', N'Пособие выступает дополнением ко второму тому учебника «Новый практический курс китайского языка» (под редакцией Лю Сюня).', N'Татьяна Лопаткина, Софья Маннапова', 608.00, 25, 12, N'20.jpg', 1, 4, 10);
GO

-- Пользователи
SET IDENTITY_INSERT Users ON;
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (1, N'Никифорова Анна Семеновна', N'94d5ous@gmail.com', N'uzWC67', 1);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (2, N'Стелина Евгения Петровна', N'uth4iz@mail.com', N'2L6KZG', 1);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (3, N'Михайлюк Анна Вячеславовна', N'5d4zbu@tutanota.com', N'rwVDh9', 1);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (4, N'Ситдикова Елена Анатольевна', N'ptec8ym@yahoo.com', N'LdNyos', 2);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (5, N'Ворсин Петр Евгеньевич', N'1qz4kw@mail.com', N'gynQMT', 2);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (6, N'Старикова Елена Павловна', N'4np6se@mail.com', N'AtnDjr', 2);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (7, N'Никифорова Весения Николаевна', N'yzls62@outlook.com', N'JlFRCZ', 3);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (8, N'Сазонов Руслан Германович', N'1diph5e@tutanota.com', N'8ntwUp', 3);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (9, N'Одинцов Серафим Артёмович', N'tjde7c@yahoo.com', N'YOyhfR', 3);
INSERT INTO Users (Id, FullName, Login, Password, RoleId) VALUES (10, N'Степанов Михаил Артёмович', N'wpmrc3do@tutanota.com', N'RSbvHv', 3);
SET IDENTITY_INSERT Users OFF;
GO

-- Заказы
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (1, '2025-02-27', '2025-04-20', N'901', 3, 10);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (2, '2025-03-28', '2025-04-21', N'789', 2, 7);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (3, '2026-02-20', '2026-04-22', N'852', 3, NULL);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (4, '2026-03-01', '2026-04-23', N'458', 2, NULL);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (5, '2026-03-17', '2026-04-24', N'905', 3, NULL);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (6, '2026-03-21', '2026-04-25', N'781', 3, 7);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (7, '2026-03-31', '2026-04-26', N'128', 3, NULL);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (8, '2026-04-02', '2026-04-27', N'908', 1, 9);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (9, '2026-04-03', '2026-04-28', N'719', 1, NULL);
INSERT INTO Orders (Number, OrderDate, DeliveryDate, PickupCode, StatusId, UserId) VALUES (10, '2026-05-30', '2026-04-29', N'910', 1, 10);
GO

-- Позиции заказов
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (1, N'А112Т4', 2);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (2, N'G843H5', 1);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (2, N'А112Т4', 1);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (3, N'D325D4', 10);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (4, N'F325D4', 5);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (4, N'D325D4', 4);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (5, N'G432G6', 20);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (6, N'А112Т4', 2);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (6, N'G843H5', 2);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (7, N'C346F5', 3);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (7, N'F256G6', 3);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (8, N'F325D4', 1);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (8, N'G432G6', 1);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (8, N'H542F5', 20);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (9, N'J532V5', 5);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (9, N'F256G6', 1);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (10, N'F256G6', 5);
INSERT INTO OrderItem (OrderNumber, ProductArticle, Quantity) VALUES (10, N'J532V5', 5);
GO
