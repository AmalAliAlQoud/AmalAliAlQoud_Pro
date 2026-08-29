*********AMALALI Alkaoud*****************************


*********CREATE DB AND CREATE TABLES*********************************************************

create database FirstDatabase;

create table Departments(
DepartmentID int,
DepartmentName nvarchar(50) not null,

constraint Departments_DepartmentID_PK primary key(DepartmentID),
constraint Departments_DepartmentName_UQ unique(DepartmentName),
);


create table Employees(
EmployeeID int,
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null,
DepartmentID int,
Salary decimal(10,2),

constraint Employees_EmployeeID_PK primary key(EmployeeID),
constraint Employees_DepartmentID_FK foreign key(DepartmentID) REFERENCES Departments(DepartmentID),
constraint Employees_Salary_CK check(Salary>=0),
);


alter table Employees 
add ManagerID int,
constraint Employees_ManagerID_FK foreign key(ManagerID) REFERENCES Employees(EmployeeID);


create table LeaveRequests(
LeaveID int,
EmployeeID int,
StartDate date not null,
EndDate date not null,
Reason nvarchar(100),
Status  nvarchar(20),

constraint LeaveRequests_LeaveID_PK primary key(LeaveID),
constraint LeaveRequests_EmployeeID_FK foreign key(EmployeeID) REFERENCES  Employees(EmployeeID),
constraint LeaveRequests_Status_CK check(Status in('Pending','Approved','Rejected')),
);


create table Instructors(
InstructorID int,
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null,

constraint Instructors_InstructorID_PK primary key(InstructorID),
);

create table TrainingCourses(
CourseID int,
CourseName nvarchar(100) not null,
StartDate date not null,
EndDate date not null,
InstructorID int,

constraint TrainingCourses_CourseID_PK primary key(CourseID),
constraint TrainingCourses_InstructorID_FK foreign key(InstructorID) references Instructors(InstructorID),
);


*********Insert Data into TABLES*********************************************************

insert into Departments values
(1,'HR'),
(2,'Finance'),
(3,'IT'),
(4,'Marketing'),
(5,'Operations'),
(6,'Sales'),
(7,'Customer Service'),
(9,'Research and Development');



insert into Employees values
(1 ,'John',' Doe', 1, 50000.00,1),
(2,'Jane','Smith',2,60000.00,2),
(3,'Ahmad', 'Ali', 3 ,55000.00,3),
(4,'Sara', 'Khan', 2 ,52000.00,2),
(5,'Mohamed', 'Hassan', 1 ,48000.00,1),
(6,'Hassan', 'Ali', 1 ,54000.00,1),
(7,'Fatima', 'Khaled', 2 ,62000.00,2),
(8,'Amr', 'Mohamed', 3 ,56000.00,3),
(9,'Sara', 'Ahmed', 4 ,51000.00,4),
(10,'Ahmed', 'Youssef', 1 ,59000.00,1);


insert into Instructors values
(1,'Sarah','Johnson'),
(2,'James','Smith'),
(3,'Mary','Davis'),
(4,'Ahmed','Hassan'),
(5,'Fatima','Ali'),
(6,'Yasmine','Ali'),
(7,'Mohamed','Hassan'),
(8,'Ahmed','Youssef'),
(9,'Amr','Mohamed'),
(10,'Sara','Ahmed');


insert into TrainingCourses values
(201, 'Leadership Skills', '2023-11-05', '2023-11-07', 1),
(202, 'Excel Advanced', '2023-11-12', '2023-11-14', 2),
(203, 'Time Management', '2023-11-20', '2023-11-21', 3),
(204, 'Communication Tips', '2023-12-02', '2023-12-04', 4),
(205, 'Conflict Resolution', '2023-12-10', '2023-12-11', 5),
(206, 'Negotiation Skills', '2023-12-20', '2023-12-22', 6),
(207, 'Financial Planning', '2024-01-05', '2024-01-07', 7),
(208, 'Leadership Workshop', '2024-01-10', '2024-01-12', 8),
(209, 'Effective Communication', '2024-01-15', '2024-01-17', 9),
(210, 'Time Management', '2024-01-20', '2024-01-22', 10);

insert into LeaveRequests values
(101,1,'2023-10-10','2023-10-14','Annual Leave','Approved'),
(102,3,'2023-11-05','2023-11-07','Conference','Pending'),
(103,2,'2023-10-20','2023-10-21','Sick Leave','Approved'),
(104,4,'2023-12-01','2023-12-03','Family Emergency','Pending'),
(105,5,'2023-10-25','2023-10-26','Personal','Approved');

*********Update and Delete*********************************************************

**[1]**

update Employees set Salary=52000.00 where EmployeeID=5;

**[2]**

update Departments set DepartmentName='Human Resources' where DepartmentName='HR';

**[3]**

update Employees set ManagerID=10 where DepartmentID=3;

**[4]**

update TrainingCourses set StartDate='2023-11-13' where CourseID=202;

**[5]**

delete from Employees where EmployeeID=8;

**[6]**

delete from Departments where DepartmentID=7;

**[7]**

delete from TrainingCourses where CourseID=205;


*********simple Query*********************************************************

**[1]**

select FirstName,Salary from Employees;

**[2]*********************************************************************

select cast(avg(Salary) as decimal(10,2)) 'salary average'  from Employees;

**[1]**

select FirstName+' '+LastName 'name',Salary from Employees;

**[2]*********************************************************************

select departmentName from Departments;

**[3]*********************************************************************

select CourseName,StartDate from TrainingCourses;

*********Complex Query*********************************************************

**[1]*********************************************************************

select departmentName,count(*) '# of enp',cast (avg(Salary) as decimal(10,2)) 'salary avg'
from Departments d,Employees e
where d.DepartmentID=e.DepartmentID
group by departmentName ;

**[2]*********************************************************************

select FirstName,CourseName
from Instructors I,TrainingCourses T
where I.InstructorID=T.InstructorID;

**[3]*********************************************************************

select departmentName,count(*) '# of enp',cast (avg(Salary) as decimal(10,2)) 'salary avg'
from Departments d,Employees e
where d.DepartmentID=e.DepartmentID
group by departmentName ;

**[4]*********************************************************************

select FirstName,
sum( case when status='Approved' then 1   else 0 end) '# of approved',
sum( case when status='Rejected' then 1   else 0 end) '# of not Rejected'
from Employees e,LeaveRequests L
where e.EmployeeID=L.EmployeeID
group by FirstName;

**[5]*********************************************************************

select departmentName,count(*) '# of enp'
from Departments d,Employees e
where d.DepartmentID=e.DepartmentID
group by departmentName ;

**[6]*********************************************************************


select FirstName+' '+LastName name,count(DISTINCT  T.InstructorID) '# of Course'
from Instructors I,TrainingCourses T
where T.InstructorID =I.InstructorID
group by T.InstructorID,FirstName,LastName;

**[7]*********************************************************************

select FirstName,departmentName
from Departments d,Employees e
where d.DepartmentID=e.DepartmentID;

**[8]*********************************************************************

select FirstName,DepartmentName
from Employees e, Departments d
where d.DepartmentID=e.DepartmentID
and e.ManagerID in(
select e.ManagerID
from Employees e
group by e.ManagerID
having count(*)>3
)
group by  FirstName,departmentName;

*[*[9]*********************************************************************

select FirstName,DepartmentName
from Employees e, Departments d
where e.DepartmentID=d.DepartmentID
and e.ManagerID in(
select e.ManagerID
from Employees e
group by e.ManagerID
having count(*)>1
)
group by  FirstName,departmentName;

**[10]*********************************************************************

select  CourseName,T.InstructorID,I.FirstName
from TrainingCourses T,Instructors I
where T.InstructorID=I.InstructorID
and FirstName='Sarah' and LastName='Johnson';

**[11]*********************************************************************

select * 
from Employees
where ManagerID=null;

**[12]*********************************************************************

select departmentName,cast (avg(Salary) as decimal(10,2)) 'salary avg'
from Departments d,Employees e
where d.DepartmentID=e.DepartmentID
group by departmentName ;

**[13]*********************************************************************

select departmentName,FirstName,max(Salary) 'salary'
from Departments d,Employees e
where d.DepartmentID=e.DepartmentID
and  Salary in(
		select  max(Salary)
		from Employees
		group by ManagerID
			)
group by departmentName,FirstName ;



**[14]*********************************************************************

select count(*) '# of column after 2023-12-01'
from TrainingCourses
where StartDate>'2023-12-01';


**[15]*********************************************************************

select departmentName,FirstName,max(Salary) 'salary'
from Departments d,Employees e
where d.DepartmentID=e.DepartmentID
and  Salary in(
		select  max(Salary)
		from Employees
		group by ManagerID
			)
group by departmentName,FirstName ;


****************THE END******************************************************

select * from Departments;
select * from Employees;
select * from TrainingCourses;
select * from Instructors;













select * from Employees;