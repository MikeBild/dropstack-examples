# Mono-Solution (C# + Nancy) example

[Mono-Solution C# + Nancy Live Example](https://qkvwwiko.cloud.dropstack.run/add)

## Deploy via [https://dropstack.run](https://dropstack.run)

```bash
dropstack deploy
```

## Local execution

### Build

```bash
./build.sh
```

### Run

```bash
./start-dev.sh
curl -s http://localhost:8080/helloworld
curl -s http://localhost:8080/hellome?name=Peter
curl --data '' -s http://localhost:8080/hellome?name=Mary
curl --data '' -s http://localhost:8080/hellome?name=Mary
curl --data '' -s http://localhost:8080/hellome?name=Mary
```

`start.sh` is used by dropstack on your server.