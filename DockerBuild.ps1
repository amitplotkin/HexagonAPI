docker build  -f HexagonAPI\Dockerfile -t "hexagonapi:v1" .

docker tun -it hexagonapi:v1


docker login --username amitplotkin
docker tag hexagonapi:v1 amitplotkin/hexagonapi:v1
docker push amitplotkin/hexagonapi:v1
