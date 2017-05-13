import json
import urllib

#http://www.boxofficemojo.com/data/js/moviegross.php?id=starwars3.htm&shortgross=0
params = urllib.urlencode({'id': "starwars3.htm", 'shortgross': 0})
movie = urllib.urlopen("http://www.boxofficemojo.com/data/js/moviegross.php?%s" % params)

print movie.read()
movie.close()
