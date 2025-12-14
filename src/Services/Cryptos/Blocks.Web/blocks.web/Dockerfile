# Build stage
FROM node:22 AS build

WORKDIR /app

# Install all deps for build (dev+prod)
COPY Services/Cryptos/Blocks.Web/blocks.web/package*.json ./
RUN npm install

# Copy source
COPY Services/Cryptos/Blocks.Web/blocks.web .

# Build the app (tsc -b && vite build)
RUN npm run build

# Production stage
FROM nginx:1.27-alpine

# Copy built assets from build stage
COPY --from=build /app/dist /usr/share/nginx/html

# Copy entrypoint script and ensure Unix line endings
COPY Services/Cryptos/Blocks.Web/blocks.web/docker-entrypoint.sh /docker-entrypoint.sh
RUN sed -i 's/\r$//' /docker-entrypoint.sh && chmod +x /docker-entrypoint.sh

EXPOSE 80

ENTRYPOINT ["/docker-entrypoint.sh"]