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
        zoom: 6,
        center: uluru,
        styles: styler
    });

    var listeDriverFree;
    var iconVoiture = {
        url: "../content/images/car2.png",
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(32, 20),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(16, 20)
    };
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/ajax/GetDriversFree", true);
    xhr.onreadystatechange = function () {

        if (xhr.status === 200) {
            if (xhr.readyState === 4) {

                if (xhr.responseText != null) {
                    listeDriverFree = JSON.parse(xhr.responseText);
                    
                    for (i = 0; i < listeDriverFree.length; i++) {
                        var pos = {
                            lat: listeDriverFree[i].PosX,
                            lng: listeDriverFree[i].PosY
                        };
                        
                        marker = new google.maps.Marker({
                            position: pos,
                            map: map,
                            icon: iconVoiture
                        });
                    }
                }
            }
        }
    };

    xhr.send();

    if (navigator.geolocation) {
        var iconPerson = {
            url: "../content/images/map-marker64.png",
            // This marker is 20 pixels wide by 32 pixels high.
            size: new google.maps.Size(64, 64),
            // The origin for this image is (0, 0).
            origin: new google.maps.Point(0,0),
            // The anchor for this image is the base of the flagpole at (0, 32).
            anchor: new google.maps.Point(32,64)
        };
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            
            map.setCenter(pos);
            marker = new google.maps.Marker({
                position: pos,
                map: map,
                icon: iconPerson
            });
            map.setZoom(10);
        }, function () {
            handleLocationError(true, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, map.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, pos) {
    console.log(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
}