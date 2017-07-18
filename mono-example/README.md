# Mono-Solution (C# + Nancy) example

## Deploy via [https://dropstack.run](https://dropstack.run)

```bash
dropstack login // only once per local user

./deploy.sh // for each new version of the service
```

`deploy.sh` will run `dropstack deploy` with a couple of parameters.

## Local execution

### Build

```bash
./ds-build.sh
```

### Run

```bash
./local-run.sh

curl -s http://localhost:8080/helloworld

curl -s http://localhost:8080/hellome?name=Peter

curl --data '' -s http://localhost:8080/hellome?name=Mary
curl --data '' -s http://localhost:8080/hellome?name=Mary
curl --data '' -s http://localhost:8080/hellome?name=Mary

curl -s http://localhost:8080/provokeexception

curl -s http://localhost:8080/provokeexit
```

## Remote execution
dropstack will execute three scripts:

* one for building the solution, `ds-build.sh`
* one for running the tests in the solution, `ds-test.sh`
* one for running the resulting exe, `ds-run.sh`

This is accomplished by a couple of command line parameters for `dropstack deploy`. Check out `deploy.sh`:

```
dropstack deploy -v build=./ds-build.sh -v test=./ds-test.sh -v start=./ds-run.sh
```

After deployment with dropstack have a look the `.dropstack.json` file:

```
{
  "variables": [],
  "instances": 1,
  "type": "mono",
  "name": "xyalhlshr", // this name will be different for you
  "serviceUrl": "xyalhlshr.cloud.dropstack.run"
}
```

Use the `serviceUrl` to call the service, e.g.

```
curl -s http://xyalhlshr.cloud.dropstack.run/hellome?name=Peter
```

To watch what's happening to the server run `dropstack logs xyalhlshr`. You'll see a live stream of whatever is output to the console by the service.

Just for the fun of it call the `/provokeexit` endpoint. It will cause an immediate exit of the server process. But then watch how the service will be back online after a short period!

### Default
If you don't specify any scripts dropstack will apply some defaults for Mono projects:

* It will build the one `.sln` or the one `.*proj` file in the folders you pushed.
* It will run no tests.
* It will run the one `.exe` created during build.

Upon any ambiguities encountered dropstack will halt the deployment process - and any old version of the service keeps running.