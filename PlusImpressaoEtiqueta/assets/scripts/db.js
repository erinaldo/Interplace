var mysql = require('mysql');
var connection = mysql.createConnection({
    host: '187.45.196.174',
    user: 'interplacelog',
    password: 'interplace2020',
    database: 'interplacelog'
});

connection.connect();

connection.query('SELECT 1 + 1 AS solution', function (error, results, fields) {
    if (error) throw error;
    console.log('The solution is: ', results[0].solution);
});

connection.end();