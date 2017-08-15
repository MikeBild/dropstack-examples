# DotNet Core 1.1 (F# + Suave) example

**Docker + F# / Suave Live Example**

## Run

```bash
dotnet restore
dotnet run
```

```bash
curl -s http://localhost:8083
```

## Deploy via [https://dropstack.run](https://dropstack.run)

```bash
dropstack deploy
```

## Docker

```bash
docker build -t dotnet-core2-fsharp-suave .
docker run -it -p 8083:80 dotnet-core2-fsharp-suave
```
