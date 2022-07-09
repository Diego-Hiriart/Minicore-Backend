# Web Engineering Mini-Core assignment: Back-end API
This API has been developed using .NET Core, and it used as the back end for the Mini-Core assignment. Since it is a relatively small project, this API only returns basic information about a parking passes system and its users. The .NET framework was chosen for this API since I already have some experience with it for this kind of applications, and because it possible to create an API rather quickly with this technology. Previous challenges had with this type of project about CORS and connection strings configuration have been more adequately solved with the proper use of settings files as well as configuration and environment information provided by the IConfiguration and IWebHostEnvironment interfaces.

# Installing and running the project locally
Since it is a .NET Framework app, you should ideally run it on Visual Studio once you have downloaded the code so that it runs locally and you can test it, for this don't forget to add the ***ASP.NET and web development*** workload. If you prefer, you can use Visual Studio Code, it is lighter and it will work as well if you run it with this command in the terminal: 
```
dotnet run
```
Remember, this is a .NET API so if you use VS Code you also need the .NET SDK. To consume the API, it is also quite important to properly set up CORS, meaning you have to add the allowed origins for the front-end.

# Deployed API
This API has been deployed to Heroku, so that it can be used with a front-end application. You can find it in the following URL; however this is just the base address and you will get a 404 error, continue reading to find URLs you can test with a GET method:
  -  [https://ingweb-back-hiriart.herokuapp.com/api](https://minicore-back-hiriart.herokuapp.com/api/)

To see this API at work, you can visit the deployed [front-end React application](https://minicore-front-hiriart.herokuapp.com/), which is has its own [GitHub repo](https://github.com/Diego-Hiriart/Minicore-Frontend). Alternatively, you can visit these URLs which will return a JSON with data:
  - [https://minicore-back-hiriart.herokuapp.com/api/users](https://minicore-back-hiriart.herokuapp.com/api/users)
  - [https://minicore-back-hiriart.herokuapp.com/api/pass-types](https://minicore-back-hiriart.herokuapp.com/api/pass-types)
  - [https://minicore-back-hiriart.herokuapp.com/api/user-passes](https://minicore-back-hiriart.herokuapp.com/api/user-passes)

# Using the API
Firstly, you must make sure you have the right CORS configuration to consume the API, then you can consume it in these two ways:
  - Using endpoints that only return JSONs (which are the three above, from the deployed API).
  - The filtering endpoint, which needs a date to be passed to it as it is a POST method, it returns passes purchased by users, with certain filters applied (explained later [here](#mini-core-functionality)).
To see exactly which endpoints you can use, as well as the data they return/take, check out the API's [controllers](https://github.com/Diego-Hiriart/Minicore-Backend/tree/main/Controllers) and [models](https://github.com/Diego-Hiriart/Minicore-Backend/tree/main/Models).

# Mini core functionality
The project consists of a site where parking passes can be checked, this includes viewing information about registered users, types of passes available, the passes that users have purchased, and, most importantly, a filtering functionality. When using the filter, a date must be passed, and the following is achieved:
  - By default, only filter through passes that, to the present day, have not ran out.
  - Show an estimated number of remaining passes (to the present day).
  - Calculate an estimated date on which the passes will ran out.
  - Using the selected date, filter out passes that would run out before that date.

# Contact
[hiriart.leon.d@gmail.com](mailto:hiriart.leon.d@gmail.com)
