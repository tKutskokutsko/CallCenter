 CallCenter api created based on .NET and VueJS

 For backend part in CallCenterController was created andpoint to determine the agent’s state.
 Also logic was added to create agent information and save it to DB, if it's not exist. EF Core was used to update a PostgreSQL database (setting the agent’s state based on the calculated value and update the agent’s skills to match the JSON “queueIds” property). 
 To test backend part swagger can be used. 

 As a database, two options was added :  PostgreSQL and MongoDb

 Frondend part shows sortable table with agents information.

 Also a project with some unit tests was added. NUnit.Framework, FakeItEasy, FluentAssertions, Bogus was used.
