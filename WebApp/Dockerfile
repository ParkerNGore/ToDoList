# Stage 1
FROM node:16.18.1 as build
WORKDIR /usr/local/app
COPY ./ /usr/local/app/
RUN npm install
RUN npm install -g @angular/cli@14.2
RUN ng build --configuration production
# Stage 2
FROM nginx:1.16.1
COPY --from=build /usr/local/app/dist/web-app /usr/share/nginx/html
EXPOSE 80
