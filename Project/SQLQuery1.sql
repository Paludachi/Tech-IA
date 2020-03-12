SELECT Students.FirstName, Students.FamilyName, Items.ObjectName, Items.DateDue 
FROM Students
Inner Join Items
ON Items.ObjectID = Students.ObjectID