/*
景點周邊設施查詢
*/		 
		 
		 var map;
         var infowindow;

         function initsMap() {
             var defaultLatlng = { lat: 25.033893, lng: 121.564488 };//初始座標
			 
			 //建立初始地圖
             map = new google.maps.Map(document.getElementById('map'), {
                 center: defaultLatlng,
                 zoom: 16
             });

             infowindow = new google.maps.InfoWindow();
            

             var service = new google.maps.places.PlacesService(map);//用於查詢附近的設施
			 
             document.getElementById('confirm').addEventListener('click', function () {
                 var circles = [];
                 //設定查詢景點的座標
                 var searchString = document.getElementById('end').value;
                 var commaPos = searchString.indexOf(',');
                 var lat = parseFloat(searchString.substring(0, commaPos));
                 var lng = parseFloat(searchString.substring(commaPos + 1, searchString.length));
                 var latlngs = new google.maps.LatLng(lat, lng);
				 		 
                 //設定查詢項目 醫院,停車場,加油站...等
                 var searchType = document.getElementById('type').options[document.getElementById('type').selectedIndex].value;
                 //設定查詢半徑範圍 500m,1km,2km
                 var circle = document.getElementById('radius').options[document.getElementById('radius').selectedIndex].value;

                 //設定圓參數
                 var circleOptions = {
                     strokeColor: "#FF0040",
                     strokeOpacity: 0.1,
                     strokeWeight: 1,
                     fillColor: "#DF013A",
                     fillOpacity: 0.1,
                     map: map,
                     center: latlngs,//景點的座標為中心
                     radius: parseInt(circle)//半徑
                 };
                 //畫圓
                 circles.push(new google.maps.Circle(circleOptions));

				 //查詢景點半徑的設施
                 service.nearbySearch({
                     location: latlngs,
                     radius: parseInt(circle),
                     types: [searchType]
                 }, callback);
                 map.setCenter(latlngs);
             });
         }
		 //將查詢後的設施插上Marker
         function callback(results, status) {
             if (status === google.maps.places.PlacesServiceStatus.OK) {
                 for (var i = 0; i < results.length; i++) {
                     createMarker(results[i]);
                 }
          
             }
         }
		 //建立查詢周圍設施的Marker
         function createMarker(place) {
             var placeLoc = place.geometry.location;
             var marker = new google.maps.Marker({
                 map: map,
                 position: place.geometry.location
             });



			//將設施名稱及推薦指數寫入infowindow內
             google.maps.event.addListener(marker, 'click', function () {
                 var placeRating = null;
                 if (place.rating == undefined) {
                     placeRating = "0";
                 }
                 else {
                     placeRating = place.rating;
                 }


                 infowindow.setContent(place.name + "<br/>" + "推薦指數：" + placeRating + " 顆星");
                 infowindow.open(map, this);
             });
         }