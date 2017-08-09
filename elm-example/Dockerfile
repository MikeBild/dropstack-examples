FROM npm
EXPOSE 80
RUN npm install -g elm
ADD . /app
WORKDIR /app
RUN elm make Main.elm --output Index.html
ENTRYPOINT node server.js