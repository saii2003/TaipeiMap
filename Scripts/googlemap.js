function initMap() {
    var directionsService = new google.maps.DirectionsService;
    var directionsDisplay = new google.maps.DirectionsRenderer;
    var mapLatlng = { lat: 25.047908, lng: 121.517315 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: mapLatlng
    });
    directionsDisplay.setMap(map);
    directionsDisplay.setPanel(document.getElementById('panel'));

    var onChangeHandler = function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    };
    document.getElementById('start').addEventListener('change', onChangeHandler);

    document.getElementById('mode').addEventListener('change', onChangeHandler);

    document.getElementById('end').addEventListener('change', function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);
        document.getElementById('map').style.width = "80%";
        document.getElementById('panel').style.height = "500px";
    });

}
function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    var selectedMode = document.getElementById('mode').value;
    directionsService.route({
        origin: document.getElementById('start').value,
        destination: document.getElementById('end').value,
        travelMode: google.maps.TravelMode[selectedMode]
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
            //顯示距離
            document.getElementById('distance').innerHTML =
                "距離約：" + (response.routes[0].legs[0].distance.value / 1000) + "公里";

            // 顯示抵達時間
            document.getElementById('duration').innerHTML =
                "時間約：" + formatSecond(response.routes[0].legs[0].duration.value);
        }
    });
}

//格式化秒數轉換
function formatSecond(sec) {
    var hr = Math.floor(sec / 3600);
    var min = Math.floor((sec - (hr * 3600)) / 60);
    var secs = parseInt(sec - (hr * 3600) - (min * 60));
    if (hr < 1) {
        return min + "分" + secs + "秒";
    }
    else {
        return hr + "小時" + min + "分" + secs + "秒";
    }

}









