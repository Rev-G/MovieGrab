#http://www.boxofficemojo.com/data/js/moviegross.php?id=starwars3.htm&shortgross=0
#urllib.urlencode({'id': "starwars3.htm", 'shorgross': 0})
$urlargs = "starwars3.htm","0"
#$url = 'http://www.boxofficemojo.com/data/js/moviegross.php?id=starwars3.htm&shortgross=0'
$url = "http://www.boxofficemojo.com/data/js/moviegross.php?id={0}&shortgross={0}" -f "$urlargs[0],$urlargs[1]"
$movie = Invoke-WebRequest -Uri $url

$movie


