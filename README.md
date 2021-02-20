# Eurofficetest
## 1. How long did you spend on the coding test?

About 3 hours and a half BE and FE included.  
## 2. What would you change / implement in your application given more time?
I would definetly spend more time on the front end to improve the user experience, it is quite a simple and straigthforward UI.  
On the Backend side, I would implement more unit tests and integration tests. The logging could also be improved as well as the genericity of few bits of code.  
## 3. Did you use IoC? Please explain your reasons either way.

Yes, I did. IoC has multpile benefits, the 2 main ones would be:  
  - making the code less couple so it does not depend on a specific implementation but rather on a functionality provided by an interface (contract).  
  - testability: by using IoC we can mock the implementation as we wish which is not possible if we use the implementation.  
## 4. What design patters have you used in your application?

In order to apply IoC I had to use the dependency injection pattern which consisits in injecting the dependencies in the constructor of the class rather than doing a new.
I also tried to separate the different layers even though the project is quite small. In order to achieve this, I created a service layer.
## 5. How would you debug an issue in production code?
I would look at the logs first to try to understand what happened, if the logs are not sufficient, I would try to reproduce the problem in another environment which is either dev or test.  
I would then fix the bug and release it.

## 6. What are you two favourite libraries for .Net?
I like automapper that makes the basic mapping much easier. I think AutoFixture can also be very usefull to write unit tests when big object needs to be mocked.
## 7. What is your favourite new feature in .Net?

I think pattern matching is nice, if it is about C# 9, records look good but I have never got the chance to use them yet.
## 8. Describe yourself in JSON.
{  
	"firstname": "Gregory",  
	"lastname": "Libert",  
	"age": 26,  
	"nationality": "Belgian",  
	"skills": [  
		"C#",  
		"Dotnet core",  
		"Sql",  
		"Azure",  
		"html",  
		"css"  
	],  
	"experienceInYear": 4,  
	"languages": [  
		"french",  
		"english"  
	]  
}  
