<?php
$xmlDoc = new DOMDocument();
$xmlDoc->load("Customers.xml");
$mysql_hostname = "localhost"; // Example : localhost
$mysql_user = "winemp83";
$mysql_password = "Julia2013!";
$mysql_database = "winemp83";

$bd = mysql_connect($mysql_hostname, $mysql_user, $mysql_password) or die("Oops some thing went wrong");
mysql_select_db($mysql_database, $bd) or die("Oops some thing went wrong");

$xmlObject = $xmlDoc->getElementsByTagName('item');
$itemCount = $xmlObject->length;

for ($i=0; $i < $itemCount; $i++){
  $title = $xmlObject->item($i)->getElementsByTagName('title')->item(0)->childNodes->item(0)->nodeValue;
  $link  = $xmlObject->item($i)->getElementsByTagName('url')->item(0)->childNodes->item(0)->nodeValue;
  $sql   = "INSERT INTO `items` (title, url) VALUES ('$title', '$link')";
  mysql_query($sql);
  print "Finished Item $title n<br/>";
}

?>