<Query Kind="SQL">
  <Connection>
    <ID>ffbe536b-021f-4ef6-a377-d2c5feefa283</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>SummerOfNHibernate</Database>
  </Connection>
</Query>

Customers.Take (100)

insert into customer 

INSERT INTO customer (Firstname,Lastname)
VALUES ('Juan', 'Huerta');

select * from customer where customerid = 3
select * from [order] where customer = 3
delete from customer where Customerid = 2





INSERT INTO [Order] (OrderDate,Customer) VALUES ('1/10/1950', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('1/10/1955', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('1/10/1958', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/15/1960', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/15/1962', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/15/1964', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/15/1966', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/20/1968', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/20/1970', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('3/20/1972', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('3/25/1974', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('3/25/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('3/25/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('4/27/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('4/27/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('4/27/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('4/27/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('8/28/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('8/28/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('8/28/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('8/5/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('10/5/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('10/5/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('10/3/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('11/3/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('11/3/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('11/3/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('12/2/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('12/3/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('12/4/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('12/4/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/1/1950', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('3/1/1955', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('4/2/1958', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('5/2/1960', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('6/3/1962', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('7/3/1964', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('8/4/1966', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('9/4/1968', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('10/5/1970', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('11/5/1972', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('12/6/1974', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('11/5/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('10/4/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('9/5/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('8/6/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('7/5/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('6/6/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('5/6/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('4/7/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('3/7/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/7/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('1/7/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('9/8/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('8/8/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('7/9/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('6/9/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('5/10/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('4/10/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('3/20/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('2/20/1976', 2);
INSERT INTO [Order] (OrderDate,Customer) VALUES ('1/29/1976', 2);

select * from customer


mpList.GroupBy(employees => new String(employees.FName[0], 1))
Customers.GroupBy(s => s.Firstname).Select(s => new {Value = s.Count(), Key = s.Key}).OrderBy(s => s.Value)