use test_db;

select * from parenttostudent

delete from parenttostudent where ParentToStudentID = 1

select * from parentaccount
select * from adminaccount
insert into adminaccount(AdminID, UserID, LastAccess, LastAccess2) values(1, 1, null, Now())

select * from studentforms;
insert into forms(formid, formname, category)values(1, 'Follow Directions', 'Behavior');
insert into forms(formid, formname, category)values(2, 'Completing Assignments', 'Behavior');
insert into forms(formid, formname, category)values(3, 'Arguing/Talking Back', 'Behavior');
insert into forms(formid, formname, category)values(4, 'Talking Out of Turn', 'Behavior');
insert into forms(formid, formname, category)values(5, 'Inattentiveness/Lack of Participation', 'Behavior');
insert into forms(formid, formname, category)values(6, 'Custom Behavior Tracking', 'Behavior');
insert into forms(formid, formname, category)values(7, 'Incident Form', 'Incident');
insert into forms(formid, formname, category)values(8, 'Intervention Form', 'Intervention');
insert into forms(formid, formname, category)values(9, 'Progress Report Form', 'Behavior');
insert into forms(formid, formname, category)values(10, 'Home Tracking Form', 'Home');

select * from forms;
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/4/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/5/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/6/2017',null,0,'',null,7);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/7/2017',null,0,'',null,2);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/8/2017',null,0,'',null,8);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/9/2017',null,0,'',null,3);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/10/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/11/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,1,'Follow Directions','10/12/2017',null,0,'',null,2);

insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/4/2017',null,0,'',null,8);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/5/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/6/2017',null,0,'',null,4);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/7/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/8/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/9/2017',null,0,'',null,3);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/10/2017',null,0,'',null,3);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/11/2017',null,0,'',null,2);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,2,'Completing Assignments','10/12/2017',null,0,'',null,4);

insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/4/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/5/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/6/2017',null,0,'',null,7);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/7/2017',null,0,'',null,2);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/8/2017',null,0,'',null,8);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/9/2017',null,0,'',null,3);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/10/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/11/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,6,'Random Crying Outbursts','10/12/2017',null,0,'',null,2);

insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/4/2017',null,0,'',null,7);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/5/2017',null,0,'',null,8);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/6/2017',null,0,'',null,8);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/7/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/8/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/9/2017',null,0,'',null,7);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/10/2017',null,0,'',null,9);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/11/2017',null,0,'',null,10);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,3,'Arguing/Talking Back','10/12/2017',null,0,'',null,7);

insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/4/2017',null,0,'',null,7);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/5/2017',null,0,'',null,7);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/6/2017',null,0,'',null,8);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/7/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/8/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/9/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/10/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/11/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,4,'Talking Out of Turn','10/12/2017',null,0,'',null,6);

insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/4/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/5/2017',null,0,'',null,6);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/6/2017',null,0,'',null,7);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/7/2017',null,0,'',null,2);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/8/2017',null,0,'',null,8);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/9/2017',null,0,'',null,3);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/10/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/11/2017',null,0,'',null,5);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,5,'Inattentiveness/Lack of Participation','10/12/2017',null,0,'',null,2);

insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/4/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/4/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/7/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/7/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/7/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/9/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/10/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/12/2017',null,0,'',null,0);
insert into studentforms(studentid, formid, formname, formdate, enddate, shared, formdata, description, behaviorrating) values(4,7,'Incident Form','10/12/2017',null,0,'',null,0);
