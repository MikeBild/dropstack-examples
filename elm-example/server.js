PORT = 80;
HOST = '127.0.0.1';

var fs = require('fs'),
http = require('http');


http.createServer(function(request, response) {
    fs.createReadStream('Index.html', {
        'bufferSize': 4*1024
    }).pipe(response);
}).listen(PORT, HOST);;