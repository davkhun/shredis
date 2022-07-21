# shredis
the simplest implementation of memcache with dockerization

## Docker build: 
```
docker build -t shredis .
```
## Deploying to Docker:
Install: 
```
docker run -d -p 32145:32145 --name shredis shredis
```
Remove:
```
docker kill shredis
docker rm shredis
```
## Docker compose:
Install:
```
docker-compose up
```
Remove:
```
docker-compose down
```
## Deploying to Docker k8s:
Install:
```
kubectl apply -f k8s\shredis.yml
```
Remove:
```
kubectl delete -f k8s\shredis.yml
```
## Helm deploying to Docker k8s:
Install:
```
helm install shredis /helm
```
Remove:
```
helm uninstall shredis
```
## Swagger: 
```
http://localhost:32145/swagger/index.html
```
