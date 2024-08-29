# Stage 1: Build the React app
FROM node:18 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the package.json and package-lock.json to the container
COPY package*.json ./

# Install dependencies
RUN npm install --frozen-lockfile

# Copy the rest of the application code
COPY . .

# Build the React application for production
RUN npm run build

# Stage 2: Serve the React app using a simple Node.js server
FROM node:18-alpine AS production

# Set the working directory
WORKDIR /app

# Copy the build files from the previous stage
COPY --from=build /app/build /app/build

# Install a lightweight HTTP server
RUN npm install -g serve

# Expose port 3000 to the outside world
EXPOSE 3000

# Command to serve the React app
CMD ["serve", "-s", "build", "-l", "3000"]
