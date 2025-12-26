let marker;
let dotNetObject;
let timeoutId;

window.formMap = {
    initMap: function (model) {
        let map = null;
        dotNetObject = model.dotNetObject;
        if (typeof model.correctPoint !== 'undefined' && model.correctPoint != null) {

            map = L.map('map', {'worldCopyJump': true}).setView([model.correctPoint.latitude, model.correctPoint.longitude], 16);

            marker = new L.marker(L.latLng(model.correctPoint.latitude, model.correctPoint.longitude)).addTo(map);

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {}).addTo(map);
        }

        if (typeof model.points !== 'undefined' && model.points.length > 0) {

            if (map == null) {
                map = L.map('map', {'worldCopyJump': true}).setView([model.points[0].latitude, model.points[0].longitude], 16);

                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {}).addTo(map);
            }

            model.points.forEach(point => {
                L.marker([point.latitude, point.longitude], {
                    icon: L.icon({
                        iconUrl: point.iconPath
                    })
                })
                    .addTo(map)
                    .bindPopup(point.Name);
            });
        }

        if (map == null) {
            map = L.map('map', {'worldCopyJump': true}).setView([35.81203626861781, 50.98454475402833], 18);
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {}).addTo(map);
        }

        map.on('click', function (e) {
            OnMapClick(e);
        });
    }
};

function OnMapClick(e) {

    if (typeof (marker) === 'undefined') {
        marker = new L.marker(e.latlng);
        marker.addTo(e.sourceTarget);
    } else {
        marker.setLatLng(e.latlng);
    }

    clearTimeout(timeoutId);
    timeoutId = setTimeout(function () {
        dotNetObject.invokeMethodAsync('PointChange', {latitude: e.latlng.lat, longitude: e.latlng.lng})
    }, 500);
}