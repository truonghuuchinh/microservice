# microservice
microservice

#Manage container in UI
	- Portianer: user:admin, password: admin123. This is service to manage containers on the UI

*Introduction use the app and create app
	1. Catalog service
		- This is a service contain product and catalog use database is MongoDb, Mongo db got from docker hub equal command $docker pull mongo, you can push this project to docker equal way right click on the project chose Add->docker compose->add docker compose->linux and ok
	2. Basket service:
		- This is service to store item of user to payment use redis db, redis db use memory direcly from hardware so if you has big data you should use another databse.
	