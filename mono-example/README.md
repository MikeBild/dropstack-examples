# Mono-Solution (C# + Nancy) example

## Deploy via [https://dropstack.run](https://dropstack.run)

```bash
dropstack login // only once per local user

dropstack deploy // for each new version of the service
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

curl -s http://localhost:8080/provokeexception

curl -s http://localhost:8080/provokeexit
```

## Remote execution
dropstack will execute the build and start (`start.sh`) scripts.

After deployment with dropstack check the `.dropstack.json` file:

```
{
  "variables": [],
  "instances": 1,
  "type": "mono",
  "name": "xyalhlshr",
  "serviceUrl": "xyalhlshr.cloud.dropstack.run"
}
```

Use the `serviceUrl` to call the service, e.g.

```
curl -s http://xyalhlshr.cloud.dropstack.run/hellome?name=Peter
```

To watch what's happening to the server run `dropstack logs xyalhlshr`. You'll see a live stream of whatever is output to the console by the service.

Just for the fun of it call the `/provokeexit` endpoint. It will cause an immediate exit of the server process. But then watch how the service will be back online after a short period!
