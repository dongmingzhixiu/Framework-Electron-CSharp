
/**
 * 底部消息显示条
 * 依赖项: jquery.js 
 * 需引入jQuery文件
 * 作者：jpw
 * Q Q: 1427953302
 */

;
//闭包限定命名空间
(function ($) {
    //扩展jquery 方法
    $.fn.extend({
    
    });


    $.extend({
        /**
         * 弹出底部消息框
         * 
         */
        openBottomTip:function(text,callback,dbcallback){
            if(typeof text=="string"){
                text={"text":text};
            }

            var option= $.extend({}, defoption, text);  

            var uuid='xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
                var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
                return v.toString(16);
              });
            //引入js文件
            var str='<div class="bottomTip" id="'+uuid+'" style="user-select: none; -webkit-app-region: no-drag;display:none;padding: 2px 20px;border: 1px solid #ccc;background: '+option["background"]+';position: fixed;bottom: 0px;width: 100%;left: 0px;">'+
            '    <div style="float:left;cursor: default;color:'+option["color"]+'">'+option["text"]+'</div>'+
            '    <div id="close_'+uuid+'" style="float: left;right: 20px;width: 30px;position: absolute;padding: 0px 10px;cursor: pointer;" title="关闭tip">x</div>'+
            '</div>';
            $("body").append(str);
            $("#"+uuid).on("click",function(){
                if(typeof callback=="function"){
                    callback(uuid);
                }
            });

            $("#"+uuid).on("dbclick",function(){
                if(typeof dbcallback=="function"){
                    dbcallback(uuid);
                }
            });
            $("#close_"+uuid).on("click",function(){
                $("#"+uuid).slideDown().remove();
            });
            $("#"+uuid).slideUp().show();
            
            return uuid;
        },
        /**
         * 关闭底部消息框
         */
        closeBottomTip:function(index){
            if(!index){
                $(".bottomTip").remove();
                return ;
            }
            $("#"+index).remove();
        },
    });
   var defoption={
       background:'#009688',
       color:'#fff',
       text:'欢迎使用JpFramework，快速开发你的PC应用程序'
   }
    
})(window.jQuery);