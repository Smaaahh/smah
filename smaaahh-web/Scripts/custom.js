var map;
function initMap() {
    var styler = [
        {
            "featureType": "road.highway.controlled_access",
            "elementType": "geometry.fill",
            "stylers": [
                {
                    "color": "#f7c5c1"
                }
            ]
        },
        {
            "featureType": "water",
            "elementType": "geometry.fill",
            "stylers": [
                {
                    "color": "#a5bce2"
                },
                {
                    "visibility": "on"
                }
            ]
        }
    ];

    var uluru = { lat: 48.864716, lng: 2.349014 };
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 7,
        center: uluru,
        styles: styler
    });
    var infoWindow = new google.maps.InfoWindow({ map: map });
    var listeDriverFree;
    var xhr = new XMLHttpRequest();
    xhr.open("get", "http://localhost:51453/api/Drivers/Free", true);
    xhr.send(null);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200) {
            if (xhr.readyState == 4) {
                Alert(xhr.responseText);
            }
        }
        else {
            console.log("error " + xhr.status)
        }
    }

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            //infoWindow.setPosition(pos);
            //infoWindow.setContent('Location found.');
            map.setCenter(pos);
            marker = new google.maps.Marker({
                position: pos,
                map: map
            });
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
}
    