<?php

    $host="localhost";
    $database="jordiea3";
    $user="jordiea3";
    $password="NcpbTyykNZ";
    $error="Cant connect";


    $connection = mysqli_connect($host,$user,$password);
    mysqli_select_db($connection,$database) or die("Unable connect to database");
    //Check connection

    if(!$connection)
    {
        die("no connection" . mysqli_connect_error());
    }
    // Grab data from users 
    $deaths = $_POST["deaths"];
    
    // Insert data into users table
    $sql = "INSERT INTO Player (Deaths)
            VALUES('$deaths')";
    $result = mysqli_query($connection,$sql);

    $last_id = $connection->insert_id;
    
    echo $last_id;
?>