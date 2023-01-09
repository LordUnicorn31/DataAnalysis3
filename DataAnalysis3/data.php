<?php

// Connect to the database
$host="localhost";
    $database="jordiea3";
    $user="jordiea3";
    $password="NcpbTyykNZ";
    $error="Cant connect";

$connection = mysqli_connect($host, $user, $password);
mysqli_select_db($connection,$database) or die("Unable connect to database");

// Check connection
if (!$connection) {
    die("Connection failed: " . mysqli_connect_error());
}

// Fetch the data
$sql = "SELECT * FROM Player";
$result = mysqli_query($connection, $sql);

// Print the data in JSON format
$rows = array();
while($row = mysqli_fetch_assoc($result)) {
    $rows[] = $row;
}

echo json_encode($rows);

// Close the connection
mysqli_close($connection);

?>