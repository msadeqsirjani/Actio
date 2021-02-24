cd src
docker publish ./Actio.Api -c Release -o ./bin/Docker
docker publish ./Actio.Services.Activities -c Release -o ./bin/Docker
docker publish ./Actio.Services.Identity -c Release -o ./bin/Docker