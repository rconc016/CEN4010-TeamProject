const express = require('express');
const path    = require('path');

const applicationName = 'geek-text';
const indexPage       = 'index.html';
const distPath        = __dirname + '/dist/' + applicationName + '/';

const app = express();

// Serve only the static files form the dist directory
app.use(express.static(distPath));

app.get('/*', function(req,res) {
    res.sendFile(path.join(distPath + indexPage));
});

// Start the app by listening on the default Heroku port
app.listen(process.env.PORT || 8080);