function httpGetAsync(url, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4 && xmlHttp.status === 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

var url = "https://avatars.abstractapi.com/v1/?api_key=1cd41182df6145838077a068b9036f92&name={name}&background_color=00adef&image_size=165"

httpGetAsync(url)