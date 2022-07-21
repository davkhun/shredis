# shredis
the simplest implementation of memcache with dockerization

## Deploying to Docker:
Docker build: 
```
docker build -t shredis .
```
Docker run: 
```
docker run -d -p 8080:80 --name shredis shredis
```
Docker compose:
```
docker-compose up
```
Swagger: 
```
http://localhost:8080/swagger/index.html
```
## Deploying to Docker k8s:
```
kubectl apply -f k8s\shredis.yml
```
Swagger: 
```
http://localhost:30000/swagger/index.html
```
## Helm deploying to Docker k8s:
```
helm install shredis /helm
```
Swagger: 
```
http://localhost:30000/swagger/index.html
```
