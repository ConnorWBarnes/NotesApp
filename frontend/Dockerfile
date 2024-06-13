# Create image based on the official Node image from dockerhub
FROM node:lts-buster AS development

# Create app directory
WORKDIR /usr/src/app

# Install Angular CLI
RUN npm install -g @angular/cli

# Copy dependency definitions
COPY package.json package-lock.json ./

# Install dependecies
#RUN npm set progress=false \
#    && npm config set depth 0 \
#    && npm i install
RUN npm ci

# Get all the code needed to run the app
COPY . /usr/src/app

# Serve the app
CMD ["ng", "serve", "--host", "0.0.0.0"]

FROM development as dev-envs

RUN apt-get update && apt-get install -y git

# TODO: Uncomment and debug?
# RUN <<EOF
# useradd -s /bin/bash -m vscode
# groupadd docker
# usermod -aG docker vscode
# EOF
# # install Docker tools (cli, buildx, compose)
# COPY --from=gloursdocker/docker / /

CMD ["ng", "serve", "--host", "0.0.0.0"]
