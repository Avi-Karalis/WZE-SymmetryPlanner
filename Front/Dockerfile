# Build stage
FROM node:22-alpine AS build
WORKDIR /app

COPY package.json package-lock.json* ./
RUN npm ci

COPY . .

ARG NUXT_PUBLIC_API_BASE=http://api:8080/api
ENV NUXT_PUBLIC_API_BASE=$NUXT_PUBLIC_API_BASE

RUN npm run build

# Runtime stage
FROM node:22-alpine AS final
WORKDIR /app

COPY --from=build /app/.output ./.output

EXPOSE 3000

ENV HOST=0.0.0.0
ENV PORT=3000
# Allow the API base to be overridden at runtime via environment variable
ENV NUXT_PUBLIC_API_BASE=${NUXT_PUBLIC_API_BASE}

CMD ["node", ".output/server/index.mjs"]
