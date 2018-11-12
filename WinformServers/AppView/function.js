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
        openForm: function (option, callback, closeBack) {
            $(this).on("click", function () {
                var index = $.openForm(option, closeBack);
                if (typeof callback == "function") {
                    callback(index);
                }
            });
        },
        /**
         * 为事件注册回车
         */
        keyDownEnter: function (callback) {
            $(this).keydown(function (e) {
                if (e.keyCode == 13) {
                    if (typeof callback == "function") {
                        callback();
                    }
                }
            });
        }
    });



    $.extend({
        /**
         * 弹出加载层
         * 
         */

        /**
         * 跳转到制定页面，并设置窗体大小
         * @param {*} src 跳转页面
         * @param {*} size [width,height] 窗体大小
         */
        openForm: function (option, callback, closeBack) {
            if (typeof src == "string") {
                option["content"] = option;
            }

            option = $.extend({}, defoption, option);
            //iframe层-父子操作
            var index = layer.open({
                type: option["type"],
                title: option["title"],
                area: option["area"],
                fixed: option["fixed"], //不固定
                maxmin: option["maxmin"],
                content: option["content"],
                success: function (lay, index) {
                    var left = $(lay).offset().left;
                    var top = $(lay).offset().top;
                    if (left < 0) {
                        $(lay).css("left", (window.screen.width - $(lay).width() - 80) / 2 + "px");
                    }
                    if (top < 0) {
                        $(lay).css("top", ((window.screen.height - $(lay).height()) / 2 - 45) + "px");
                    }
                },
                cancel: function (index, layero) {
                    if (option["cancelMsg"]) {
                        var conIndex = layer.confirm('确定关闭吗？', {
                            btn: ['确定', '取消'] //按钮
                        },
                            function (_index, lary) {
                                if (typeof closeBack == "function") {
                                    closeBack(_index, lary);
                                } else {
                                    layer.close(index);
                                    layer.close(_index);
                                }
                            },
                            function (_index, lary) {
                                layer.close(_index);
                                return false;

                            });
                        return false;
                    } else {
                        layer.close(index);
                        if (typeof closeBack == "function") {
                            closeBack(null,null);
                        } 
                    }
                }
            });

            if (typeof callback == "function") {
                callback(index);
            }

            return index;
        },
        /**
         *生成随机唯一字符
         * @param len 长度
         */
        getUUid: function (len) {
            if(len==undefined){
                len=16;
            }
           return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
                var r = Math.random()*len|0, v = c == 'x' ? r : (r&0x3|0x8);
                return v.toString(16);
           });
        },
        /**
         * 随机生成颜色并 赋值给 style的属性
         * @param  stylePro 默认=="color"
         * @param rgbRange 随机颜色范围 [[0, 255], [0, 255], [0, 255]];
         * 
         */
        setRandColor: function (selector, stylePro, rgbRange) {
            if (!stylePro) {
                stylePro = "color";
            }
            if (!rgbRange) {
                rgbRange = [[0, 255], [0, 255], [0, 255]];
            }

            var R = Math.floor(Math.random() * (rgbRange[0][1] - rgbRange[0][0]) + rgbRange[0][0]);
            var G = Math.floor(Math.random() * (rgbRange[1][1] - rgbRange[1][0]) + rgbRange[1][0]);
            var B = Math.floor(Math.random() * (rgbRange[2][1] - rgbRange[2][0]) + rgbRange[2][0]);
            $(selector).css(stylePro, 'rgb(' + R + ',' + G + ',' + B + ')'); 
        },
        /**
        * 根据class随机生成颜色并 赋值给 style的属性
        * @param  stylePro 默认=="color"
        */
        setRandColorByClass: function (clas, stylePro, min, max) {
            if (!stylePro) {
                stylePro = "color";
            }
            if (clas.indexOf(".") < 0) {
                clas = "." + clas;
            }
            var length = $(clas).length;
            for (var i = 0; i < length; i++) {
                var selector = clas + ":eq(" + i + ")";
                $.setRandColor(selector, stylePro, min, max);
            }
        }, 
        /* 使用回调函数处理 需要接管后的操作
         * @param iframe jquery iframe对象
         * @param target 接管的Tager类型 [_blank	在新窗口中打开被链接文档。_self	默认。在相同的框架中打开被链接文档。_parent	在父框架集中打开被链接文档。_top	在整个窗口中打开被链接文档。framename	在指定的框架中打开被链接文档。]        
         * @callback 回调函数，会返回2个参数 [e:所有target="_black"点击的事件源;url 获取到的url路径]
         */
        iframeTargetClick: function (iframe, target, callback) {
            if (typeof target == "function") {
	            callback = target;
	            target = undefined;
            }
            var find = !target ? "a:not([href^='#'])" : "[target='" + target + "']";
            iframe.load(function () {
	            iframe.contents().find(find).click(function (e) {
	                var url = $(this).attr("href");
	                callback(e, url)
	            });
	            var ifrmaes = iframe.contents().find("iframe");
	            if (iframes.length > 0) {
	                for (var i = 0; i < iframe.length; i++) {
		            $.iframeTargetClick($(iframe[i]), target, callback);
	                }
	            }
            })
        },
        padLeft: function (value, len) {
            if (!len) {
                len = 2;
            }
            var v = "0000000000000000000000" + value;
            return v.substring(v.length - len, v.length);
        },
        getTime: function () {
            var d = new Date();
            var time = [$.padLeft(d.getHours()), $.padLeft(d.getMinutes()), $.padLeft(d.getSeconds())];
            return time.join(":");
        },
        getDay: function () {
            var d = new Date();
            var day = [d.getFullYear(), $.padLeft(d.getMonth() + 1), $.padLeft(d.getDate())];
            return day.join("-");
        },
        getDayTime: function () {
            return $.getDay() + " " + $.getTime();
        },
        getNow: function () {

        },
        /**
         * 获取当前url参数
         */
        getRequestValue: function (key, url) {
            if (!url) {
                url = location.href;
            }
            var href = "{'" + url.replace(/.*[?]/g, "").replace(/&/g, "','").replace(/=/g, "':'") + "'}";
            var request = eval("(" + href + ")");
            try {
                return key == undefined ? request : request[key];
            } catch (e) {
                return null;
            }
        }
        
    });
    var defoption = {
        type: 2,
        title: "欢迎使用--控制台",
        area: [(window.screen.width - 200) + 'px', (window.screen.height - 120) + 'px'],
        fixed: false, //不固定
        maxmin: true,
        content: "",
        cancelMsg: true
    }

})(window.jQuery);
