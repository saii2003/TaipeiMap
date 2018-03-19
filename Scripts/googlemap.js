/*景點導航
先建立兩個google map物件,DirectionsService及DirectionsRenderer,
DirectionsService用來計算路線
DirectionsRenderer用來顯示路線
建立calculateAndDisplayRoute方法，用來計算出發地到目的地的路線和將路線繪製到google map上
建立change事件當User輸入完查詢資訊後會觸發onChangeHandler
*/

function initMap() {
    var directionsService = new google.maps.DirectionsService; //查詢路線物件
    var directionsDisplay = new google.maps.DirectionsRenderer; //顯示路線物件
    var mapLatlng = { lat: 25.047908, lng: 121.517315 };//初始座標
	
	//建立初始地圖
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: mapLatlng
    });
	
    directionsDisplay.setMap(map);//將查詢的景點路線顯到地圖上
    directionsDisplay.setPanel(document.getElementById('panel'));//將路線提示導覽顯示到網頁上
    
	//用來計算及顯示路線
    var onChangeHandler = function () {
        calculateAndDisplayRoute(directionsService, directionsDisplay);
    };
	
	//建立change事件，使用者輸入查詢資訊後會觸發onChangeHandler方法
    document.getElementById('start').addEventListener('change', onChangeHandler);//出發地
    document.getElementById('mode').addEventListener('change', onChangeHandler);//交通工具
    document.getElementById('end').addEventListener('change', function () {//目的地
        calculateAndDisplayRoute(directionsService, directionsDisplay);
        document.getElementById('map').style.width = "80%";
        document.getElementById('panel').style.height = "500px";
    });

}
function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    var selectedMode = document.getElementById('mode').value;//交通工具：汽車、大眾運輸、步行
	
	//計算路線
    directionsService.route({
        origin: document.getElementById('start').value,//出發地
        destination: document.getElementById('end').value,//目的地
        travelMode: google.maps.TravelMode[selectedMode]//交通工具
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









