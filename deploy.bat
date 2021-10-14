docker stop sagamap sagalogin
docker rm sagamap sagalogin
docker rmi kietara/sagamap:latest kietara/sagalogin:latest
docker run -d --restart always --name sagalogin -p 12200:12200 -v d:/SagaConfig/Config:/Config -v d:/SagaConfig/Log:/Log -v c:/Users/Bokie/sagaeco/Configuration/DB:/DB kietara/sagalogin:latest
docker run -d --restart always --name sagamap -p 12201:12201 -v d:/SagaConfig/Config:/Config -v d:/SagaConfig/Log:/Log -v c:/Users/Bokie/sagaeco/Configuration/DB:/DB kietara/sagamap:latest