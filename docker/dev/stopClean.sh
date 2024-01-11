# stop containers
docker stop note-taker-frontend
docker stop note-taker-backend
docker stop note-taker-db

# remove containers
docker rm note-taker-frontend
docker rm note-taker-backend
docker rm note-taker-db

# remove images
docker image rm dev-note-taker-frontend:latest
docker image rm dev-note-taker-backend:latest
docker image rm postgres