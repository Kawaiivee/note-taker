# Node image
FROM node:latest as build

# Working directory
WORKDIR /app

# Copy package.json and yarn.lock
COPY package.json yarn.lock ./

# Install dependencies
RUN yarn install

# Copy the rest of the application code
COPY . .

# Expose ports
EXPOSE 7891 7890

# Run development build
CMD ["yarn", "dev"]