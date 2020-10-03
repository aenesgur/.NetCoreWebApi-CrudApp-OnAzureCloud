# Crud App in .Net Core 3.1 Web API on Azure Cloud Platform
 Crud App in .Net Core 3.1 Web API with Redis and SQL Server which are running on Azure Cloud Platform. Application developed according to Multi Layer Architecture, SOLID principles, Asynchronous programming etc. After development, application dockerized and published on Azure Cloud Platform 
 with domain "http://crudapp-azure.westeurope.azurecontainer.io/api/book/getall/".

Redis Cache is used effectively. After Deletion and Update, all cached for "Book" is cleared.

** Medium article link of this project: https://medium.com/@aenesgur/net-core-web-api-3-1-crud-operations-with-redis-and-sql-server-on-azure-cloud-7657b5fd1196

** DockerHub: https://hub.docker.com/repository/docker/aenes/crudapp-azure

** appsettings.json did not published because of the security reasons for Redis Cache Server and SQL Server. **

#### Used Technologies:
* .Net Core 3.1
* Redis Cache
* SQL Server
* Dapper
* Docker
* Azure

### Crud Operations
- Adding List of Data
![addlist](https://user-images.githubusercontent.com/47754791/94902548-08464200-04a1-11eb-8425-054a89f5b17a.PNG)


- Getting All Data with paging(every page has just four data)
![getall](https://user-images.githubusercontent.com/47754791/94902550-0a100580-04a1-11eb-9078-0a26152abfc5.PNG)


- Geting Data By Id
![getbyid](https://user-images.githubusercontent.com/47754791/94902558-0d0af600-04a1-11eb-9b83-38fb2c62217a.PNG)


- Updating
![update](https://user-images.githubusercontent.com/47754791/94902560-0ed4b980-04a1-11eb-9e85-613ec68475e0.PNG)

- Deleting
![del](https://user-images.githubusercontent.com/47754791/94902578-1300d700-04a1-11eb-83fc-1150fda84b22.PNG)

- Adding
![add](https://user-images.githubusercontent.com/47754791/94902709-4d6a7400-04a1-11eb-9587-4d78c57b359d.PNG)
