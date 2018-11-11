
  /**
 * 加载层
 * 依赖项:   jquery.js           手动引入
 * 需引入jQuery文件,jquery版本在1.10以上(layui.js要求jquery版本在1.10.0以上)		 (需要手动引用，且必须在当前js插件之前)
 * 作者：jpw
 * Q Q: 1427953302
 */

;
//闭包限定命名空间
(function ($) {
    //扩展jquery 方法
    $.fn.extend({
    	/***
    	 * 为制定标签创建点击加载层效果
    	 */
    	//调用之前引用文件 <script type="text/javascript" src="${ctx}/resources/js/checkInput/layui.all.js"></script>
    	openLoadForm:function(msg,callback){
    		$(this).on("click",function(){
    			var index=$.openLoadForm(msg);
    			if(typeof callback=="function"){
    				callback(index);
    			}
    		});
    	},
    });


    $.extend({
        /**
         * 弹出加载层
         * 
         */
    	//调用之前引用文件 <script type="text/javascript" src="${ctx}/resources/js/checkInput/layui.all.js"></script>	
        openLoadForm:function(msg){
            if(typeof msg=="string"){
                msg["msg"]="加载中，请稍后";
            }

            var option= $.extend({}, defoption, msg);  

           //加载层-风格4

           var uuid= layer.msg('加载中', {
                icon: option["icon"],
                shade: option["shade"],
                time:option["time"],
            });
            return uuid;
        },
        /**
         * 关闭加载层
         */
        closeLoadForm:function(index){
            if(!index){
                layer.closeAll();
                return ;
            }
            layer.close(index);
        },
    });
    var defoption={
        icon:16,
        shade: 0.21,
        time:0,
        msg:'加载中，请稍后'
    }
    
})(window.jQuery);