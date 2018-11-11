window.weather=null;
(function ($){
    $("#close").on("click",function(){ 
        var currwin =require('electron').remote.getCurrentWindow();
        currwin.close();
    });


    window.initTime = new Date();
    window.bgIndex = 0;


    initCenter();

    function padLeft(h){
        return h>=10?h:"0"+h;
    }

    function initCenter(){
        var count=0;
        //初始化事件
        init(count);
       
        setInterval(function(){
            init(count);
            count++;

            $(".restTime").html((function(){
                w=window.initTime,d=new Date(),totalSecs=(d-w)/1000;   //获得两个时间的总毫秒数. 靠前的就调换再减。
                var days=Math.floor(totalSecs/3600/24); 
                hours=Math.floor((totalSecs-days*24*3600)/3600);
                mins=Math.floor((totalSecs-days*24*3600-hours*3600)/60);
                s=Math.floor((totalSecs-days*24*3600-hours*3600-mins*60));
                time=[padLeft(hours),padLeft(mins),padLeft(s)];
                return (days<=0?"":days+"天")+time.join(':');
            })());
        },1000);


        function init(count){
            $(".time").html((function(){
                return d=new Date(),
                s=[(h=d.getHours(),padLeft(h)),(m=d.getMinutes(),padLeft(m)),(s=d.getSeconds(),padLeft(s))],
                s.join(':');
            })());
            if(count%60000==0){
                $(".day").html((function(){
                    return d=new Date(),
                    s=[d.getFullYear(),(m=d.getMonth()+1,padLeft(m)),(_d=d.getDate(),padLeft(_d))],
                    s.join('-');
                })());
            }
            if(count%3600000==0||window.weather==null){
                 $.ajax({
                     url: "http://wthrcdn.etouch.cn/weather_mini?city=兰州市",
                     success: function (data) {
                        window.weather = JSON.parse(data);
                        if (typeof window.weather == "string") {
                            window.weather = JSON.parse(window.weather);
                        }
                        if (window.weather.desc == "OK") {
                            var getHtml = function (day, week, wendu, type, fengxiang) {
                                return '<div class="weather-list"><div>' + day + '</div><div>' + week + '</div><div>' + wendu + '</div><div>' + type + '</div><div>' + fengxiang + '</div></div>';
                            }
                            var getStr = function (_d) {
                                str = getHtml(_d.date.split('星')[0], "星" + _d.date.split('星')[1], _d.low.replace(/[^0-9.]/g, '') + "~" + _d.high.replace(/[^0-9.℃]/g, ''), _d.type, _d.fengxiang);
                                return str.replace(/undefined/g, "");
                            }
                            var d = window.weather.data;
                            var str = "<div>" + d.city + "：" + d.wendu + "℃</div><div style='height:150px'>";
                            var _d = d.yesterday;
                            str += getStr(_d);
                            for (var i = 0; i < d.forecast.length; i++) {
                                _d = d.forecast[i];
                                str += getStr(_d);
                            }
                            str += '</div><div class="ganmao">' + d.ganmao + '</div>';
                            $(".weather").html(str);
                            $(".weather-list:eq(0)").css("border-left", "1px solid #ffffff36");
                        }
                    }
                });
            }
        }

        
        $(".week").html((function(){
           return d=new Date(),w=["星期日","星期一","星期二","星期三","星期四","星期五","星期六"],
           w[d.getDay()];
        })());
       
        initPoetry();
        function initPoetry(){
            var data = $.ajax({
                url: "http://api.apiopen.top/recommendPoetry", success: function (data) {
                    var _data = JSON.parse(data) || JSON.parse(data.respendText);
                    if (_data.code == "200") {
                        var getStr = function (d) {
                            return '<div><div class="pro-title">' + d.title + '</div ><div class="pro-authors">' + d.authors + '</div><div class="pro-content">' + d.content.replace(/[|]/g, '</p><p>') + '</p></div></div>';
                        }
                        var str = getStr(_data.result);
                        $(".poetry").html(str);
                    }
                }
            });
        }
        getBackImg(window.bgIndex);
        //初始化古诗词
        setInterval(function(){
            initPoetry();
            getBackImg(window.bgIndex);
            window.bgIndex++;
        },1000*30);

        //获取必应背景图片
        function getBackImg(bgIndex){
            // var url="https://www.bing.com";
            // var setBg=function(data){
            //     var _data=JSON.parse(data.responseText);
            //     if(_data.images.length>0){
            //         var bgUrl=url+_data.images[0].url;
            //         $("body").css("background","url('"+bgUrl+"')");
            //     }
            // }
            // $.ajax({url: url+"/HPImageArchive.aspx?format=js&idx="+bgIndex+"&n=1",
            //     dataType:"jsonp",
            //     jsonp:'setBg',
            //     success:function(data){
            //         setBg(data);
            //     }
            // });
        }
    }
    
})(top.$||window.$||window.jquery);