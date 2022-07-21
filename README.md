# shredis
the simplest implementation of memcache with dockerization

## Deploying to Docker:
Docker build: 
```
docker build -t shredis .
```
Docker run: 
```
docker run -d -p 32145:80 --name shredis shredis
```
## Docker compose:
```
docker-compose up
```
## Deploying to Docker k8s:
```
kubectl apply -f k8s\shredis.yml
```
## Helm deploying to Docker k8s:
```
helm install shredis /helm
```
## Swagger: 
```
http://localhost:32145/swagger/index.html
```
