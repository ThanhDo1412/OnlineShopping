# Online Shopping

## Description
This is the BackEnd platform of Online Shopping application. It's used for search, filter, order products and view product details. Depend on this situation, the system will log all activities of user on product

## Technical applied
- Asp .Net Core web API
- Repository patten
- Entity framework Code first

## How to run
Firstly, please download project and open cmnd in this folder. Please check connectionstring before execute commands to initalize database as below: 
```
dotnet ef database update -c OnlineShoppingContext -p OnlineShopping.WebApi
```
   After finished database, run commands to restore and build project:
```
dotnet restore
dotnet build
```
  Follow by this, run the webapi project by this command
```
dotnet run --project ./OnlineShopping.WebApi/OnlineShopping.WebApi.csproj
```
  Finally, project is working. Enjoy it.

## API list
Get product branch
```
curl --location --request GET 'https://localhost:5001/api/product/branch'
```
Search product by codition
```
curl --location --request GET 'https://localhost:5001/api/product/search?page=1&size=3&OrderBy=PriceAsc'
```
```
curl --location --request GET 'https://localhost:5001/api/product/search?page=1&size=3&OrderBy=PriceAsc&term=iphone&branchs=Apple,Samsung'
```
Get Product details
```
curl --location --request GET 'https://localhost:5001/api/product/2'
```
## Architecture


## Database Structure

My database structure can slipt to 2 databases:
  - Product database : it's used for managing product, product information and everything related to product
  - Activity database: this database is used for supporting sale and marketing team to do the report.
  
## Future plane to improve
  - Apply Swagger for API Doc
  - Apply cache for product search
  - Improve table activity: slipt more column and more information, make user easy to do the report
  - Apply CI/CD
  - Apply Docker
  - Change structure from monolithic to Microservice (1 product service and 1 activity service) -> every call from searching product or going to product detail will capture and send an message to message queue and handle on activity service 