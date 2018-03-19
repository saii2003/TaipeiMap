
var map;

//台北市觀光景點名稱,座標,內容,Marker
var latlngs = [
['台北101', 25.033893, 121.564488,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/101.jpg" />台北101位於台北市信義區，高509.2公尺，地上樓層共有101層、地下5層，地板總面積37萬4千平方公尺，由李祖原聯合建築師事務所設計、KTRT團隊建造，最初名稱為台北國際金融中心，2003年改為101大樓。臺灣第一高樓，目前為世界第9高樓，同時也是全球最高綠建築及環地震帶最高建築物，成為臺北重要地標之一。此外，大樓內擁有全球最大的阻尼器、以及全球起降速度次快的電梯。<br> <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：臺北市信義區信義路五段7號<br>電話：02-8101-8800<br>網站：<a href="www.taipei-101.com.tw/">www.taipei-101.com.tw/</a></address>'
,"~/Content/themes/base/images/101icon.png"],
['陽明山國家公園', 25.194251, 121.5587476,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/yang.jpg" />陽明山原名草山，是台灣第三個設置的國家公園，陽明山國家公園管理處規劃有下列遊憩區：小油坑遊憩區、冷水坑地區、大屯遊憩區、二子坪遊憩區、擎天崗地區、草山行館、陽明書屋、中山樓、林語堂故居、龍鳳谷硫磺谷遊憩區。陽明公園遍植各種櫻花與其他花木，每年春季百花齊放，其間所舉行的陽明山花季均能吸引滿山人潮，為全台最知名的賞花節慶之一。<br> <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市陽明山竹子湖路1-20號<br>電話：02-2861-3601<br>網站：<a href="www.lungshan.org.tw/">www.lungshan.org.tw/</a></address>'
,"~/Content/themes/base/images/yangicon.png"],
['西門町', 25.044953, 121.505978,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/xi.jpg" />西門町位於台北市萬華區，為台北市西區最重要且國際化程度最高的消費商圈，以年輕族群為主要的消費對象，並吸引了許多國際觀光客以自助旅行造訪此處。其特色還包含擁有台北市第一條指標性意義的徒步區，以及象徵青少年的流行次文化、電影街。西門町的範圍，東至中華路、西至康定路、北至漢口街、南至成都路，西門町獨特小吃，例如中華路上的鴨肉扁、峨嵋街的阿宗麵線、開封街的賽門鄧普拉，成都路上的上海老天祿、成都楊桃冰與蜂大咖啡、漢口街的楊記花生玉米冰、昆明街的宏益水晶餃、洛陽街的牛肉麵等。<br> <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div>'
,"~/Content/themes/base/images/xiicon.png"],
['松山文化創意園區', 25.044047, 121.561205,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/sh.jpg" />松山文創園區位於台灣台北市信義區，建於1937年，前身為台灣日治時期「台灣總督府專賣局松山菸草工場」，1945年更名為「台灣省專賣局松山菸草工廠」，1947年又更名為「台灣省菸酒公賣局松山菸廠」，1998年停止生產，2001年由台北市政府指定為第99處市定古蹟，並於2010年正式轉型定名「松山文創園區」。松山菸廠在光復後種植大量植栽，景觀優美，成為台北市東區最大的綠地。除了松山文創園區，松山菸廠舊址目前還有臺北文創大樓。<br> <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市信義區光復南路133號<br>電話：02-2765-1388<br>網站：<a href="www.songshanculturalpark.org/">www.songshanculturalpark.org/</a></address>'
,"~/Content/themes/base/images/shicon.png"],
['華山1914文化創意產業園區 ', 25.044121, 121.529378,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/huashan.jpg" />華山1914文化創意產業園區，前身為「台北酒廠」，為臺灣台北市市定古蹟。在1999年後，成為提供給藝文界、非營利團體及個人使用的藝術展覽、音樂表演等文化活動場地。此外，園區內也有多間餐廳、店舖、藝廊等商業設施。<br> <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市中正區八德路一段1號<br>電話：02-2358-1914<br>網站：<a href="www.huashan1914.com/">www.huashan1914.com/</a></address>'
,"~/Content/themes/base/images/huashanicon.png"],
['台北市立動物園', 24.998852, 121.581068,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/zoo.jpg" />臺北市立動物園是臺北市的一座公立動物園。園區總面積原為182公頃，包含8個戶外展示區、6個室內展示館、5個環境教育教學場所，動物共有377種、2,783隻。整個園區被自然次生林地所圍繞，是一處結合自然景觀形成具生態特色之休閒場所。全園最大的特色是展示環境的布置上採用「地理生態展示法」，依照動物原先的生存環境加以布置在新的環境內，使動物脫離鐵籠的束縛，有自由的活動空間，並創造出與動物原生地最接近的生活環境，使動物不必去改變其生活習性，也讓遊客更能了解動物，是一座具有教育、研究、保護及娛樂功能的動物園。<br> <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市文山區新光路二段30號<br>電話：02-2938-2300#630<br>網站：<a href="www.zoo.gov.taipei/">www.zoo.gov.taipei/</a></address>'
,"~/Content/themes/base/images/zooicon.png"],
['艋舺龍山寺', 25.037143, 121.499890,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/lung.jpg" />艋舺龍山寺，位於台北市萬華，建於1738年（清乾隆3年）5月18日，主祭神觀世音菩薩，是台灣直轄市定古蹟，也是旅遊宗教勝地。艋舺龍山寺與艋舺清水巖和大龍峒保安宮合稱為臺北三大廟門。<br> <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市萬華區廣州街211號<br>電話：02-2302-5162<br>網站：<a href="www.ymsnp.gov.tw/">www.ymsnp.gov.tw/</a></address>'
,"~/Content/themes/base/images/langicon.png"],
['行天宮', 25.062927, 121.533599,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/shitan.jpg" />行天宮，俗稱恩主公廟，為主祀關公的臺灣民間信仰廟宇，是全臺知名的關帝廟，煤礦主黃欉所建設而成。行天宮參訪香客眾多，行天宮位於北市中山區，為行天三宮最晚成立者，行天宮也成為大臺北地區關帝廟的代表，廟門設計上與文廟臺北市孔廟相同，大門均沒有門神圖樣，用108顆門釘代表108位神靈，即36天罡星、72地煞星。 <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市中山區民權東路二段109號<br>電話：02-2502-7924<br>網站：<a href="www.ht.org.tw/">www.ht.org.tw/</a></address>'
,"~/Content/themes/base/images/shitanicon.png"],
['國立故宮博物院', 25.102355, 121.548492,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/gugo.jpg" />國立故宮博物院，簡稱故宮、故宮博物院或臺北故宮，又名中山博物院，座落在臺北市士林區外雙溪地區，為臺灣規模最大的博物館，同時也是古代中國藝術史與漢學研究的重鎮，所擁有的69.6萬餘件冊文物為世界上最負盛名的古代中國藝術品珍藏。 一年可接待超過500萬名參訪者，名列全球最受歡迎藝術博物館第7位。 <div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市士林區至善路二段221號<br>電話：02-2881-2021<br>網站：<a href="www.npm.gov.tw/">www.npm.gov.tw/</a></address>'
,"~/Content/themes/base/images/gugoicon.png"],
['中正紀念堂', 25.034741, 121.521913,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/chi.jpg" />中正紀念堂位於台北市中正區是一座為紀念前中華民國總統蔣中正而興建的建築，也是眾多紀念蔣中正總統的建築中規模最大者。全區250,000平方公尺，主樓高76公尺，管理機關為「國立中正紀念堂管理處」；而園區廣場的南北側，另建有國家表演藝術中心國家兩廳院管理的國家戲劇院和國家音樂廳，落成以來成為臺北市在國際上最著名地標之一。園區廣場除了供民眾休憩外，也常是大型藝文活動的場地，常舉辦展覽；邦交國元首訪臺歡迎儀式也在此舉行。園區部分建築被登錄為國定古蹟，園區被登錄為文化景觀。<div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市中正區中山南路21號<br>電話：02-2343-1100<br>網站：<a href="www.cksmh.gov.tw/">www.cksmh.gov.tw/</a></address>'
,"~/Content/themes/base/images/chiicon.png"],
['國父紀念館', 25.039525, 121.560267,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/ya.jpg" />國立國父紀念館是為紀念中華民國國父孫中山先生百年誕辰而興建的綜合性文化設施，於1972年5月16日落成啟用，全部面積共三萬五千坪。除了具紀念價值外，也是戶外運動、休閒、藝文與知性活動的綜合性休憩公園。<div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市信義區仁愛路四段505號<br>電話：02-2758-8008<br>網站：<a href="www.yatsen.gov.tw/">www.yatsen.gov.tw/</a></address>'
,"~/Content/themes/base/images/yaicon.png"],
['士林夜市', 25.087794, 121.524224,
'<div class="text"><img class="img-thumbnail" src="~/Content/themes/base/images/slin.jpg" />士林夜市或稱士林市場，位於臺灣臺北市士林區，以攤販範圍界於東邊文林路、西邊基河路、北邊小北街與小西街內為主的三角地帶，是台北市內最大、最為人所知的夜市。並多次獲選為台灣代表夜市，揚名國際。士林夜市原本是以小吃與攤商而興起。士林夜市與其他大型固定夜市共同維繫著這一特殊的文化現象，也成為台北人夜生活的經常去處。<div class="text-right" style="padding:5px 0px;">資料來源：維基百科</div></div><address>聯絡資訊：<br> 地址：台北市士林區基河路101號<br>電話：02-2881-5557</address>'
,"~/Content/themes/base/images/slinicon.png"]
];
/*建立初始地圖，設定初始座標*/
function initMap() {
    var mapLatlngs = { lat: 25.0540009, lng: 121.5626713 }
    map = new google.maps.Map(document.getElementById('map'), {
        center: mapLatlngs,
        zoom: 12
    });
    setMarkers(map, latlngs);
    
   
}
/*建立自訂的新Markers*/
function setMarkers(map, latlngs) {
    var marker,i;
    var infowindow = new google.maps.InfoWindow({maxWidth:400});
	
	//在googleMap插入自訂的Marker
    for (i = 0; i < latlngs.length; i++) {
        
        var lat = latlngs[i][1];
        var lng = latlngs[i][2];
        var travelLatlng = new google.maps.LatLng(lat, lng);

        var marker = new google.maps.Marker({
            map: map,
            position: travelLatlng, //景點座標
            title: latlngs[i][0], //景點名稱
            icon:latlngs[i][4] //自訂景點icon的檔案位置
        });

        //infowindow中插入各景點簡介內容
        var iconcontent = latlngs[i][0];
        var content ='<div class="mcontainer"><div class="title">' + latlngs[i][0] + '</div><div class="mcontent">'+ latlngs[i][3] +'</div></div>';
		
		//開啟infowindow景點簡介
        google.maps.event.addListener(marker, 'click', (function (marker, content, infowindow) {
            return function () {
                infowindow.setContent(content);
                infowindow.open(map, marker);
                map.setCenter(marker.getPosition());
                map.setZoom(17);
            };
        })(marker, content, infowindow));
		
		//關閉infowindow景點簡介
        google.maps.event.addDomListener(infowindow, "closeclick", function () {
            google.maps.event.trigger(map, "close");
            map.setZoom(12);
            map.setCenter({ lat: 25.0540009, lng: 121.5626713 });
        });
    }
}


