# TvMaze Scraper API
TvMaze Scraper API live version can be found here:
https://tvmazescraper-rest-api.herokuapp.com/swagger/index.html
![TvMaze Scraper API](https://i.imgur.com/0qbyXRm.png)

### Tools
- Visual studio version: 16.8.2
- C# 7.1
- .NET Core 2.1
- Docker


#### 1. Download and install Docker for windows
https://hub.docker.com/editions/community/docker-ce-desktop-windows/

#### 2. Run the project
Navigate to the project folder where the **docker-compose.yml** file is contained, for example:
![docker-compose file](https://i.imgur.com/EzkO7pA.png)

> C:\git\tvmazescraper-rest-api

Open a console in project root and execute:

    docker-compose build
    docker-compose up -d

![docker-compose build](https://i.imgur.com/NO01iq4.png)
![docker-compose up -d](https://i.imgur.com/4Byk9Yt.png)

#### 3. Swagger
The project has a swagger UI to test endpoints.
Is available in URL:
[http://localhost/swagger/index.html](http://localhost/swagger/index.html)

#### 4. Background service
There is a hosted service that will run when application starts and start fetching shows and casts from TvMaze Api.
After a couple of seconds there will be data to test the paginated endpoint.
There is also a scheduler that will sync from last page every day at 00:01 UTC(-1) following specified CRON expression.

You can test paginated endpoint with the following JSON:

    {
      "Page": 1,
      "PageSize": 10
    }
    
![swagger response](https://i.imgur.com/zh4O5HB.png)