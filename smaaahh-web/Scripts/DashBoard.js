$(document).ready(function () {
    // mettre à jour le dashboard toutes les 30 sec
    var delay = 30; // Toutes les 5 secondes

    setTimeout(function () {
        location.reload();
    }, delay * 1000);


    // récupérer la géolocalisation du driver

    if (navigator.geolocation) {

        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude + Math.random() - 0.5,
                lng: position.coords.longitude + Math.random() - 0.5
            };
            //alert("Latitude : " + pos.lat + " Longitude : " + pos.lng);
            // mettre à jour dans l'api
            $.ajax({
                url: '/ajax/UpdateDriverPosition',
                type: 'POST',
                dataType: 'json',
                data: {
                    "latitude": pos.lat,
                    "longitude": pos.lng
                },
                success: function (response, statut) {

                },
                error: function (response, statut, erreur) {

                }
            });
            //map.setCenter(pos);
            //setMarker(iconPerson, "Point de départ", pos);
            //map.setZoom(10);
        }, function () {
            //handleLocationError(true, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, map.getCenter());
    }
});