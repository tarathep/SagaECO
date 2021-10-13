# SagaECO
Emil Chronicle Online Server
https://econline.org


## Docker

SagaLogin

```sh
docker run -d --restart always --name sagalogin -p 12200:12200 -v d:/SagaConfig/Config:/Config -v d:/SagaConfig/Log:/Log kietara/sagalogin:latest
```

SagaMap

```sh
docker run -d --restart always --name sagamap -p 12201:12201 -v d:/SagaConfig/Config:/Config -v d:/SagaConfig/Log:/Log kietara/sagamap:latest
```
