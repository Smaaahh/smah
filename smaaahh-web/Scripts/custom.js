var map, posDeparture;
var markers = [];
var directionsDisplay;
var directionsService;

var iconDeparture;
var iconVoiture;
var iconArrived;

var infowindow;

var listeDriverFree;

function initMap() {
    directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer();

    iconDeparture = {
        url: "../content/images/map-marker64.png",
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(64, 64),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(32, 64)
    };
    iconArrived = {
        url: "../content/images/flag64.png",
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(64, 64),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 64)
    };
    iconVoiture = {
        url: "../content/images/car2.png",
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(32, 20),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(16, 20)
    };
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
    infowindow = new google.maps.InfoWindow();

    var uluru = { lat: 48.864716, lng: 2.349014 };
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 6,
        center: uluru,
        styles: styler
    });
    directionsDisplay.setMap(map);

    var input = document.getElementById('pac-input');
    if (input != undefined) {

        var searchBox = new google.maps.places.SearchBox(input);
        //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

        // Bias the SearchBox results towards current map's viewport.
        map.addListener('bounds_changed', function () {
            searchBox.setBounds(map.getBounds());
        });

        
        // Listen for the event fired when the user selects a prediction and retrieve
        // more details for that place.
        searchBox.addListener('places_changed', function () {
            var places = searchBox.getPlaces();
            if (places.length == 0) {
                return;
            }
            
            // For each place, get the icon, name and location.
            var bounds = new google.maps.LatLngBounds();
            places.forEach(function (place) {
                //document.getElementById('depart-input').value = place.;
                if (!place.geometry) {
                    console.log("Returned place contains no geometry");
                    return;
                }
                setMarker(iconArrived, place.name, place.geometry.location);
                
                if (place.geometry.viewport) {
                    // Only geocodes have viewport.
                    bounds.union(place.geometry.viewport);
                } else {
                    bounds.extend(place.geometry.location);
                }
            });
            map.fitBounds(bounds);
        });
    }
    
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
                        map.setCenter(pos);
                        var marker = new google.maps.Marker({
                            position: pos,
                            map: map,
                            icon: iconVoiture,
                            title: listeDriverFree[i].UserName,
                            zIndex: i
                        });
                        map.setZoom(10);
                        google.maps.event.addListener(marker, 'click', function () {
                            infowindow.setContent("Conducteur : " + listeDriverFree[this.zIndex].UserName + "<br>Note : " + listeDriverFree[this.zIndex].Rating);
                            infowindow.open(map, this);
                        });

                    }
                }
            }
        }
    };

    xhr.send();

    if (navigator.geolocation) {
        
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            posDeparture = pos;
            map.setCenter(pos);

            setMarker(iconDeparture,"Point de départ", pos);
            map.setZoom(10);
        }, function () {
            handleLocationError(true, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, map.getCenter());
    }
}

function setMarker(icon, title, pos) {
    
    map.setCenter(pos);
    markers.push(new google.maps.Marker({
        position: pos,
        map: map,
        icon: icon,
        title: title
    }));
    map.setZoom(10);
}


function calcRoute() {
    var start = document.getElementById('depart-input').value;
    var end = document.getElementById('pac-input').value;
    var request = {
        origin: posDeparture,
        destination: end,
        travelMode: 'DRIVING'
    };
    directionsService.route(request, function (result, status) {
        console.log(status);
        if (status == 'OK') {
            directionsDisplay.setDirections(result);
        }
    });
}

function handleLocationError(browserHasGeolocation, pos) {
    console.log(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
}
