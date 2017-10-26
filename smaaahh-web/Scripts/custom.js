var map, posDeparture, iconDeparture, iconVoiture, iconArrived, directionsDisplay, directionsService, infowindow;
var markerDepart, markerArrive;
var markers = [];

var listeDriverFree, nbKm;

function initMap() {
    directionsService = new google.maps.DirectionsService();
    directionsDisplay = new google.maps.DirectionsRenderer({ suppressMarkers: true });

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
        anchor: new google.maps.Point(3, 64)
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
    
    var inputFrom = document.getElementById('from-input');
    if (inputFrom != undefined) {
        boxSearch(inputFrom, iconDeparture,"depart");
    }

    var inputTo = document.getElementById('to-input');
    if (inputTo != undefined) {
        boxSearch(inputTo, iconArrived, "arrive");
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
                            document.getElementById('driver-name').innerText = listeDriverFree[this.zIndex].UserName;
                            document.getElementById('driver-id').value = listeDriverFree[this.zIndex].UserId;
                            var starsDriver = "";
                            for (i = 0; i < listeDriverFree[this.zIndex].Rating; i++) {
                                starsDriver += '<img src="../content/images/plein.png">';
                            }
                            for (i = listeDriverFree[this.zIndex].Rating; i < 5; i++) {
                                starsDriver += '<img src="../content/images/vide.png">';
                            }

                            document.getElementById('driver-rate').innerHTML = starsDriver;//listeDriverFree[this.zIndex].Rating;
                            infowindow.setContent(document.getElementById('window-driver').innerHTML);//"Conducteur : " + listeDriverFree[this.zIndex].UserName + "<br>Note : " + listeDriverFree[this.zIndex].Rating);
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

            setMarker(iconDeparture, "Point de départ", pos,"depart");
            
            if (inputFrom != undefined) {

                inputFrom.value = "Ma position";
                inputFrom.addEventListener("click", function (e) {
                    if (inputFrom.value == "Ma position") {
                        inputFrom.value = "";
                    }
                });
                inputFrom.addEventListener("blur", function (e) {
                    if (inputFrom.value == "") {
                        inputFrom.value = "Ma position"
                    }
                });
            }
            map.setZoom(10);
        }, function () {
            handleLocationError(true, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, map.getCenter());
    }
    
}

function boxSearch(input,icon, type) {

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
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            $(".validate-choice").hide();
            $("#driver-id-choosen").val('');
            $("#driver-name-choosen").html('');

            setMarker(icon, place.name, place.geometry.location, type);

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

function setMarker(icon, title, pos, type) {
    
    map.setCenter(pos);
    if (type == "depart") {
        if (markerDepart != undefined)
            markerDepart.setMap(null);

        markerDepart = new google.maps.Marker({
            position: pos,
            map: map,
            icon: icon,
            title: title
        });
    }
    if (type == "arrive") {
        if (markerArrive != undefined)
            markerArrive.setMap(null);

        markerArrive = new google.maps.Marker({
            position: pos,
            map: map,
            icon: icon,
            title: title
        });
    }

    map.setZoom(10);
}


function resetRoute() {
    directionsDisplay.setMap(null);
}

function calcRoute() {

    directionsDisplay.setMap(map);
    var start = $('#from-input').val();
    var end = $('#to-input').val();

    if (start == "Ma position") {
        start = posDeparture;
    }
    var request = {
        origin: start,
        destination: end,
        travelMode: 'DRIVING'
    };
    
    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            directionsDisplay.setDirections(result);

            nbKm = directionsDisplay.directions.routes[0].legs[0].distance.value / 1000;

            $('#divNbKm').html(nbKm + " Km");
            
            $.ajax({
                url: '/ajax/GetPrice',
                type: 'GET',
                dataType: 'json',
                success: function (response, statut) {
                    //alert("Youhou : " + response);
                    var price = (response.Price / 100) * nbKm;
                    $("#divPrice").html(price + " €");
                },
                error: function (response, statut, erreur) {
                    console.log(response);
                }
            });
        }
    });
}

function handleLocationError(browserHasGeolocation, pos) {
    console.log(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
}



$(document).ready(function () {
    $(".validate-route").on("click", function () {
        calcRoute();
    });
    $("body").on("click", "#driver-name", function () {
        window.location.replace("/users/profil?id=" + $("#driver-id").val() + "&type=driver");
    });
    $("body").on("click","#choose-driver", function () {
        $(".validate-choice").show();
        $("#driver-id-choosen").val($("#driver-id").val());
        $("#driver-name-choosen").html($("#driver-name").html());
    });

    $(".validate-choice").on("click", function () {
        $.ajax({
            url: '/ajax/ChooseDriver',
            type: 'POST',
            dataType: 'json',
            data: {
                "driverId": $("#driver-id-choosen").val(),
                "posXStart": (markerDepart.position.lat),
                "posYStart": (markerDepart.position.lng),
                "posXEnd": (markerArrive.position.lat),
                "posYEnd": (markerArrive.position.lng),
                "nbKm": (nbKm)
            },
            success: function (response, statut) {
                window.location.replace("/riders/Payment");
            },
            error: function (response, statut, erreur) {
                console.log(response);
            }
        });
    });
});