## Local Run

Run following commands in the root directory of this repository (You will need ports 3000 and 5000 unoccupied):

dotnet restore
dotnet build
dotnet run --urls=http://localhost:5000/ --project TranslationManagement.Api

cd Frontend
npm install
npm run build
npm run dev


## Comments to the backend refactoring

- I chose **Clean Architecture** (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) as an overall backend architecture. 
- I was also considering three-layered model (it is as sligtly simpler for this project size), but with a messaging included I prefer Clean Architecture because of better abstraction of Application (Use case) layer.
- **entities** and **data transfer objects** was divided - so that Api users do not  need to worry about direct relation between Translator and Job (which I added into the database).
- I have solved unreliable notification service by adding **wait and retry** mechanism with exponential backoff so that the notification service might not be exhausted by too many retries in short time.
- I have added filter for **global error handling** which logs any uncaught exception and differs between client and server errors.
- There is a very basic **telemetry** in the project (in /metrics endpoint) and basic logging.
- Several small **tests** added  - they test Application layer functionality (because of my time limitations included only two tests - this is now the biggest technical debt left - the app needs much more automation tests). I used non-mocked DbContext with real database, because I consider this kind of EFCore-dependent code testing more useful than mocked AppDbContext that might require quite a lot of code for simple tests. Therefore I consider these to be more integration than unit tests (I would cover other functionality by both integration and unit tests).

## Comments to the REST API design
- I have added **versioning** which was missing (API should always contain versions).
- Endpoints that update data are changed from HTTP Post method to **HTTP Put** method.
- I have slightly changed urls and added several parameters into url path, but this is just a detail and was not neccessary.

## Additional features
- "only Certified translators can work on jobs" implemented - requirement put to the entity validations so that it is covered everywhere.

## Frontend
- Implemented basic frontend with simple translators list and new translator dialog.
- Didn't use any **central state management** like Redux - application is too simple. Basic component local state is sufficient for now.
- Use tailwind as a CSS library, shadcn as acomponent library (so that the development of frontend is faster).
- Error handling is only through console.error, missing other logging for now (I would add sentry, seq api or something similar for logging).

## TODO in future (from top priority):
- Better test coverage,
- Better telemetry and logging (dashboards etc.),
- Static texts in frontend added to the localization resources (they are hard-coded now),
- Added docker-compose for faster local run,
- Central state management on frontend if application would be more and more complex